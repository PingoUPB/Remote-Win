/********************************************************************************
 *    Project   : Awesomium.NET (TabbedFormsSample)
 *    File      : MainForm.cs
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
 *    Application window. This does not act as a main-parent window. 
 *    It's reusable. The application will exit when all windows are closed.
 *    
 *    
 ********************************************************************************/

#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinRemote.App.Controllers;

#endregion

namespace WinRemote.App.Views
{
    /// <summary>
    ///     Awesomium web view container. Displays Webdocuments in tabs.
    /// </summary>
    partial class BrowserContainer : Form
    {
        #region Ctors

        /// <summary>
        ///     Default initialization.
        /// </summary>
        public BrowserContainer()
        {
            InitializeComponent();

           // KeyDown += Key_Pressed;
        }

        #endregion

        #region Methods

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Create the initial tab.
            OpenTab(new Uri(Browser.CurrentUrl)); //Open with last url given in start method of Browser.cs
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Hide during cleanup.
            Hide();

            // Close the views and perform cleanup for every tab.
            var contents = new List<Form>(dockPanel.Contents.OfType<Form>());

            foreach (Form content in contents)
                content.Close();

            // Save application settings.

            Close();
            base.OnFormClosing(e);
        }

        #endregion

        #region OpenTab

        internal WebDocument OpenTab(Uri url = null, string title = null)
        {
            // - A tab with no url specified, will open WebCore.HomeURL.
            // - A tab with a predefined title, will not display a toolbar
            //   and address-box. This is used to display fixed web content
            //   such as the Help Contents.
            WebDocument doc = url == null
                ? new WebDocument()
                : String.IsNullOrEmpty(title) ? new WebDocument(url) : new WebDocument(url, title);
            doc.Show(dockPanel);


            return doc;
        }

        internal void OpenTab(WebDocument doc)
        {
            doc.Show(dockPanel);
        }

        #endregion

        #endregion

        #region Properties

        public string Status
        {
            get { return statusLabel.Text; }
            set
            {
                if (String.CompareOrdinal(statusLabel.Text, value) == 0)
                    return;

                statusLabel.Text = value;
            }
        }

        public string Title
        {
            get { return Text; }
            set { Text = "PINGO - Web"; }
        }

        public bool ShowProgress
        {
            get { return progressBar.Visible; }
            set { progressBar.Visible = value; }
        }

        #endregion

        #region Event Handlers

        private void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            foreach (WebDocument doc in dockPanel.Documents)
                // We pause WebControl rendering for documents
                // that are currently not visible.
                doc.IsRendering = (doc == dockPanel.ActiveDocument);

            if (dockPanel.ActiveDocument != null)
            {
                var doc = (WebDocument) dockPanel.ActiveDocument;
                Text = String.Format("{0} - {1}", Application.ProductName, doc.Text);
                doc.Focus();
            }
            else
                Text = Application.ProductName;
        }

        #endregion

        private void Key_Pressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) Console.WriteLine("true");
        }
    }
}