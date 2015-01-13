/********************************************************************************
 *    Project   : Awesomium.NET (TabbedFormsSample)
 *    File      : WebDocument.cs
 *    Version   : 1.7.0.0 
 *    Date      : 3/5/2013
 *    Author    : Perikles C. Stephanidis (perikles@awesomium.com)
 *    Copyright : ©2013 Awesomium Technologies LLC
 *    
 *    This code is provided "AS IS" and for demonstration purposes only,
 *    without warranty of any kind.
 *     
 *-------------------------------------------------------------------------------
 *
 *    Notes     :
 *
 *    Represents the contents of a tab in an application window. This control
 *    contains the WebControl and an independent bar with the address-box,
 *    navigation buttons etc..
 *    
 *    
 ********************************************************************************/

#region Using
using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Windows.Input;
using Awesomium.Core;
using Awesomium.Windows.Controls;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using WinRemote.App.Controllers;
using WinRemote.App.Views;

#endregion

namespace WinRemote
{
    partial class WebDocument : DockContent
    {
        #region Fields
        private const String JsFavicon = "(function(){links = document.getElementsByTagName('link'); wHref=window.location.protocol + '//' + window.location.hostname + '/favicon.ico'; for(i=0; i<links.length; i++){s=links[i].rel; if(s.indexOf('icon') != -1){ wHref = links[i].href }; }; return wHref; })();";
        private readonly bool _goToHome;
        private readonly bool _fixedUrl;
        private BrowserContainer _mainForm;
        #endregion


        #region Ctors
        // Since we are displaying our controls in a tabbed dock-manager,
        // we can pause and resume rendering ourselves.
        // (See documentation of: IsRendering, OnEnabledChanged)

        public WebDocument()
        {
            InitializeComponent();
            
            _goToHome = true;
            Initialize();
        }

        public WebDocument( Uri url )
        {
            InitializeComponent();

            Initialize();
            webControl.Source = url;
            CreateBrowser();
        }

        public WebDocument( IntPtr nativeView )
        {

            InitializeComponent();

            Initialize();
            webControl.NativeView = nativeView;

        }

        public WebDocument( Uri url, string title )
        {
            InitializeComponent();

            Initialize();

            _fixedUrl = true;
            webControl.Source = url;
            Text = title;

            //this.toolStripButton4.Visible = false;
            //this.toolStripButton5.Visible = false;
            //this.addressBox.ReadOnly = true;
           
        }
        #endregion


        #region Methods
        private void Initialize()
        {
            
           // KeyDown += Key_Pressed;

            // Set the source for our data bindings.
            webControlBindingSource.DataSource = webControl;
            
            // In this example, ShowCreatedWebView of all WebControls, 
            // is handled by a common handler.
            if (CultureInfo.CurrentCulture.Name.Contains("de"))
            {
                webControl.ShowCreatedWebView += Browser.OnShowNewView;
                WebSession session = WebCore.CreateWebSession(new WebPreferences
                {
                    AcceptLanguage = "de,de"
                    
                });
                webControl.WebSession = session;


            }

        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            cutToolStripMenuItem.Enabled = ( webControl.FocusedElementType == FocusedElementType.EditableContent ) && webControl.HasSelection;
            copyToolStripMenuItem.Enabled = webControl.HasSelection;
            copyHTMLToolStripMenuItem.Enabled = webControl.HasSelection;
            pasteToolStripMenuItem.Enabled = ( webControl.FocusedElementType == FocusedElementType.EditableContent ) && Clipboard.ContainsText();

            if ( _goToHome )
                webControl.GoToHome();
        }

        protected override void OnDockStateChanged( EventArgs e )
        {
            base.OnDockStateChanged( e );

            switch ( DockState )
            {
                case DockState.Hidden:
                    // Pause rendering when this window is hidden.
                    webControl.Enabled = false;
                    break;

                case DockState.Document:
                    // A non-activated document, should be covered
                    // by others since it is displayed in a tab-control;
                    // we can safely pause rendering when it is not activated.
                    // This is not the case with tool-windows docked on
                    // the sides of the main window that may be visible while not activated;
                    // this is why we default to true for all other scenarios below.
                    webControl.Enabled = IsActivated;
                    break;

                default:
                    webControl.Enabled = true;
                    break;
            }
        }

        protected override void OnGotFocus( EventArgs e )
        {
            base.OnGotFocus( e );
            // Transfer focus to the control when the
            // tab acquires it.
            webControl.Focus();
        }

        private void UpdateFavicon()
        {
            // Execute some simple javascript that will search for a favicon.
            string val = webControl.ExecuteJavascriptWithResult( JsFavicon );

            // Check for any errors.
            if ( webControl.GetLastError() != Error.None )
                return;

            // Check if we got a valid response.
            if ( String.IsNullOrEmpty( val ) || !Uri.IsWellFormedUriString( val, UriKind.Absolute ) )
                return;

            // We do not need to perform the download of the favicon synchronously.
            // May be a full icon set (thus big).
            Task.Factory.StartNew<Icon>( GetFavicon, val ).ContinueWith( t =>
            {
                // If the download completed successfully, set the new favicon.
                // This post-completion procedure is executed synchronously.

                if ( t.Exception != null )
                    return;

                if ( t.Result != null )
                    Icon = t.Result;

                if ( DockPanel != null )
                    DockPanel.Refresh();
            },
            TaskScheduler.FromCurrentSynchronizationContext() );
        }

        private static Icon GetFavicon( Object href )
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    //catch WebException, necessary for operating on a local server
                    Byte[] data = client.DownloadData(href.ToString());

                    if ((data == null) || (data.Length <= 0))
                        return null;

                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        try
                        {
                            return new Icon(ms, 16, 16);
                        }
                        catch (ArgumentException)
                        {
                            // May not be an icon file.
                            using (Bitmap b = new Bitmap(ms))
                                return Icon.FromHandle(b.GetHicon());
                        }
                    }
                }
            }
            catch (WebException) { return null; }
        }
        #endregion

        #region Properties
        public BrowserContainer MainForm
        {
            get
            {
                if ( _mainForm == null )
                    _mainForm = DockPanel != null ?
                        DockPanel.FindForm() as BrowserContainer :
                        Application.OpenForms.OfType<BrowserContainer>().FirstOrDefault();

                return _mainForm;
            }
        }

        public bool IsRendering
        {
            get { return webControl.Enabled; }
            set { webControl.Enabled = value; }
        }
        #endregion

        #region Event Handlers

        #region WebControl
        private void webControl_DomReady( object sender, EventArgs e )
        {
            // DOM is ready. We can start looking for a favicon.
            UpdateFavicon();
        }

        private void webControl_TargetUrlChanged( object sender, UrlEventArgs e )
        {
            BrowserContainer mainForm = MainForm;

            if ( mainForm != null )
                mainForm.Status = e.Url == null ? String.Empty : e.Url.AbsoluteUri;
        }

        private void webControl_PropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            BrowserContainer mainForm = MainForm;

            switch ( e.PropertyName )
            {
                case "Title":
                    if ( _fixedUrl )
                        break;

                    // The WebCocument's title is updated by the data binding.

                    if ( mainForm != null )
                        mainForm.Title = webControl.Title;

                    break;
                case "IsLoading":
                    if ( mainForm != null )
                        mainForm.ShowProgress = webControl.IsLoading;

                    break;

                case "HasSelection":
                    if ( !webControl.HasSelection )
                    {
                        cutToolStripMenuItem.Enabled = false;
                        copyToolStripMenuItem.Enabled = false;
                        copyHTMLToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        cutToolStripMenuItem.Enabled = webControl.FocusedElementType == FocusedElementType.EditableContent;
                        copyToolStripMenuItem.Enabled = true;
                        copyHTMLToolStripMenuItem.Enabled = true;
                    }
                    break;

                case "FocusedElementType":
                    pasteToolStripMenuItem.Enabled = ( webControl.FocusedElementType == FocusedElementType.EditableContent ) && Clipboard.ContainsText();
                    break;

                case "IsCrashed":
                    bool isLive = !webControl.IsCrashed;

                    copyToolStripMenuItem.Enabled =
                        cutToolStripMenuItem.Enabled =
                        copyHTMLToolStripMenuItem.Enabled =
                        pasteToolStripMenuItem.Enabled =
                        refreshToolStripMenuItem.Enabled =
                        printToolStripMenuItem.Enabled =
                        selectAllToolStripMenuItem.Enabled = isLive;

                    break;
            }
        }

        private void webControl_BeginLoading( object sender, LoadingFrameEventArgs e )
        {
            if ( !e.IsMainFrame )
                return;

            // Clear the old favicon.
            if ( Icon != null )
                Icon.Dispose();

            var resources = new ComponentResourceManager( typeof( WebDocument ) );
            Icon = ( (Icon)( resources.GetObject( "$this.Icon" ) ) );

            if ( DockPanel != null )
                DockPanel.Refresh();
        }

        private void webControl_SelectLocalFiles( object sender, FileDialogEventArgs e )
        {
            using ( var dialog = new OpenFileDialog
            {
                Title = e.Title,
                InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ),
                CheckFileExists = true,
                Multiselect = e.Mode == WebFileChooserMode.OpenMultiple
            } )
            {
                if ( ( dialog.ShowDialog( MainForm ) == DialogResult.OK ) && ( dialog.FileNames.Length > 0 ) )
                    e.SelectedFiles = dialog.FileNames;
                else
                    e.Cancel = true;
            }
        }

        private void webControl_WindowClose( object sender, WindowCloseEventArgs e )
        {
            // Respect window.close if we are a popup.
            if ( DockPanel == null )
                Close();
        }
        #endregion

        #region Toolstrip / Menu
        private void closeTabToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void closeOtherTabsToolStripMenuItem_Click( object sender, EventArgs e )
        {
            var contents = from content in DockPanel.Contents
                           where content is WebDocument
                           where content != this
                           select content as WebDocument;

            List<WebDocument> list = new List<WebDocument>( contents );
            list.ForEach( c => c.Close() );
        }


        private void newTabToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( DockPanel != null )
            {
                WebDocument doc = new WebDocument();
                doc.Show( DockPanel );
            }
        }

        private void refreshToolStripMenuItem_Click( object sender, EventArgs e )
        {
            webControl.Reload( false );
        }


        private void printToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( !webControl.IsLive )
                return;

            //webControl.Print();
        }

        private void cutToolStripMenuItem_Click( object sender, EventArgs e )
        {
           if ( !webControl.IsLive )
                webControl.Cut();
        }

        private void copyToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( !webControl.IsLive )
                webControl.Copy();
        }

        private void copyHTMLToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( !webControl.IsLive )
                return;

            if ( webControl.HasSelection )
                webControl.CopyHTML();
        }

        private void pasteToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( !webControl.IsLive )
                webControl.Paste();
        }

        private void selectAllToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if ( !webControl.IsLive )
                webControl.SelectAll();
        }

        #endregion

        #endregion


        //This fix enables DropDown menus in the awesomium webbrowser. In the current version (1.7.1) this is not supported

        private ListBox _lbSelect;
       
        /// <summary>
        /// Initialize the fix by adding controls and eventhandlers.
        /// </summary>
        private void CreateBrowser()
        {
            
            
            webControl.Dock = DockStyle.Fill;
            webControl.ShowPopupMenu += browser_ShowPopupMenu;

            _lbSelect = new ListBox();
            Controls.Add(_lbSelect);
            _lbSelect.BringToFront();
            _lbSelect.Visible = false;
            _lbSelect.MouseClick += lbSelect_MouseClick;
            _lbSelect.LostFocus += lbSelect_LostFocus;
        }

        private PopupMenuEventArgs _menuArgs;
        /// <summary>
        /// Draw your own drop down if the user clicks a dropdown on the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void browser_ShowPopupMenu(object sender, PopupMenuEventArgs e)
        {
            Console.WriteLine("x");
            _lbSelect.Items.Clear();
            uint count = e.Info.Count;
            for (uint i = 0; i < count; i++)
                _lbSelect.Items.Add(e.Info[i].Label);

            Rectangle lbb = new Rectangle
            {
                X = e.Info.Bounds.X,
                Y = e.Info.Bounds.Y + e.Info.Bounds.Height,
                Height = (count <= 10 ? (int)(e.Info.ItemHeight * count) : e.Info.ItemHeight * 10),
                Width = e.Info.Bounds.Width
            };

            _lbSelect.Bounds = lbb;
            _lbSelect.Visible = true;
            _lbSelect.SelectedIndex = e.Info.SelectedItem;
            _lbSelect.Focus();

            _menuArgs = e;
        }

        void lbSelect_MouseClick(object sender, MouseEventArgs e)
        {
            _menuArgs.Info.Select(_lbSelect.SelectedIndex);
            _lbSelect.Visible = false;
        }

        void lbSelect_LostFocus(object sender, EventArgs e)
        {
            _lbSelect.Visible = false;
        }

        private void Key_Pressed(object sender, KeyEventArgs e)
        {
            Console.WriteLine("trweqw");
            if (e.KeyCode == Keys.A) Console.WriteLine("true");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.C))
            {
                MessageBox.Show("You pressed Ctrl+A!");
            }
            return true;
        }
    }
}
