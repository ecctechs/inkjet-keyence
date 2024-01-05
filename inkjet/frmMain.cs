using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using CsvHelper;
using inkjet.Class;
using inkjet.UserControls;
using static System.Net.Mime.MediaTypeNames;

namespace inkjet
{
    
    public partial class frmMain : Form
    {
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
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            chk_file();
            permisstion();
            TimerClient.Start_timer();
            TimerClient.aTimer.Stop();
            TimerClient.aTimer.Start();

            _instance = this;
            get_Userinfo();
            ucLogin uc = new ucLogin();
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            uc.BringToFront();
            
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            permisstion();
            TimerClient.aTimer.Stop();
            TimerClient.aTimer.Start();
            ucOverview uc = new ucOverview();
            addUserControl(uc);
        }

        private void btnCsv_Click(object sender, EventArgs e)
        {
            permisstion();
            TimerClient.aTimer.Stop();
            TimerClient.aTimer.Start();
            string gg2 = ucCSVmarking.inkjet_name;
            //Console.WriteLine("wWE+------->" + gg2);

            //if (gg2 == "" )
            //{
            //    Console.WriteLine("222222222222222222");
            //    ucCSVmarking uc = new ucCSVmarking();
            //    addUserControl(uc);
            //}
            //else
            //{
                Console.WriteLine("111111111111111111");
                ucCSVmarking uc = ucCSVmarking.Instance;
                addUserControl(uc);
            //}
}

        private void btnError_Click(object sender, EventArgs e)
        {
            permisstion();
            TimerClient.aTimer.Stop();
            TimerClient.aTimer.Start();
            ucError uc = new ucError();
            addUserControl(uc);
        }

        private void btnDataLog_Click(object sender, EventArgs e)
        {
            permisstion();
            TimerClient.aTimer.Stop();
            TimerClient.aTimer.Start();
            ucData uc = new ucData();
            addUserControl(uc);
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            permisstion();
            TimerClient.aTimer.Stop();
            TimerClient.aTimer.Start();
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
            //frmMain.Instance.addUserControl(uc);
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            uc.BringToFront();
            txtUserInfo.Text = "";
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
                permisstion();
            }
        }

        public void permisstion()
        {
            if (User.getUserRole() == "Operator")
            {
                
                pictureBox3.Visible = false;
            }
            else
            {
                pictureBox3.Visible = true;
            }
        }

        public void chk_file()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            List<string> path_file  = new List<string>(new string[] {
                path + "\\Inkjet\\Data\\user.csv",
                path + "\\Inkjet\\Data\\shift.csv",
                path + "\\Inkjet\\Data\\list-error.csv",
                path + "\\Inkjet\\Data\\inkjet.csv",
                path + "\\Inkjet\\Data\\error.csv",
                path + "\\Inkjet\\Data\\email.csv",
                path + "\\Inkjet\\Data\\data_log.csv",
            });

            for (int i = 0; i < path_file.Count; i++)
            {
                try
                {
                    using (var reader = new StreamReader(path_file[i]));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Not Found File Name" + path_file[i]);
                    this.Close();                   
                }
            }
          
        }
    }
}
