namespace Vorze_PlayerHelper
{
  partial class Form1
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.cbMonitoringEnabled = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.gbZPlayer = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.tbZplayerPort = new System.Windows.Forms.TextBox();
      this.tbZplayerIP = new System.Windows.Forms.TextBox();
      this.gbVorze = new System.Windows.Forms.GroupBox();
      this.label4 = new System.Windows.Forms.Label();
      this.lblVorzeDIR = new System.Windows.Forms.Label();
      this.btnBrowseVorzeDir = new System.Windows.Forms.Button();
      this.tbVorzeDir = new System.Windows.Forms.TextBox();
      this.gbLog = new System.Windows.Forms.GroupBox();
      this.lvLog = new System.Windows.Forms.ListView();
      this.cbMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.cbMinimizeToTray = new System.Windows.Forms.CheckBox();
      this.gbZPlayer.SuspendLayout();
      this.gbVorze.SuspendLayout();
      this.gbLog.SuspendLayout();
      this.SuspendLayout();
      // 
      // cbMonitoringEnabled
      // 
      this.cbMonitoringEnabled.AutoSize = true;
      this.cbMonitoringEnabled.Location = new System.Drawing.Point(17, 128);
      this.cbMonitoringEnabled.Name = "cbMonitoringEnabled";
      this.cbMonitoringEnabled.Size = new System.Drawing.Size(230, 17);
      this.cbMonitoringEnabled.TabIndex = 1;
      this.cbMonitoringEnabled.Text = "Enable monitoring for Zoom player activities\r\n";
      this.cbMonitoringEnabled.UseVisualStyleBackColor = true;
      this.cbMonitoringEnabled.CheckedChanged += new System.EventHandler(this.cbMonitoringEnabled_CheckedChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(8, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(17, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "IP";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(8, 60);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(26, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Port";
      // 
      // gbZPlayer
      // 
      this.gbZPlayer.Controls.Add(this.label3);
      this.gbZPlayer.Controls.Add(this.tbZplayerPort);
      this.gbZPlayer.Controls.Add(this.label2);
      this.gbZPlayer.Controls.Add(this.tbZplayerIP);
      this.gbZPlayer.Controls.Add(this.label1);
      this.gbZPlayer.Location = new System.Drawing.Point(17, 21);
      this.gbZPlayer.Name = "gbZPlayer";
      this.gbZPlayer.Size = new System.Drawing.Size(366, 90);
      this.gbZPlayer.TabIndex = 5;
      this.gbZPlayer.TabStop = false;
      this.gbZPlayer.Text = "Zoom player settings";
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(191, 30);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(144, 39);
      this.label3.TabIndex = 0;
      this.label3.Text = "Requires the \r\nZoom Player control API\r\nto be enabled";
      // 
      // tbZplayerPort
      // 
      this.tbZplayerPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vorze_PlayerHelper.Properties.Settings.Default, "ZplayerPORT", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tbZplayerPort.Location = new System.Drawing.Point(40, 57);
      this.tbZplayerPort.Name = "tbZplayerPort";
      this.tbZplayerPort.Size = new System.Drawing.Size(100, 20);
      this.tbZplayerPort.TabIndex = 2;
      this.tbZplayerPort.Text = global::Vorze_PlayerHelper.Properties.Settings.Default.ZplayerPORT;
      this.tbZplayerPort.Validating += new System.ComponentModel.CancelEventHandler(this.tbZplayerPort_Validating);
      // 
      // tbZplayerIP
      // 
      this.tbZplayerIP.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vorze_PlayerHelper.Properties.Settings.Default, "ZplayerIP", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tbZplayerIP.Location = new System.Drawing.Point(40, 27);
      this.tbZplayerIP.Name = "tbZplayerIP";
      this.tbZplayerIP.Size = new System.Drawing.Size(100, 20);
      this.tbZplayerIP.TabIndex = 0;
      this.tbZplayerIP.Text = global::Vorze_PlayerHelper.Properties.Settings.Default.ZplayerIP;
      this.tbZplayerIP.Validating += new System.ComponentModel.CancelEventHandler(this.tbZplayerIP_Validating);
      // 
      // gbVorze
      // 
      this.gbVorze.Controls.Add(this.label4);
      this.gbVorze.Controls.Add(this.lblVorzeDIR);
      this.gbVorze.Controls.Add(this.btnBrowseVorzeDir);
      this.gbVorze.Controls.Add(this.tbVorzeDir);
      this.gbVorze.Location = new System.Drawing.Point(389, 21);
      this.gbVorze.Name = "gbVorze";
      this.gbVorze.Size = new System.Drawing.Size(472, 130);
      this.gbVorze.TabIndex = 7;
      this.gbVorze.TabStop = false;
      this.gbVorze.Text = "Vorze settings";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(6, 57);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(400, 70);
      this.label4.TabIndex = 0;
      this.label4.Text = resources.GetString("label4.Text");
      // 
      // lblVorzeDIR
      // 
      this.lblVorzeDIR.AutoSize = true;
      this.lblVorzeDIR.Location = new System.Drawing.Point(6, 14);
      this.lblVorzeDIR.Name = "lblVorzeDIR";
      this.lblVorzeDIR.Size = new System.Drawing.Size(158, 13);
      this.lblVorzeDIR.TabIndex = 5;
      this.lblVorzeDIR.Text = "Vorze CSV / script root directory";
      // 
      // btnBrowseVorzeDir
      // 
      this.btnBrowseVorzeDir.Location = new System.Drawing.Point(412, 20);
      this.btnBrowseVorzeDir.Name = "btnBrowseVorzeDir";
      this.btnBrowseVorzeDir.Size = new System.Drawing.Size(54, 37);
      this.btnBrowseVorzeDir.TabIndex = 7;
      this.btnBrowseVorzeDir.Text = "Browse";
      this.btnBrowseVorzeDir.UseVisualStyleBackColor = true;
      this.btnBrowseVorzeDir.Click += new System.EventHandler(this.btnBrowseVorzeDir_Click);
      // 
      // tbVorzeDir
      // 
      this.tbVorzeDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.tbVorzeDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
      this.tbVorzeDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Vorze_PlayerHelper.Properties.Settings.Default, "VorzeRootDIR", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tbVorzeDir.Location = new System.Drawing.Point(6, 30);
      this.tbVorzeDir.Name = "tbVorzeDir";
      this.tbVorzeDir.Size = new System.Drawing.Size(400, 20);
      this.tbVorzeDir.TabIndex = 6;
      this.tbVorzeDir.Text = global::Vorze_PlayerHelper.Properties.Settings.Default.VorzeRootDIR;
      this.tbVorzeDir.Validating += new System.ComponentModel.CancelEventHandler(this.tbVorzeDir_Validating);
      // 
      // gbLog
      // 
      this.gbLog.Controls.Add(this.lvLog);
      this.gbLog.Location = new System.Drawing.Point(17, 157);
      this.gbLog.Name = "gbLog";
      this.gbLog.Size = new System.Drawing.Size(852, 217);
      this.gbLog.TabIndex = 8;
      this.gbLog.TabStop = false;
      this.gbLog.Text = "Log";
      // 
      // lvLog
      // 
      this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cbMessage});
      this.lvLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvLog.GridLines = true;
      this.lvLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.lvLog.Location = new System.Drawing.Point(3, 16);
      this.lvLog.Name = "lvLog";
      this.lvLog.Size = new System.Drawing.Size(846, 198);
      this.lvLog.TabIndex = 0;
      this.lvLog.UseCompatibleStateImageBehavior = false;
      this.lvLog.View = System.Windows.Forms.View.Details;
      // 
      // cbMessage
      // 
      this.cbMessage.Text = "Message";
      this.cbMessage.Width = 833;
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
      this.notifyIcon1.Text = "V - Helper";
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
      // 
      // cbMinimizeToTray
      // 
      this.cbMinimizeToTray.AutoSize = true;
      this.cbMinimizeToTray.Checked = global::Vorze_PlayerHelper.Properties.Settings.Default.minimizeToTray;
      this.cbMinimizeToTray.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Vorze_PlayerHelper.Properties.Settings.Default, "minimizeToTray", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.cbMinimizeToTray.Location = new System.Drawing.Point(765, 375);
      this.cbMinimizeToTray.Name = "cbMinimizeToTray";
      this.cbMinimizeToTray.Size = new System.Drawing.Size(98, 17);
      this.cbMinimizeToTray.TabIndex = 9;
      this.cbMinimizeToTray.Text = "Minimize to tray";
      this.cbMinimizeToTray.UseVisualStyleBackColor = true;
      this.cbMinimizeToTray.CheckedChanged += new System.EventHandler(this.cbMinimizeToTray_CheckedChanged);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(868, 392);
      this.Controls.Add(this.cbMinimizeToTray);
      this.Controls.Add(this.gbLog);
      this.Controls.Add(this.gbVorze);
      this.Controls.Add(this.gbZPlayer);
      this.Controls.Add(this.cbMonitoringEnabled);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.Text = "Vorze - Player helper  ( Version: 0.14 )";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.Resize += new System.EventHandler(this.Form1_Resize);
      this.gbZPlayer.ResumeLayout(false);
      this.gbZPlayer.PerformLayout();
      this.gbVorze.ResumeLayout(false);
      this.gbVorze.PerformLayout();
      this.gbLog.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbZplayerIP;
    private System.Windows.Forms.CheckBox cbMonitoringEnabled;
    private System.Windows.Forms.TextBox tbZplayerPort;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.GroupBox gbZPlayer;
    private System.Windows.Forms.TextBox tbVorzeDir;
    private System.Windows.Forms.GroupBox gbVorze;
    private System.Windows.Forms.Button btnBrowseVorzeDir;
    private System.Windows.Forms.GroupBox gbLog;
    private System.Windows.Forms.ListView lvLog;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.ColumnHeader cbMessage;
    private System.Windows.Forms.Label lblVorzeDIR;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private System.Windows.Forms.CheckBox cbMinimizeToTray;
  }
}

