using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace gExploreFTP
{
	/// <summary>
	/// The main form of the application
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		#region Member Variables

        private System.Windows.Forms.ListBox m_listBoxMessages;
        private gExploreFTP.FTP.FtpServer.FtpServer m_theFtpServer = null;

		#endregion
        private NotifyIcon notifyIcon1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem clearToolStripMenuItem;
        private IContainer components;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem controlToolStripMenuItem;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator3;
        private Panel panel1;
        private Button buttonHide;
        private ContextMenuStrip contextMenuStripTray;
        private ToolStripMenuItem restoreGExploreFTPToolStripMenuItem;
        private ToolStripMenuItem restartToolStripMenuItem;
		#region Construction

		public MainForm()
		{

			InitializeComponent();

            gExploreFTP.FTP.FtpServer.FtpServerMessageHandler.Message += new gExploreFTP.FTP.FtpServer.FtpServerMessageHandler.MessageEventHandler(MessageHandler_Message);
            m_theFtpServer = new gExploreFTP.FTP.FtpServer.FtpServer(new gExploreFTP.FTP.FileSystem.Interfaces.GoogleDocs.GoogleDocsFilesClassFactory());

            if (gExploreFTP.Common.Registry.getStart() == true)
            {
                StartServer();
            }

		}

        /// <summary>
        /// Determine if the beta has expired.
        /// </summary>
        /// <returns></returns>

        private void StartServer() 
        {

            // Read from settings.
            int port = gExploreFTP.Common.Registry.getPort();

            m_theFtpServer.Start(port);
            
            m_theFtpServer.ConnectionClosed += new gExploreFTP.FTP.FtpServer.FtpServer.ConnectionHandler(m_theFtpServer_ConnectionClosed);
            m_theFtpServer.NewConnection += new gExploreFTP.FTP.FtpServer.FtpServer.ConnectionHandler(m_theFtpServer_NewConnection);

            toolStripStatusLabel1.Text = "gExploreFTP is listening on port " + port.ToString() + ".";

            stopToolStripMenuItem.Enabled = true;
            startToolStripMenuItem.Enabled = false;
            restartToolStripMenuItem.Enabled = true;
               
        }

        private void StopServer()
        {

            m_theFtpServer.Stop();

            toolStripStatusLabel1.Text = "gExploreFTP is ready.";
            
            stopToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = true;
            restartToolStripMenuItem.Enabled = false;

        }

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.m_listBoxMessages = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreGExploreFTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonHide = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStripTray.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_listBoxMessages
            // 
            this.m_listBoxMessages.ContextMenuStrip = this.contextMenuStrip1;
            this.m_listBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listBoxMessages.IntegralHeight = false;
            this.m_listBoxMessages.Location = new System.Drawing.Point(0, 67);
            this.m_listBoxMessages.Name = "m_listBoxMessages";
            this.m_listBoxMessages.Size = new System.Drawing.Size(490, 201);
            this.m_listBoxMessages.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(102, 26);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "gExploreFTP is still running.";
            this.notifyIcon1.BalloonTipTitle = "gExploreFTP";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStripTray;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "gExploreFTP";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStripTray
            // 
            this.contextMenuStripTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreGExploreFTPToolStripMenuItem});
            this.contextMenuStripTray.Name = "contextMenuStripTray";
            this.contextMenuStripTray.Size = new System.Drawing.Size(172, 26);
            // 
            // restoreGExploreFTPToolStripMenuItem
            // 
            this.restoreGExploreFTPToolStripMenuItem.Name = "restoreGExploreFTPToolStripMenuItem";
            this.restoreGExploreFTPToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.restoreGExploreFTPToolStripMenuItem.Text = "Show gExploreFTP";
            this.restoreGExploreFTPToolStripMenuItem.Click += new System.EventHandler(this.restoreGExploreFTPToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.controlToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(490, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(125, 22);
            this.toolStripMenuItem2.Text = "Settings...";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(122, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.restartToolStripMenuItem});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.controlToolStripMenuItem.Text = "Control";
            this.controlToolStripMenuItem.Click += new System.EventHandler(this.controlToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Enabled = false;
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 268);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(490, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "gExploreFTP is ready.";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonHide);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(490, 43);
            this.panel1.TabIndex = 5;
            // 
            // buttonHide
            // 
            this.buttonHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHide.Location = new System.Drawing.Point(360, 12);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(118, 23);
            this.buttonHide.TabIndex = 6;
            this.buttonHide.Text = "Hide gExploreFTP";
            this.buttonHide.UseVisualStyleBackColor = true;
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(490, 290);
            this.Controls.Add(this.m_listBoxMessages);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(390, 251);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gExploreFTP";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStripTray.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#endregion

		#region Main

		[STAThread]
		static void Main() 
		{

            Application.Run(new MainForm());
			
		}

		#endregion

		#region Event Handlers

		private void m_menuItemExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            try
            {
                m_theFtpServer.Stop();
            }
            catch (Exception ex)
            {
                // It wasn't running.
            }
		}

		private void MessageHandler_Message(int nId, string sMessage)
		{
           
			m_listBoxMessages.BeginUpdate();

            if (sMessage.Contains("PASS") == false)
            {

                int nItem = m_listBoxMessages.Items.Add(string.Format("({0}) <{1}> {2}", nId, System.DateTime.Now, sMessage));

			    if (m_listBoxMessages.Items.Count > 5000)
			    {
				    m_listBoxMessages.Items.RemoveAt(0);
			    }

			    if (m_listBoxMessages.SelectedIndex < 0)
			    {
				    m_listBoxMessages.TopIndex = nItem;
			    }
			    else if (m_listBoxMessages.SelectedIndex == nItem - 1)
			    {
				    m_listBoxMessages.SelectedIndex = nItem;
			    }

            }

			m_listBoxMessages.EndUpdate();

		}

		private void m_theFtpServer_ConnectionClosed(int nId)
		{

		}

		private void m_theFtpServer_NewConnection(int nId)
		{

		}

		#endregion

        private void MainForm_Load(object sender, EventArgs e)
        {

            // TODO: Remove this and fix properly.
            Form.CheckForIllegalCrossThreadCalls = false;

        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutgExploreFTP about = new AboutgExploreFTP();
            about.ShowDialog();
            about.Dispose();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_listBoxMessages.Items.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartServer();
        }

        private void toolStripMenuItemCheckForUpdates_Click(object sender, EventArgs e)
        {

        }

        private void controlToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            Settings s = new Settings();
            s.ShowDialog();
            s.Dispose();

        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopServer();
            StartServer();
        }

        private void toolStripMenuItemHelpAndSupport_Click(object sender, EventArgs e)
        {

        }

        private void buttonHide_Click(object sender, EventArgs e)
        {            
            notifyIcon1.ShowBalloonTip(10000);
            notifyIcon1.Visible = true;
            this.Visible = false;
        }

        private void restoreGExploreFTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Visible = true;
        }

	}

}
