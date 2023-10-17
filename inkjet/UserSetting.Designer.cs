namespace inkjet
{
    partial class UserSetting
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.CloseSetting = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUser = new Guna.UI2.WinForms.Guna2Button();
            this.btnShift = new Guna.UI2.WinForms.Guna2Button();
            this.btnEmail = new Guna.UI2.WinForms.Guna2Button();
            this.panelSetting = new System.Windows.Forms.Panel();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloseSetting)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.guna2HtmlLabel1);
            this.panel1.Controls.Add(this.CloseSetting);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 72);
            this.panel1.TabIndex = 0;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(20, 19);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(90, 34);
            this.guna2HtmlLabel1.TabIndex = 1;
            this.guna2HtmlLabel1.Text = "Setting";
            // 
            // CloseSetting
            // 
            this.CloseSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseSetting.Image = global::inkjet.Properties.Resources.cancel;
            this.CloseSetting.Location = new System.Drawing.Point(592, 19);
            this.CloseSetting.Name = "CloseSetting";
            this.CloseSetting.Size = new System.Drawing.Size(55, 37);
            this.CloseSetting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CloseSetting.TabIndex = 0;
            this.CloseSetting.TabStop = false;
            this.CloseSetting.Click += new System.EventHandler(this.CloseSetting_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnUser);
            this.panel2.Controls.Add(this.btnShift);
            this.panel2.Controls.Add(this.btnEmail);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 72);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(189, 435);
            this.panel2.TabIndex = 1;
            // 
            // btnUser
            // 
            this.btnUser.BorderColor = System.Drawing.Color.DimGray;
            this.btnUser.BorderThickness = 5;
            this.btnUser.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnUser.CheckedState.BorderColor = System.Drawing.Color.Maroon;
            this.btnUser.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.btnUser.CheckedState.FillColor = System.Drawing.Color.Red;
            this.btnUser.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnUser.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnUser.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnUser.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnUser.FillColor = System.Drawing.Color.Silver;
            this.btnUser.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUser.ForeColor = System.Drawing.Color.White;
            this.btnUser.Location = new System.Drawing.Point(4, 161);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(177, 65);
            this.btnUser.TabIndex = 1;
            this.btnUser.Text = "User";
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnShift
            // 
            this.btnShift.BackColor = System.Drawing.Color.Gray;
            this.btnShift.BorderColor = System.Drawing.Color.DimGray;
            this.btnShift.BorderThickness = 5;
            this.btnShift.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnShift.CheckedState.BorderColor = System.Drawing.Color.Maroon;
            this.btnShift.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.btnShift.CheckedState.FillColor = System.Drawing.Color.Red;
            this.btnShift.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnShift.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnShift.DisabledState.FillColor = System.Drawing.Color.Silver;
            this.btnShift.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnShift.FillColor = System.Drawing.Color.Silver;
            this.btnShift.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShift.ForeColor = System.Drawing.Color.White;
            this.btnShift.Location = new System.Drawing.Point(4, 88);
            this.btnShift.Name = "btnShift";
            this.btnShift.Size = new System.Drawing.Size(177, 66);
            this.btnShift.TabIndex = 1;
            this.btnShift.Text = "Shift Setting";
            this.btnShift.Click += new System.EventHandler(this.btnShift_Click);
            // 
            // btnEmail
            // 
            this.btnEmail.BorderColor = System.Drawing.Color.DimGray;
            this.btnEmail.BorderThickness = 5;
            this.btnEmail.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnEmail.Checked = true;
            this.btnEmail.CheckedState.BorderColor = System.Drawing.Color.Maroon;
            this.btnEmail.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.btnEmail.CheckedState.FillColor = System.Drawing.Color.Red;
            this.btnEmail.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEmail.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEmail.FillColor = System.Drawing.Color.Silver;
            this.btnEmail.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmail.ForeColor = System.Drawing.Color.White;
            this.btnEmail.Location = new System.Drawing.Point(3, 7);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(178, 75);
            this.btnEmail.TabIndex = 0;
            this.btnEmail.Text = "Email Alert";
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // panelSetting
            // 
            this.panelSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSetting.Location = new System.Drawing.Point(189, 72);
            this.panelSetting.Margin = new System.Windows.Forms.Padding(4);
            this.panelSetting.Name = "panelSetting";
            this.panelSetting.Size = new System.Drawing.Size(472, 435);
            this.panelSetting.TabIndex = 2;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.panel1;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // UserSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(661, 507);
            this.Controls.Add(this.panelSetting);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserSetting";
            this.Load += new System.EventHandler(this.UserSetting_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CloseSetting)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelSetting;
        private Guna.UI2.WinForms.Guna2Button btnEmail;
        private Guna.UI2.WinForms.Guna2Button btnUser;
        private Guna.UI2.WinForms.Guna2Button btnShift;
        private System.Windows.Forms.PictureBox CloseSetting;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
    }
}