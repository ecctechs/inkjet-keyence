namespace inkjet
{
    partial class frmMain
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.cbxMake = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtUserInfo = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnConnection = new Guna.UI2.WinForms.Guna2Button();
            this.btnDataLog = new Guna.UI2.WinForms.Guna2Button();
            this.btnError = new Guna.UI2.WinForms.Guna2Button();
            this.btnCsv = new Guna.UI2.WinForms.Guna2Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnOverview = new Guna.UI2.WinForms.Guna2Button();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.panelContainer = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.btnConnection);
            this.panel1.Controls.Add(this.btnDataLog);
            this.panel1.Controls.Add(this.btnError);
            this.panel1.Controls.Add(this.btnCsv);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.btnOverview);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1902, 99);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox5);
            this.panel2.Controls.Add(this.cbxMake);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1704, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(98, 99);
            this.panel2.TabIndex = 0;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox5.Image = global::inkjet.Properties.Resources.รูปภาพ4;
            this.pictureBox5.Location = new System.Drawing.Point(0, 0);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(98, 99);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // cbxMake
            // 
            this.cbxMake.BackColor = System.Drawing.Color.Transparent;
            this.cbxMake.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxMake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMake.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbxMake.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbxMake.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbxMake.ForeColor = System.Drawing.Color.IndianRed;
            this.cbxMake.ItemHeight = 30;
            this.cbxMake.Items.AddRange(new object[] {
            "Logout"});
            this.cbxMake.Location = new System.Drawing.Point(-24, 63);
            this.cbxMake.Name = "cbxMake";
            this.cbxMake.Size = new System.Drawing.Size(122, 36);
            this.cbxMake.TabIndex = 1;
            this.cbxMake.Visible = false;
            this.cbxMake.SelectedIndexChanged += new System.EventHandler(this.cbxMake_SelectedIndexChanged);
            // 
            // txtUserInfo
            // 
            this.txtUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserInfo.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserInfo.Location = new System.Drawing.Point(0, 0);
            this.txtUserInfo.Name = "txtUserInfo";
            this.txtUserInfo.Size = new System.Drawing.Size(181, 99);
            this.txtUserInfo.TabIndex = 9;
            this.txtUserInfo.Text = "Who Login";
            this.txtUserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox3.Image = global::inkjet.Properties.Resources.รูปภาพ3;
            this.pictureBox3.Location = new System.Drawing.Point(1802, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 99);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // btnConnection
            // 
            this.btnConnection.BorderColor = System.Drawing.Color.DimGray;
            this.btnConnection.BorderThickness = 5;
            this.btnConnection.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnConnection.CheckedState.BorderColor = System.Drawing.Color.Maroon;
            this.btnConnection.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.btnConnection.CheckedState.FillColor = System.Drawing.Color.Red;
            this.btnConnection.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnConnection.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnConnection.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnConnection.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnConnection.FillColor = System.Drawing.Color.Silver;
            this.btnConnection.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnection.ForeColor = System.Drawing.Color.White;
            this.btnConnection.Location = new System.Drawing.Point(733, 12);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(162, 79);
            this.btnConnection.TabIndex = 6;
            this.btnConnection.Text = "Connection";
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // btnDataLog
            // 
            this.btnDataLog.BorderColor = System.Drawing.Color.DimGray;
            this.btnDataLog.BorderThickness = 5;
            this.btnDataLog.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnDataLog.CheckedState.BorderColor = System.Drawing.Color.Maroon;
            this.btnDataLog.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.btnDataLog.CheckedState.FillColor = System.Drawing.Color.Red;
            this.btnDataLog.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDataLog.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDataLog.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDataLog.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDataLog.FillColor = System.Drawing.Color.Silver;
            this.btnDataLog.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataLog.ForeColor = System.Drawing.Color.White;
            this.btnDataLog.Location = new System.Drawing.Point(565, 12);
            this.btnDataLog.Name = "btnDataLog";
            this.btnDataLog.Size = new System.Drawing.Size(150, 79);
            this.btnDataLog.TabIndex = 5;
            this.btnDataLog.Text = "Data Log";
            this.btnDataLog.Click += new System.EventHandler(this.btnDataLog_Click);
            // 
            // btnError
            // 
            this.btnError.BorderColor = System.Drawing.Color.DimGray;
            this.btnError.BorderThickness = 5;
            this.btnError.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnError.CheckedState.BorderColor = System.Drawing.Color.Maroon;
            this.btnError.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.btnError.CheckedState.FillColor = System.Drawing.Color.Red;
            this.btnError.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnError.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnError.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnError.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnError.FillColor = System.Drawing.Color.Silver;
            this.btnError.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnError.ForeColor = System.Drawing.Color.White;
            this.btnError.Location = new System.Drawing.Point(395, 12);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(152, 79);
            this.btnError.TabIndex = 4;
            this.btnError.Text = "Error History";
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // btnCsv
            // 
            this.btnCsv.BorderColor = System.Drawing.Color.DimGray;
            this.btnCsv.BorderThickness = 5;
            this.btnCsv.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnCsv.CheckedState.BorderColor = System.Drawing.Color.Maroon;
            this.btnCsv.CheckedState.CustomBorderColor = System.Drawing.Color.Red;
            this.btnCsv.CheckedState.FillColor = System.Drawing.Color.Red;
            this.btnCsv.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCsv.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCsv.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCsv.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCsv.FillColor = System.Drawing.Color.Silver;
            this.btnCsv.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCsv.ForeColor = System.Drawing.Color.White;
            this.btnCsv.Location = new System.Drawing.Point(231, 12);
            this.btnCsv.Name = "btnCsv";
            this.btnCsv.Size = new System.Drawing.Size(147, 79);
            this.btnCsv.TabIndex = 3;
            this.btnCsv.Text = "Csv Marking";
            this.btnCsv.Click += new System.EventHandler(this.btnCsv_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::inkjet.Properties.Resources.รูปภาพ2;
            this.pictureBox2.Location = new System.Drawing.Point(0, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(60, 79);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // btnOverview
            // 
            this.btnOverview.BorderColor = System.Drawing.Color.DimGray;
            this.btnOverview.BorderThickness = 5;
            this.btnOverview.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnOverview.Checked = true;
            this.btnOverview.CheckedState.BorderColor = System.Drawing.Color.Maroon;
            this.btnOverview.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnOverview.CheckedState.FillColor = System.Drawing.Color.Red;
            this.btnOverview.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnOverview.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnOverview.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnOverview.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnOverview.FillColor = System.Drawing.Color.Silver;
            this.btnOverview.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOverview.ForeColor = System.Drawing.Color.White;
            this.btnOverview.Location = new System.Drawing.Point(66, 12);
            this.btnOverview.Name = "btnOverview";
            this.btnOverview.Size = new System.Drawing.Size(152, 79);
            this.btnOverview.TabIndex = 0;
            this.btnOverview.Text = "Overview";
            this.btnOverview.Click += new System.EventHandler(this.btnOverview_Click);
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.panel1;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 149);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1902, 884);
            this.panelContainer.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::inkjet.Properties.Resources.รูปภาพ1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 99);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1902, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtUserInfo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1523, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(181, 99);
            this.panel3.TabIndex = 11;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Guna.UI2.WinForms.Guna2Button btnOverview;
        private Guna.UI2.WinForms.Guna2Button btnConnection;
        private Guna.UI2.WinForms.Guna2Button btnDataLog;
        private Guna.UI2.WinForms.Guna2Button btnError;
        private Guna.UI2.WinForms.Guna2Button btnCsv;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label txtUserInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private Guna.UI2.WinForms.Guna2ComboBox cbxMake;
        private System.Windows.Forms.Panel panel3;
    }
}

