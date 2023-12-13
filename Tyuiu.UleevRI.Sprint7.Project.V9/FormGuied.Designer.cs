
namespace Tyuiu.UleevRI.Sprint7.Project.V9
{
    partial class FormGuied
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
            this.panelHelp_URI = new System.Windows.Forms.Panel();
            this.MenuStrip_URI = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp_URI = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new System.Windows.Forms.Panel();
            this.panelActions_URI = new System.Windows.Forms.Panel();
            this.textBoxGueid_URI = new System.Windows.Forms.TextBox();
            this.toolStripMenuItemBack_URI = new System.Windows.Forms.ToolStripMenuItem();
            this.panelHelp_URI.SuspendLayout();
            this.MenuStrip_URI.SuspendLayout();
            this.panelActions_URI.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHelp_URI
            // 
            this.panelHelp_URI.Controls.Add(this.MenuStrip_URI);
            this.panelHelp_URI.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHelp_URI.Location = new System.Drawing.Point(0, 0);
            this.panelHelp_URI.Name = "panelHelp_URI";
            this.panelHelp_URI.Size = new System.Drawing.Size(1219, 30);
            this.panelHelp_URI.TabIndex = 0;
            // 
            // MenuStrip_URI
            // 
            this.MenuStrip_URI.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip_URI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem,
            this.toolStripMenuItemBack_URI,
            this.toolStripMenuItemHelp_URI});
            this.MenuStrip_URI.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip_URI.Name = "MenuStrip_URI";
            this.MenuStrip_URI.Size = new System.Drawing.Size(1219, 28);
            this.MenuStrip_URI.TabIndex = 10;
            this.MenuStrip_URI.Text = "menuStrip1";
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(14, 24);
            // 
            // toolStripMenuItemHelp_URI
            // 
            this.toolStripMenuItemHelp_URI.Name = "toolStripMenuItemHelp_URI";
            this.toolStripMenuItemHelp_URI.Size = new System.Drawing.Size(118, 24);
            this.toolStripMenuItemHelp_URI.Text = "О программе";
            this.toolStripMenuItemHelp_URI.Click += new System.EventHandler(this.toolStripMenuItemHelp_URI_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(0, 645);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1219, 45);
            this.panel.TabIndex = 4;
            // 
            // panelActions_URI
            // 
            this.panelActions_URI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelActions_URI.Controls.Add(this.textBoxGueid_URI);
            this.panelActions_URI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelActions_URI.Location = new System.Drawing.Point(0, 30);
            this.panelActions_URI.Name = "panelActions_URI";
            this.panelActions_URI.Size = new System.Drawing.Size(1219, 615);
            this.panelActions_URI.TabIndex = 5;
            // 
            // textBoxGueid_URI
            // 
            this.textBoxGueid_URI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxGueid_URI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxGueid_URI.Location = new System.Drawing.Point(0, 0);
            this.textBoxGueid_URI.Multiline = true;
            this.textBoxGueid_URI.Name = "textBoxGueid_URI";
            this.textBoxGueid_URI.ReadOnly = true;
            this.textBoxGueid_URI.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxGueid_URI.Size = new System.Drawing.Size(1219, 615);
            this.textBoxGueid_URI.TabIndex = 0;
            this.textBoxGueid_URI.Text = "Текст будет позже!!!";
            // 
            // toolStripMenuItemBack_URI
            // 
            this.toolStripMenuItemBack_URI.Name = "toolStripMenuItemBack_URI";
            this.toolStripMenuItemBack_URI.Size = new System.Drawing.Size(65, 24);
            this.toolStripMenuItemBack_URI.Text = "Назад";
            // 
            // FormGuied
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 690);
            this.Controls.Add(this.panelActions_URI);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.panelHelp_URI);
            this.Name = "FormGuied";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Краткое руководство";
            this.panelHelp_URI.ResumeLayout(false);
            this.panelHelp_URI.PerformLayout();
            this.MenuStrip_URI.ResumeLayout(false);
            this.MenuStrip_URI.PerformLayout();
            this.panelActions_URI.ResumeLayout(false);
            this.panelActions_URI.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHelp_URI;
        private System.Windows.Forms.MenuStrip MenuStrip_URI;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp_URI;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Panel panelActions_URI;
        private System.Windows.Forms.TextBox textBoxGueid_URI;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBack_URI;
    }
}