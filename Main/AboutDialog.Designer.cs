namespace SpotlightToDesktop
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.installContextMenuButton = new System.Windows.Forms.Button();
            this.installScheduledTaskButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(16, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(746, 72);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::SpotlightToDesktop.Properties.Resources.About;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(778, 390);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // installContextMenuButton
            // 
            this.installContextMenuButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.installContextMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.installContextMenuButton.Location = new System.Drawing.Point(0, 0);
            this.installContextMenuButton.Margin = new System.Windows.Forms.Padding(0);
            this.installContextMenuButton.Name = "installContextMenuButton";
            this.installContextMenuButton.Size = new System.Drawing.Size(370, 80);
            this.installContextMenuButton.TabIndex = 1;
            this.installContextMenuButton.Text = "Install Desktop\r\nContext Menu Item";
            this.installContextMenuButton.UseVisualStyleBackColor = true;
            this.installContextMenuButton.Click += new System.EventHandler(this.OnInstallContextMenu);
            // 
            // installScheduledTaskButton
            // 
            this.installScheduledTaskButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.installScheduledTaskButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.installScheduledTaskButton.Location = new System.Drawing.Point(384, 0);
            this.installScheduledTaskButton.Margin = new System.Windows.Forms.Padding(0);
            this.installScheduledTaskButton.Name = "installScheduledTaskButton";
            this.installScheduledTaskButton.Size = new System.Drawing.Size(370, 80);
            this.installScheduledTaskButton.TabIndex = 2;
            this.installScheduledTaskButton.Text = "Install Daily\r\nScheduled Task";
            this.installScheduledTaskButton.UseVisualStyleBackColor = true;
            this.installScheduledTaskButton.Click += new System.EventHandler(this.OnInstallScheduledTask);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.installContextMenuButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.installScheduledTaskButton, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 482);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(754, 80);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(778, 574);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AboutDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spotlight To Desktop";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button installContextMenuButton;
        private System.Windows.Forms.Button installScheduledTaskButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}