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

namespace inkjet.UserControls
{
    public partial class ucLogin : UserControl
    {
        public ucLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            chk_login();
        }

        private void chk_login()
        {

            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("กรูณากรอกข้อมูล");
            }
            else 
            {  
                get_user();
            }
        }

        private void get_user()
        {

            List<User> _items = User.ListUser();

            for (int i = 0; i < _items.Count; i++)
            {
                if (txtUsername.Text == _items[i].UserName && txtPassword.Text == _items[i].UserPassword || txtUsername.Text == _items[i].UserNameOp && txtPassword.Text == _items[i].UserPasswordOp)
                {
                    User usr = new User();
                    usr.setUser(this.txtUsername.Text);
                    usr.setUserPassword(this.txtPassword.Text);

                    if (txtUsername.Text == _items[i].UserNameOp && txtPassword.Text == _items[i].UserPasswordOp)
                    {
                        usr.setUserRole("Operator");
                    }
                    else
                    {
                        usr.setUserRole(_items[i].UserRole);
                    }
                    ucOverview uc = new ucOverview();
                    frmMain.Instance.addUserControl(uc);
                }
            }
        }
    }
}
