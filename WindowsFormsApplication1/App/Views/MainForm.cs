using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WinRemote.App.Controllers;
using WinRemote.App.Helpers;
using WinRemote.App.Models;

namespace WinRemote.App.Views
{
    /// <summary>
    /// Holds any relevant logic(eventhandlers, etc.) behind the main application window.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Sets default translation based on resource files.
        /// </summary>
        public void Translate()
        {

            if (CultureInfo.CurrentCulture.Name.Contains("de"))
            {
                InfoSessionLabel.Text = Properties.translate_de.SessionInfo;
                LogoutButton.Text = Properties.translate_de.Logout;
                QuickDurationTextLabel.Text = Properties.translate_de.Duration;
                CatalogueDurationLabel.Text = Properties.translate_de.Duration;
                CatalogueTab.Text = Properties.translate_de.Catalogue;
                QuickstartTab.Text = Properties.translate_de.Quickstart;
                QTypeLabel.Text = Properties.translate_de.QuestionType;
                QuestionLabel.Text = Properties.translate_de.Question;
                OptionLabel.Text = Properties.translate_de.Options;
            }
            else
            {
                InfoSessionLabel.Text = Properties.translate.SessionInfo;
                LogoutButton.Text = Properties.translate.Logout;
                QuickDurationTextLabel.Text = Properties.translate.Duration;
                CatalogueDurationLabel.Text = Properties.translate.Duration;
                CatalogueTab.Text = Properties.translate.Catalogue;
                QuickstartTab.Text = Properties.translate.Quickstart;
                QTypeLabel.Text = Properties.translate.QuestionType;
                QuestionLabel.Text = Properties.translate.Question;
                OptionLabel.Text = Properties.translate.Options;
            }
        }

        #region fields
        /// <summary>
        /// Current xposition of the window. Needed for maximize and minimize behaviour. Ignores position changes due to max- or minimizing.
        /// </summary>
        private int _currentxpos;

        /// <summary>
        /// Current yposition of the window. Needed for maximize and minimize behaviour. Ignores position changes due to max- or minimizing.
        /// </summary>
        private int _currentypos;

        /// <summary>
        /// true, when window is being minimized, else false.
        /// </summary>
        private bool _isminimized;

        /// <summary>
        /// true, when form is ready to recieve further countdown messages from websocket.
        /// </summary>
        private bool _countdownmessagesrecievable = true;

        /// <summary>
        /// true, when results of the latest survey are ready to be displayed.
        /// </summary>
        private bool _resultsthere;
        /// <summary>
        /// true, when a survey is currently running.
        /// </summary>
        private Boolean _runningsurvey;

        #endregion

        

        #region initializing and authenticating
        /// <summary>
        /// Initializes the form, adds translation and places it at the bottom right corner of the user's display.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
            //Wanna test english language?
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width, Screen.PrimaryScreen.WorkingArea.Height - Height);
            Translate();
            
        }

     

     
        /// <summary>
        /// Is called on loading the form. Basically checks for validity of user's authentication token.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            DbController.CheckVersion();
            var baseUrl = DbController.RetrieveBaseUrl();
            var baseSocketUrl = DbController.RetrieveBaseSocketUrl();

            if (baseUrl.Length > 0 && baseSocketUrl.Length > 0)
            {
                Settings.BaseUrl = baseUrl;
                Settings.BaseSocketUrl = baseSocketUrl;
            }

            var authToken = DbController.RetrieveAuthToken();//get token from db

            if (authToken == null || !User.valid_token(authToken))//token is not valid
            {
                //open LoginForm and subscribe to a successful authentication
                var login = new LoginForm();
                login.AuthSuccess += AuthSuccessListener;
                login.ShowDialog();
            }

            else//token is valid
            {
                Settings.AuthToken = authToken;
                Settings.ListBuilding();
                BuildLists();
                new Thread(() => Browser.Start(Settings.BaseUrl, false)).Start();
            }

           // new Thread(new ThreadStart(new emptyFunction(delegate() { while (true) { Console.WriteLine(Application.OpenForms.Cast<Form>().Last()); Thread.Sleep(1000); } }))).Start();
            
        }
        /// <summary>
        /// Listener for successful authentication. Initializes lists when successful.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthSuccessListener(object sender, EventArgs e)
        {
            Settings.ListBuilding();
            BuildLists();
        }

        /// <summary>
        /// Has to be called before displaying the form. Initializes any needed dropdown menus and checks for the user's
        /// session and question collection.
        /// </summary>
        public void BuildLists()
        {
            Startsettings.Get();//get default quickstart settings from PINGO server
            //Create lists in the quickstart tab, preselect the first items in list
            ChooseType.Items.AddRange(items: Startsettings.TypeList.ToArray());
            ChooseType.SelectedItem = ChooseType.Items[0];
            ChooseQuickDuration.Items.AddRange(items: Startsettings.DurationChoices.ToArray());
            ChooseQuickDuration.SelectedItem = ChooseQuickDuration.Items[0];
            ChooseDuration.Items.AddRange(items: Startsettings.DurationChoices.ToArray());
            ChooseDuration.SelectedItem = ChooseDuration.Items[0];

            //Create user's sessionlist
            object[] eventarr = Event.All().ToArray();
            SessionList.Items.AddRange(eventarr);
            if (SessionList.Items.Count == 0)//user has no sessions
            {
                //The user will be logged out and asked to create sessions with his logged in account
                NotificationHelper.ShowError(Properties.translate.NoSessionError);
                ClearLists();
                User.Logout();//logging out the user gives the opportunity to sign in with another account
                var lf = new LoginForm();
                lf.ShowDialog();
            }
            else//user has sessions, preselect the first in list
                SessionList.SelectedItem = eventarr[0];
            //Initialize tag dropdown menu
            ChooseTag.Items.Add(Properties.translate.AllTags);
            ChooseTag.SelectedItem = Properties.translate.AllTags;
            var taglist = new List<string>();
            foreach (string s in Settings.Tagtable.Keys)
                if (!s.Equals(Properties.translate.AllTags))
                    taglist.Add(s);
            taglist.Sort();
            ChooseTag.Items.AddRange(items: taglist.ToArray());
            //QuestionList is initialized dynamically, depending on the currently selected tag. Look at ChooseTag_selected_changed for more.
        }
        #endregion

        #region TabControl
        /// <summary>
        /// Prevents switching tabs when a survey is currently running.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_runningsurvey)
                e.Cancel = true;
        }

        #endregion TabControl

        #region selecting from dropdown
        /// <summary>
        /// Changes the Session in the Settings according to the new choice. Updates the title of the window.
        /// -> Window will always display current session's name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SessionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Session = ((Event)(SessionList.SelectedItem));
            Text = Settings.Session.Token;
        }
        /// <summary>
        /// Set's the DurationLabel's text to the chosen duration. Probably not necessary. Just to be sure.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            DurationLabel.Text = ChooseDuration.SelectedItem.ToString();
        }
        /// <summary>
        /// Set's the QuickDurationLabel's text to the chosen duration. Probably not necessary. Just to be sure.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseQuickDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuickDurationLabel.Text = ChooseQuickDuration.SelectedItem.ToString();
        }
        /// <summary>
        /// Updates the question list according to the chosen tag. Disables starting a survey if no question is available for the 
        /// chosen tag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuestionList.Items.Clear();
            QuestionList.Items.AddRange(items: ((List<Question>)Settings.Tagtable[ChooseTag.SelectedItem]).ToArray()); //update QuestionList
            if (QuestionList.Items.Count == 0) //no question with such tag
            {
                StartSurvey.Visible = false;
                StopButton.Visible = false;
            }
            else //preselect first question, update tooltip(see below)
            {
                QuestionList.SelectedItem = QuestionList.Items[0];
                QuestionTooltip.SetToolTip(QuestionList, QuestionList.SelectedItem.ToString());
            }
        }
        /// <summary>
        /// Updates the textbox according to the chosen question. User's can read the full question's name in the
        /// textbox, while only seeing a small part of it in the dropdown. Furthermore updates the tooltip, which is shown, when
        /// hovering over the dropdown menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuestionTextBox.Text = QuestionList.SelectedItem.ToString();
            QuestionTooltip.SetToolTip(QuestionList, QuestionList.SelectedItem.ToString());
        }
        /// <summary>
        /// Updates the option choice according to the selected question type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SurveyType chosen = (SurveyType)ChooseType.SelectedItem;
            if (chosen.Options.First().Name.Equals(""))//Type has no options
            {
                OptionLabel.Visible = false;
                ChooseOptions.Visible = false;
            }
            else//Type has options
            {
                ChooseOptions.Items.Clear();
                ChooseOptions.Items.AddRange(items: chosen.Options.ToArray());
                ChooseOptions.SelectedItem = ChooseOptions.Items[0];
                ChooseOptions.Visible = true;
                OptionLabel.Visible = true;
            }
        }
        #endregion

        #region switching beetween running and not running surveys

        /// <summary>
        /// Executes necessary changes when a survey ends. Disabling Stop buttons, enabling Start buttons, disabling countdown, enabling
        /// panels for selecting new questions or surveys.
        /// </summary>
        private void SwitchModeOff()
        {
            CatalogueQuestionPanel.Visible = true;
            QuickstartPanel.Visible = true;
            StartSurvey.Enabled = true;
            StartQuickSurvey.Enabled = true;
            QuickStopButton.Enabled = false;
            StopButton.Enabled = false;
            QuickDurationLabel.Visible = false;
            DurationLabel.Visible = false;
            ParticipantLabel.Visible = false;
            QuickParticipantLabel.Visible = false;
            _runningsurvey = false;
        }

        /// <summary>
        /// Executes necessary changes when a survey starts. Enabling Stop buttons, disabling Start buttons, enabling countdown, disabling
        /// panels for selecting new questions or surveys.
        /// </summary>
        private void SwitchModeOn()
        {
            _resultsthere = false;
            CatalogueQuestionPanel.Visible = false;
            QuickstartPanel.Visible = false;
            StartSurvey.Enabled = false;
            StartQuickSurvey.Enabled = false;
            QuickStopButton.Enabled = true;
            StopButton.Enabled = true;
            QuickDurationLabel.Visible = true;
            DurationLabel.Visible = true;
            ParticipantLabel.Visible = true;
            QuickParticipantLabel.Visible = true;
            _runningsurvey = true;
        }

        #endregion switching beetween running and not running surveys

        #region start buttons
        /// <summary>
        /// Executed, when the start button in the quickstart tab is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartQuickSurvey_Click(object sender, EventArgs e)
        {
            //Necessary adjustments when starting a survey
            string part;
            string load;
            if (CultureInfo.CurrentCulture.Name.Contains("de"))
            {
                part = Properties.translate_de.Participants;
                load = Properties.translate_de.load;
            }
            else
            {
                part = Properties.translate.Participants;
                load = Properties.translate.load;
            }

            QuickDurationLabel.Text = load;
            QuickParticipantLabel.Text = part + ": 0";
            SwitchModeOn();
            //Start the survey with socket, etc...
            click_action(() => { Survey.PostSurvey(Settings.Session.Token, ((SurveyType)ChooseType.SelectedItem).Type, ((Duration)ChooseQuickDuration.SelectedItem).Sec.ToString(), ((TypeOption)ChooseOptions.SelectedItem).Name); });
        }
        /// <summary>
        /// Executed, when the start button in the catalogue tab is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartSurvey_Click(object sender, EventArgs e)
        {
            string part;
            if (CultureInfo.CurrentCulture.Name.Contains("de"))
            {
                part = Properties.translate_de.Participants;
                DurationLabel.Text = Properties.translate_de.load;
            }
            else
            {
                part = Properties.translate.Participants;
                DurationLabel.Text = Properties.translate.load;
            }
            //Necessary adjustments when starting a survey
            
            ParticipantLabel.Text = part + ": 0";
            SwitchModeOn();
            //Start the survey with socket, etc...
            click_action(() =>
            {
                Question.PostQuestion(Settings.Session.Token, ((Question)(QuestionList.SelectedItem)).Id, ((Duration)ChooseDuration.SelectedItem).Sec.ToString());
            });
        }

        /// <summary>
        /// Posts the question/survey and starts the websocket connection.
        /// </summary>
        /// <param name="f">The action which executes posting the survey/question to the server.</param>
        private void click_action(Action f)
        {
            f();
            var sh = new SocketHelper();
            sh.CountdownChanged += SocketCountdownMessageRecieved;
            sh.VotersChanged += SocketVotersMessageRecieved;
            sh.Execute();
            Settings.Session.ReloadLatestSurvey();
            //Subscribing for countdown and voter count of the started survey
            sh.SubscribeToSurveyCountdown(Settings.Session.LatestSurvey.Id);
            sh.SubscribeToSurveyVoters(Settings.Session.LatestSurvey.Id);
        }
        #endregion

        #region stop buttons
        /// <summary>
        /// Executed when a survey from a catalogue question is being stopped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            click_stop();
        }
        /// <summary>
        /// Executed when a survey from quickstart menu is being stopped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuickStopButton_Click(object sender, EventArgs e)
        {
            click_stop();
        }

        /// <summary>
        /// Necessary prompts for stopping a survey: Calling the StopSurvey Method and sending a message to the listeners
        /// of the websocket on stopping the survey manually.
        /// </summary>
        private void click_stop()
        {
            Survey.StopSurvey(Settings.Session);
            var sh = new SocketHelper();
            SocketCountdownMessageRecieved(sh, new SocketEventArgs("stopped manually"));
        }
        #endregion

        #region logout
        /// <summary>
        /// Performs clearLists and logs the user out thus showing the login in dialog again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            ClearLists();
            User.Logout();
            //show Login again
            var login = new LoginForm();
            login.AuthSuccess += this.AuthSuccessListener;
            login.ShowDialog();
        }

        /// <summary>
        /// Clears all dropdown lists. Necessary for logout.
        /// </summary>
        private void ClearLists()
        {
            SessionList.Items.Clear();
            QuestionList.Items.Clear();
            ChooseTag.Items.Clear();
            ChooseDuration.Items.Clear();
            ChooseType.Items.Clear();
            ChooseQuickDuration.Items.Clear();
        }

        #endregion

        #region listening to socket events
        /// <summary>
        /// Called when the countdown needs to be updated. Updates the countdowns and opens the built in webbrowser with the
        /// result page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SocketCountdownMessageRecieved(object sender, SocketEventArgs e)
        {
            var payload = e.Payload;
            if (payload.Equals("stopped") || !_countdownmessagesrecievable) return; //do not accept any more messages
            if (payload.Equals("stopped manually")) { payload = "0"; _countdownmessagesrecievable = false; } //stop button was pressed
            //Update countdowns
            DurationLabel.Invoke(new emptyFunction(delegate { DurationLabel.Text = Duration.ConvertSeconds(Convert.ToInt32(Convert.ToDouble(payload))); }));
            QuickDurationLabel.Invoke(new emptyFunction(delegate { QuickDurationLabel.Text = Duration.ConvertSeconds(Convert.ToInt32(Convert.ToDouble(payload))); }));
            if (Convert.ToDouble(payload) == 0 && !_resultsthere) //time to show results
            {
                _countdownmessagesrecievable = false;
                _resultsthere = true;
                Settings.Session.ReloadLatestSurvey();

                //Open the result page. By convention with developement team, this is the default result page and the parameter remote_view=true
                Browser.Start(Settings.BaseUrl + "/events/" + Settings.Session.Token + "/surveys/" + Settings.Session.LatestSurvey.Id + "/?auth_token=" + Settings.AuthToken + "&remote_view=true");
                //prepare to start new survey
                Invoke(new emptyFunction(SwitchModeOff)); //needs to be called with invoke, because called from socket thread
                _countdownmessagesrecievable = true;
            }
        }

        /// <summary>
        /// Called when socket informs on a changed number of current voters. Updates the participants labels to display the
        /// number of current voters properly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SocketVotersMessageRecieved(object sender, SocketEventArgs e)
        {
            //need to be called with invoke, because this method is called from another thread, namely the socket thread
            string part;
            if (CultureInfo.CurrentCulture.Name.Contains("de"))
                part = Properties.translate_de.Participants;
            else
                part = Properties.translate.Participants;
            ParticipantLabel.Invoke(new emptyFunction(delegate { ParticipantLabel.Text = part + ": " + e.Payload; }));
            QuickParticipantLabel.Invoke(new emptyFunction(delegate { QuickParticipantLabel.Text = part + ": " + e.Payload; }));
        }
        #endregion

        #region maximinimize change

        /// <summary>
        /// Overrides default maximized and minimized behaviour. On maximiziation, the window returns to it's last position before it was 
        /// minimized. On minimization the window is only set to it's minimal size and does not disappear in the task bar.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0112) // WM_SYSCOMMAND
            {
                // Check window state 
                if (m.WParam == new IntPtr(0xF030)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    // The window is being maximized
                    Size = MaximumSize;

                    Location = new Point(_currentxpos, _currentypos);

                    return;
                }
                if (m.WParam == new IntPtr(0xF020))
                {//window is being minimized
                    _isminimized = true;
                    Size = MinimumSize;
                    //go to bottom right corner
                    Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width, Screen.PrimaryScreen.WorkingArea.Height - Height);
                    _isminimized = false;
                    return;
                }
            }
            base.WndProc(ref m); //call default max and min procedure.
        }

        /// <summary>
        /// Updates current x and y pos, if window is not minimized currently.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (!_isminimized)
            {
                _currentxpos = Location.X;
                _currentypos = Location.Y;
            }
        }

        #endregion maximinimize change

        /// <summary>
        /// Called on closing the form. Shutdowns the Webcore of the built-in webbrowser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Close(object sender, EventArgs e)
        {
            Awesomium.Core.WebCore.Shutdown();
        }
      
    }
}