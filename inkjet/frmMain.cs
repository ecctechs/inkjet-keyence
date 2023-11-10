using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using inkjet.Class;
using inkjet.UserControls;
using static System.Net.Mime.MediaTypeNames;

namespace inkjet
{
    public partial class frmMain : Form
    {     
        System.Timers.Timer t;
        private static frmMain _instance;
        public static frmMain Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new frmMain();
                return _instance;
            }
        }

        public frmMain()
        {
            InitializeComponent();
        }
        
        public void addUserControl(UserControl userControl)
        {
            if (User.getUser() == "" || User.getUser() == null)
            {
                MessageBox.Show("Please Login first");
                btnOverview.Checked = true;
                ucLogin uc = new ucLogin();
                uc.Dock = DockStyle.Fill;
                panelContainer.Controls.Clear();
                panelContainer.Controls.Add(uc);
                uc.BringToFront();
            }
            else
            {
                _instance = this;
                userControl.Dock = DockStyle.Fill;
                panelContainer.Controls.Clear();
                panelContainer.Controls.Add(userControl);
                userControl.BringToFront();
                get_Userinfo();
            }
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            t.Stop();
            t.Start();
            ucOverview uc =  new ucOverview();
            addUserControl(uc);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            t = new System.Timers.Timer();
            t.Interval = 5000;
            t.Elapsed += OnTimeEvent;
            t.Stop();
            t.Start();

            _instance = this;
            get_Userinfo();
            ucLogin uc = new ucLogin();
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            uc.BringToFront();
        }
        private void get_Userinfo()
        {
            if (User.getUser() != "")
            {
                txtUserInfo.Text = User.getUser() + "<" + User.getUserRole() + ">";
            }
            else 
            {
                txtUserInfo.Text = "";
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (User.getUser() != "")
            {
                cbxMake.DroppedDown = true;
            }
        }

        private void cbxMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            User usr = new User();
            usr.clearUser();
            ucLogin uc = new ucLogin();
            frmMain.Instance.addUserControl(uc);
            txtUserInfo.Text = "";
        }

        private void btnCsv_Click(object sender, EventArgs e)
        {
            t.Stop();
            ucCSVmarking uc = new ucCSVmarking();
            addUserControl(uc);
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            //t.Stop();
            ucError uc = new ucError();
            addUserControl(uc);
        }

        private void btnDataLog_Click(object sender, EventArgs e)
        {
            //t.Stop();
            ucData uc = new ucData();
            addUserControl(uc);
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            t.Stop();
            t.Start();
            ucConnection uc = new ucConnection();
            addUserControl(uc);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (User.getUser() == "" || User.getUser() == null)
            {          
                MessageBox.Show("Please Login first");
            }
            else if(User.getUserRole() != "Admin")
            {
                MessageBox.Show("Admin Only");
            }
            else 
            {
                using (UserSetting frm_setting = new UserSetting())
                {
                    if (frm_setting.ShowDialog() == DialogResult.OK)
                    {
                        txtUserInfo.Text = User.getUser() + "<" + User.getUserRole() + ">";
                        Console.WriteLine(User.getUser());
                    }
                }
            }
        }

        private void OnTimeEvent(object sender, EventArgs e)
        {
            client.Program program = new client.Program();
            program.Execute_Client();
        }
    }
}
