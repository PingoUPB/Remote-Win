using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Awesomium.Core;
using Awesomium.Windows.Forms;
using WinRemote.App.Views;

namespace WinRemote.App.Controllers
{
    /// <summary>
    /// needed to let mainform call methods
    /// </summary>
    public delegate void EmptyFunction();

    /// <summary>
    /// Provides Awesomium webbrowser control.
    /// </summary>

    ///<summary>
    ///Controls BrowserContainer and WebDocument
    /// </summary>
    internal class Browser
    {
        /// <summary>
        /// Latest URL given to the Browser Object.
        /// </summary>
        public static string CurrentUrl;

        /// <summary>
        ///  Opens Awesomium webbrowser with given url as start page
        /// </summary>
        /// <param name="url">The url to show on startup.</param>
        /// <param name="visibility">Indicates whether the BrowserContainer should be shown or hidden. The latter is used to
        /// preload the Browser once on start-up.</param>
        public static void Start(String url, Boolean visibility=true)
        {
            // Set some initialization settings.
            var webConfig = new WebConfig
            {
                HomeURL = new Uri(Settings.BaseUrl),
                LogLevel = LogLevel.Verbose
            };
            
            // Lazy initialization of the core.
// ReSharper disable once CSharpWarnings::CS0618
            if (!WebCore.IsRunning)
                WebCore.Initialize(webConfig);
            
            CurrentUrl = url;

          
            //Threadsafe delegation. MainForm needs to open browser as dialog.
            Settings.F1.Invoke(new EmptyFunction(delegate {
                if (!visibility)
                {
// ReSharper disable once ObjectCreationAsStatement
                    new BrowserContainer();           
                }
                else
                    new BrowserContainer().ShowDialog();          
            }));              
        }

        /// <summary>
        ///  Is called, when start is called. Controls the way tabs are opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void OnShowNewView(object sender, ShowCreatedWebViewEventArgs e)
        {      
            var view = sender as IWebView;

            if (view == null)
                return;

            if (!view.IsLive)
                return;

           
            BrowserContainer browsecont = Application.OpenForms.OfType<BrowserContainer>().FirstOrDefault();
            
            if (browsecont == null)
            {
                return;
            }
            // Treat popups differently. If IsPopup is true, the event is always
            // the result of 'window.open' (IsWindowOpen is also true, so no need to check it).
            // Our application does not recognize user defined, non-standard specs.
            // Therefore child views opened with non-standard specs, will not be presented as
            // popups but as regular new windows (still wrapping the child view however -- se below).
            if (e.IsPopup && !e.IsUserSpecsOnly)
            {
                // JSWindowOpenSpecs.InitialPosition indicates screen coordinates.
                Rectangle screenRect = e.Specs.InitialPosition.ToRectangle();

                // Set the created native view as the underlying view of the
                // WebControl. This will maintain the relationship between
                // the parent view and the child, usually required when the new view
                // is the result of 'window.open' (JS can access the parent window through
                // 'window.opener'; the parent window can manipulate the child through the 'window'
                // object returned from the 'window.open' call).
                var newWindow = new WebDocument(e.NewViewInstance)
                {
                    ShowInTaskbar = false,
                    ClientSize = screenRect.Size != Size.Empty ? screenRect.Size : new Size(640, 480)
                };

                // If the caller has not indicated a valid size for the new popup window,
                // let it be opened with the default size specified at design time.
                if ((screenRect.Width > 0) && (screenRect.Height > 0))
                {
                    // Assign the indicated size.
                    newWindow.Width = screenRect.Width;
                    newWindow.Height = screenRect.Height;
                }

                // Show the window.
                newWindow.Show();

                // If the caller has not indicated a valid position for the new popup window,
                // let it be opened in the default position specified at design time.
                if (screenRect.Location != Point.Empty)
                    // Move it to the specified coordinates.
                    newWindow.DesktopLocation = screenRect.Location;
            }
            else if (e.IsWindowOpen || e.IsPost)
            {
                // No specs or only non-standard specs were specified, but the event is still
                // the result of 'window.open' or of an HTML form with tagret="_blank" and method="post".
                // We will open a normal window but we will still wrap the new native child view,
                // maintaining its relationship with the parent window.
                var doc = new WebDocument(e.NewViewInstance);
                browsecont.OpenTab(doc);
            }
            else
            {
                // The event is not the result of 'window.open' or of an HTML form with tagret="_blank"
                // and method="post"., therefore it's most probably the result of a link with target='_blank'.
                // We will not be wrapping the created view; we let the WebControl hosted in ChildWindow
                // create its own underlying view. Setting Cancel to true tells the core to destroy the
                // created child view.
                //
                // Why don't we always wrap the native view passed to ShowCreatedWebView?
                //
                // - In order to maintain the relationship with their parent view,
                // child views execute and render under the same process (awesomium_process)
                // as their parent view. If for any reason this child process crashes,
                // all views related to it will be affected. When maintaining a parent-child
                // relationship is not important, we prefer taking advantage of the isolated process
                // architecture of Awesomium and let each view be rendered in a separate process.
                e.Cancel = true;
                // Note that we only explicitly navigate to the target URL, when a new view is
                // about to be created, not when we wrap the created child view. This is because
                // navigation to the target URL (if any), is already queued on created child views.
                // We must not interrupt this navigation as we would still be breaking the parent-child
                // relationship.
                var doc = new WebDocument(e.TargetURL);
                browsecont.OpenTab(doc);
            }
        }
    }
}