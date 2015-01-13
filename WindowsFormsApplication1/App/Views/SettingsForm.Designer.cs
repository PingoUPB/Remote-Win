namespace WinRemote.App.Views
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SiteURLText = new System.Windows.Forms.TextBox();
            this.SocketURLText = new System.Windows.Forms.TextBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.DiscardButton = new System.Windows.Forms.Button();
            this.InfoSettingsLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "PINGO Website URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "PINGO Socket URL";
            // 
            // SiteURLText
            // 
            this.SiteURLText.Location = new System.Drawing.Point(159, 46);
            this.SiteURLText.Name = "SiteURLText";
            this.SiteURLText.Size = new System.Drawing.Size(258, 22);
            this.SiteURLText.TabIndex = 2;
            // 
            // SocketURLText
            // 
            this.SocketURLText.Location = new System.Drawing.Point(159, 80);
            this.SocketURLText.Name = "SocketURLText";
            this.SocketURLText.Size = new System.Drawing.Size(258, 22);
            this.SocketURLText.TabIndex = 3;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(159, 132);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(138, 46);
            this.ResetButton.TabIndex = 4;
            this.ResetButton.Text = "Reset to default";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // DiscardButton
            // 
            this.DiscardButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.DiscardButton.Location = new System.Drawing.Point(17, 132);
            this.DiscardButton.Name = "DiscardButton";
            this.DiscardButton.Size = new System.Drawing.Size(136, 46);
            this.DiscardButton.TabIndex = 5;
            this.DiscardButton.Text = "Discard Changes";
            this.DiscardButton.UseVisualStyleBackColor = true;
            this.DiscardButton.Click += new System.EventHandler(this.DiscardButton_Click);
            // 
            // InfoSettingsLabel
            // 
            this.InfoSettingsLabel.AutoSize = true;
            this.InfoSettingsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.InfoSettingsLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F);
            this.InfoSettingsLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.InfoSettingsLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.InfoSettingsLabel.Location = new System.Drawing.Point(0, 0);
            this.InfoSettingsLabel.Name = "InfoSettingsLabel";
            this.InfoSettingsLabel.Size = new System.Drawing.Size(373, 20);
            this.InfoSettingsLabel.TabIndex = 6;
            this.InfoSettingsLabel.Text = "Adjust these URLs to the PINGO Version you are using.";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(304, 132);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(113, 46);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Save changes";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.SaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.DiscardButton;
            this.ClientSize = new System.Drawing.Size(436, 188);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.InfoSettingsLabel);
            this.Controls.Add(this.DiscardButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.SocketURLText);
            this.Controls.Add(this.SiteURLText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SiteURLText;
        private System.Windows.Forms.TextBox SocketURLText;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button DiscardButton;
        private System.Windows.Forms.Label InfoSettingsLabel;
        private System.Windows.Forms.Button SaveButton;
    }
}