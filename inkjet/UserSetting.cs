using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using inkjet.Class;
using inkjet.UserControls;

namespace inkjet
{
    public partial class UserSetting : Form
    {
        static UserSetting _instance;
        string path_user = "C:\\Users\\ADMIN\\Desktop\\test\\user.csv";
        public static UserSetting Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserSetting();
                return _instance;
            }
        }

        public void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelSetting.Controls.Clear();
            panelSetting.Controls.Add(userControl);
            userControl.Dock = DockStyle.Fill;
            userControl.BringToFront();
        }

        public UserSetting()
        {
            InitializeComponent();
        }

        private void CloseSetting_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            _instance = this;
            ucEmail uc = new ucEmail();
            addUserControl(uc);
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            _instance = this;
            ucShift uc = new ucShift();
            addUserControl(uc);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            _instance = this;
            ucUser uc = new ucUser();
            addUserControl(uc);
        }    
        
        private void UserSetting_Load(object sender, EventArgs e)
        {
            //get_user_operator();
            _instance = this;
            ucEmail uc = new ucEmail();
            addUserControl(uc);
        }
    }
}
