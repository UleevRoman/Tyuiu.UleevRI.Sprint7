﻿
namespace Tyuiu.UleevRI.Sprint7.Project.V9
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.pictureBoxInformation_URI = new System.Windows.Forms.PictureBox();
            this.textBoxInformation_URI = new System.Windows.Forms.TextBox();
            this.buttonOK_URI = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInformation_URI)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxInformation_URI
            // 
            this.pictureBoxInformation_URI.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxInformation_URI.Image")));
            this.pictureBoxInformation_URI.Location = new System.Drawing.Point(12, 34);
            this.pictureBoxInformation_URI.Name = "pictureBoxInformation_URI";
            this.pictureBoxInformation_URI.Size = new System.Drawing.Size(331, 317);
            this.pictureBoxInformation_URI.TabIndex = 0;
            this.pictureBoxInformation_URI.TabStop = false;
            // 
            // textBoxInformation_URI
            // 
            this.textBoxInformation_URI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInformation_URI.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxInformation_URI.Location = new System.Drawing.Point(349, 34);
            this.textBoxInformation_URI.Multiline = true;
            this.textBoxInformation_URI.Name = "textBoxInformation_URI";
            this.textBoxInformation_URI.ReadOnly = true;
            this.textBoxInformation_URI.Size = new System.Drawing.Size(437, 317);
            this.textBoxInformation_URI.TabIndex = 1;
            this.textBoxInformation_URI.TabStop = false;
            this.textBoxInformation_URI.Text = resources.GetString("textBoxInformation_URI.Text");
            // 
            // buttonOK_URI
            // 
            this.buttonOK_URI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK_URI.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonOK_URI.Location = new System.Drawing.Point(711, 360);
            this.buttonOK_URI.Name = "buttonOK_URI";
            this.buttonOK_URI.Size = new System.Drawing.Size(87, 40);
            this.buttonOK_URI.TabIndex = 2;
            this.buttonOK_URI.Text = "OK";
            this.buttonOK_URI.UseVisualStyleBackColor = false;
            this.buttonOK_URI.Click += new System.EventHandler(this.buttonOK_URI_Click);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(810, 412);
            this.Controls.Add(this.buttonOK_URI);
            this.Controls.Add(this.textBoxInformation_URI);
            this.Controls.Add(this.pictureBoxInformation_URI);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinimumSize = new System.Drawing.Size(828, 459);
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "О прорамме";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInformation_URI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxInformation_URI;
        private System.Windows.Forms.TextBox textBoxInformation_URI;
        private System.Windows.Forms.Button buttonOK_URI;
    }
}