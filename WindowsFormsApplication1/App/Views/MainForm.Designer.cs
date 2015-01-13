namespace WinRemote.App.Views
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label3 = new System.Windows.Forms.Label();
            this.QuickDurationTextLabel = new System.Windows.Forms.Label();
            this.ChooseQuickDuration = new System.Windows.Forms.ComboBox();
            this.OptionLabel = new System.Windows.Forms.Label();
            this.QTypeLabel = new System.Windows.Forms.Label();
            this.ChooseOptions = new System.Windows.Forms.ComboBox();
            this.ChooseType = new System.Windows.Forms.ComboBox();
            this.StartQuickSurvey = new System.Windows.Forms.Button();
            this.TagLabel = new System.Windows.Forms.Label();
            this.ChooseTag = new System.Windows.Forms.ComboBox();
            this.QuestionLabel = new System.Windows.Forms.Label();
            this.QuestionList = new System.Windows.Forms.ComboBox();
            this.CatalogueDurationLabel = new System.Windows.Forms.Label();
            this.ChooseDuration = new System.Windows.Forms.ComboBox();
            this.QuestionTextBox = new System.Windows.Forms.RichTextBox();
            this.StartSurvey = new System.Windows.Forms.Button();
            this.EditQuestionList = new System.Windows.Forms.Button();
            this.QuestionTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.QuickStopButton = new System.Windows.Forms.Button();
            this.DurationLabel = new System.Windows.Forms.Label();
            this.CatalogueQuestionPanel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SessionTab = new System.Windows.Forms.TabPage();
            this.InfoSessionLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.SessionList = new System.Windows.Forms.ComboBox();
            this.QuickstartTab = new System.Windows.Forms.TabPage();
            this.QuickParticipantLabel = new System.Windows.Forms.Label();
            this.QuickstartPanel = new System.Windows.Forms.Panel();
            this.QuickDurationLabel = new System.Windows.Forms.Label();
            this.CatalogueTab = new System.Windows.Forms.TabPage();
            this.ParticipantLabel = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.CatalogueQuestionPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SessionTab.SuspendLayout();
            this.QuickstartTab.SuspendLayout();
            this.QuickstartPanel.SuspendLayout();
            this.CatalogueTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // QuickDurationTextLabel
            // 
            resources.ApplyResources(this.QuickDurationTextLabel, "QuickDurationTextLabel");
            this.QuickDurationTextLabel.Name = "QuickDurationTextLabel";
            // 
            // ChooseQuickDuration
            // 
            this.ChooseQuickDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseQuickDuration.FormattingEnabled = true;
            resources.ApplyResources(this.ChooseQuickDuration, "ChooseQuickDuration");
            this.ChooseQuickDuration.Name = "ChooseQuickDuration";
            this.ChooseQuickDuration.SelectedIndexChanged += new System.EventHandler(this.ChooseQuickDuration_SelectedIndexChanged);
            // 
            // OptionLabel
            // 
            resources.ApplyResources(this.OptionLabel, "OptionLabel");
            this.OptionLabel.Name = "OptionLabel";
            // 
            // QTypeLabel
            // 
            resources.ApplyResources(this.QTypeLabel, "QTypeLabel");
            this.QTypeLabel.Name = "QTypeLabel";
            // 
            // ChooseOptions
            // 
            this.ChooseOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseOptions.FormattingEnabled = true;
            resources.ApplyResources(this.ChooseOptions, "ChooseOptions");
            this.ChooseOptions.Name = "ChooseOptions";
            // 
            // ChooseType
            // 
            this.ChooseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseType.FormattingEnabled = true;
            resources.ApplyResources(this.ChooseType, "ChooseType");
            this.ChooseType.Name = "ChooseType";
            this.ChooseType.SelectedIndexChanged += new System.EventHandler(this.ChooseType_SelectedIndexChanged);
            // 
            // StartQuickSurvey
            // 
            resources.ApplyResources(this.StartQuickSurvey, "StartQuickSurvey");
            this.StartQuickSurvey.Name = "StartQuickSurvey";
            this.StartQuickSurvey.UseVisualStyleBackColor = true;
            this.StartQuickSurvey.Click += new System.EventHandler(this.StartQuickSurvey_Click);
            // 
            // TagLabel
            // 
            resources.ApplyResources(this.TagLabel, "TagLabel");
            this.TagLabel.Name = "TagLabel";
            // 
            // ChooseTag
            // 
            this.ChooseTag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseTag.FormattingEnabled = true;
            resources.ApplyResources(this.ChooseTag, "ChooseTag");
            this.ChooseTag.Name = "ChooseTag";
            this.ChooseTag.SelectedIndexChanged += new System.EventHandler(this.ChooseTag_SelectedIndexChanged);
            // 
            // QuestionLabel
            // 
            resources.ApplyResources(this.QuestionLabel, "QuestionLabel");
            this.QuestionLabel.Name = "QuestionLabel";
            // 
            // QuestionList
            // 
            this.QuestionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QuestionList.FormattingEnabled = true;
            resources.ApplyResources(this.QuestionList, "QuestionList");
            this.QuestionList.Name = "QuestionList";
            this.QuestionList.SelectedIndexChanged += new System.EventHandler(this.QuestionList_SelectedIndexChanged);
            // 
            // CatalogueDurationLabel
            // 
            resources.ApplyResources(this.CatalogueDurationLabel, "CatalogueDurationLabel");
            this.CatalogueDurationLabel.Name = "CatalogueDurationLabel";
            // 
            // ChooseDuration
            // 
            this.ChooseDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseDuration.FormattingEnabled = true;
            resources.ApplyResources(this.ChooseDuration, "ChooseDuration");
            this.ChooseDuration.Name = "ChooseDuration";
            this.ChooseDuration.SelectedIndexChanged += new System.EventHandler(this.ChooseDuration_SelectedIndexChanged);
            // 
            // QuestionTextBox
            // 
            resources.ApplyResources(this.QuestionTextBox, "QuestionTextBox");
            this.QuestionTextBox.Name = "QuestionTextBox";
            this.QuestionTextBox.ReadOnly = true;
            // 
            // StartSurvey
            // 
            resources.ApplyResources(this.StartSurvey, "StartSurvey");
            this.StartSurvey.Name = "StartSurvey";
            this.StartSurvey.UseVisualStyleBackColor = true;
            this.StartSurvey.Click += new System.EventHandler(this.StartSurvey_Click);
            // 
            // EditQuestionList
            // 
            resources.ApplyResources(this.EditQuestionList, "EditQuestionList");
            this.EditQuestionList.Name = "EditQuestionList";
            this.EditQuestionList.UseVisualStyleBackColor = true;
            // 
            // QuestionTooltip
            // 
            this.QuestionTooltip.AutoPopDelay = 5000;
            this.QuestionTooltip.InitialDelay = 200;
            this.QuestionTooltip.ReshowDelay = 100;
            this.QuestionTooltip.ShowAlways = true;
            // 
            // QuickStopButton
            // 
            resources.ApplyResources(this.QuickStopButton, "QuickStopButton");
            this.QuickStopButton.Name = "QuickStopButton";
            this.QuickStopButton.UseVisualStyleBackColor = true;
            this.QuickStopButton.Click += new System.EventHandler(this.QuickStopButton_Click);
            // 
            // DurationLabel
            // 
            resources.ApplyResources(this.DurationLabel, "DurationLabel");
            this.DurationLabel.Name = "DurationLabel";
            // 
            // CatalogueQuestionPanel
            // 
            this.CatalogueQuestionPanel.BackColor = System.Drawing.Color.White;
            this.CatalogueQuestionPanel.Controls.Add(this.QuestionTextBox);
            this.CatalogueQuestionPanel.Controls.Add(this.ChooseDuration);
            this.CatalogueQuestionPanel.Controls.Add(this.CatalogueDurationLabel);
            this.CatalogueQuestionPanel.Controls.Add(this.TagLabel);
            this.CatalogueQuestionPanel.Controls.Add(this.QuestionLabel);
            this.CatalogueQuestionPanel.Controls.Add(this.ChooseTag);
            this.CatalogueQuestionPanel.Controls.Add(this.QuestionList);
            resources.ApplyResources(this.CatalogueQuestionPanel, "CatalogueQuestionPanel");
            this.CatalogueQuestionPanel.Name = "CatalogueQuestionPanel";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SessionTab);
            this.tabControl1.Controls.Add(this.QuickstartTab);
            this.tabControl1.Controls.Add(this.CatalogueTab);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.Tab_Selecting);
            // 
            // SessionTab
            // 
            this.SessionTab.Controls.Add(this.InfoSessionLabel);
            this.SessionTab.Controls.Add(this.label1);
            this.SessionTab.Controls.Add(this.LogoutButton);
            this.SessionTab.Controls.Add(this.SessionList);
            resources.ApplyResources(this.SessionTab, "SessionTab");
            this.SessionTab.Name = "SessionTab";
            this.SessionTab.UseVisualStyleBackColor = true;
            // 
            // InfoSessionLabel
            // 
            resources.ApplyResources(this.InfoSessionLabel, "InfoSessionLabel");
            this.InfoSessionLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.InfoSessionLabel.Name = "InfoSessionLabel";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // LogoutButton
            // 
            resources.ApplyResources(this.LogoutButton, "LogoutButton");
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.UseVisualStyleBackColor = true;
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // SessionList
            // 
            this.SessionList.DisplayMember = "Event";
            this.SessionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SessionList.FormattingEnabled = true;
            resources.ApplyResources(this.SessionList, "SessionList");
            this.SessionList.Name = "SessionList";
            this.SessionList.SelectedIndexChanged += new System.EventHandler(this.SessionList_SelectedIndexChanged);
            // 
            // QuickstartTab
            // 
            this.QuickstartTab.Controls.Add(this.QuickParticipantLabel);
            this.QuickstartTab.Controls.Add(this.QuickstartPanel);
            this.QuickstartTab.Controls.Add(this.QuickStopButton);
            this.QuickstartTab.Controls.Add(this.StartQuickSurvey);
            this.QuickstartTab.Controls.Add(this.QuickDurationLabel);
            resources.ApplyResources(this.QuickstartTab, "QuickstartTab");
            this.QuickstartTab.Name = "QuickstartTab";
            this.QuickstartTab.UseVisualStyleBackColor = true;
            // 
            // QuickParticipantLabel
            // 
            resources.ApplyResources(this.QuickParticipantLabel, "QuickParticipantLabel");
            this.QuickParticipantLabel.Name = "QuickParticipantLabel";
            // 
            // QuickstartPanel
            // 
            this.QuickstartPanel.BackColor = System.Drawing.Color.White;
            this.QuickstartPanel.Controls.Add(this.QTypeLabel);
            this.QuickstartPanel.Controls.Add(this.QuickDurationTextLabel);
            this.QuickstartPanel.Controls.Add(this.OptionLabel);
            this.QuickstartPanel.Controls.Add(this.ChooseType);
            this.QuickstartPanel.Controls.Add(this.ChooseQuickDuration);
            this.QuickstartPanel.Controls.Add(this.ChooseOptions);
            resources.ApplyResources(this.QuickstartPanel, "QuickstartPanel");
            this.QuickstartPanel.Name = "QuickstartPanel";
            // 
            // QuickDurationLabel
            // 
            resources.ApplyResources(this.QuickDurationLabel, "QuickDurationLabel");
            this.QuickDurationLabel.Name = "QuickDurationLabel";
            // 
            // CatalogueTab
            // 
            this.CatalogueTab.Controls.Add(this.ParticipantLabel);
            this.CatalogueTab.Controls.Add(this.StopButton);
            this.CatalogueTab.Controls.Add(this.CatalogueQuestionPanel);
            this.CatalogueTab.Controls.Add(this.StartSurvey);
            this.CatalogueTab.Controls.Add(this.DurationLabel);
            resources.ApplyResources(this.CatalogueTab, "CatalogueTab");
            this.CatalogueTab.Name = "CatalogueTab";
            this.CatalogueTab.UseVisualStyleBackColor = true;
            // 
            // ParticipantLabel
            // 
            resources.ApplyResources(this.ParticipantLabel, "ParticipantLabel");
            this.ParticipantLabel.Name = "ParticipantLabel";
            // 
            // StopButton
            // 
            resources.ApplyResources(this.StopButton, "StopButton");
            this.StopButton.Name = "StopButton";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_Close);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Move += new System.EventHandler(this.Form1_LocationChanged);
            this.CatalogueQuestionPanel.ResumeLayout(false);
            this.CatalogueQuestionPanel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.SessionTab.ResumeLayout(false);
            this.SessionTab.PerformLayout();
            this.QuickstartTab.ResumeLayout(false);
            this.QuickstartTab.PerformLayout();
            this.QuickstartPanel.ResumeLayout(false);
            this.QuickstartPanel.PerformLayout();
            this.CatalogueTab.ResumeLayout(false);
            this.CatalogueTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button StartSurvey;
        private System.Windows.Forms.ComboBox QuestionList;
        private System.Windows.Forms.Button EditQuestionList;
        private System.Windows.Forms.ComboBox ChooseTag;
        private System.Windows.Forms.RichTextBox QuestionTextBox;
        private System.Windows.Forms.Button StartQuickSurvey;
        private System.Windows.Forms.ComboBox ChooseOptions;
        private System.Windows.Forms.ComboBox ChooseType;
        private System.Windows.Forms.Label QuickDurationTextLabel;
        private System.Windows.Forms.ComboBox ChooseQuickDuration;
        private System.Windows.Forms.Label OptionLabel;
        private System.Windows.Forms.Label QTypeLabel;
        private System.Windows.Forms.ToolTip QuestionTooltip;
        private System.Windows.Forms.Label QuestionLabel;
        private System.Windows.Forms.Label TagLabel;
        private System.Windows.Forms.Label CatalogueDurationLabel;
        private System.Windows.Forms.ComboBox ChooseDuration;
        private System.Windows.Forms.Button QuickStopButton;
        private System.Windows.Forms.Label DurationLabel;
        private System.Windows.Forms.Panel CatalogueQuestionPanel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SessionTab;
        private System.Windows.Forms.Button LogoutButton;
        private System.Windows.Forms.ComboBox SessionList;
        private System.Windows.Forms.TabPage CatalogueTab;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TabPage QuickstartTab;
        private System.Windows.Forms.Panel QuickstartPanel;
        private System.Windows.Forms.Label QuickDurationLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label QuickParticipantLabel;
        private System.Windows.Forms.Label ParticipantLabel;
        private System.Windows.Forms.Label InfoSessionLabel;

        /// <summary>
        /// empty delegate function, needed for calling methods from another thread.
        /// </summary>
        public delegate void emptyFunction();
    }
}

