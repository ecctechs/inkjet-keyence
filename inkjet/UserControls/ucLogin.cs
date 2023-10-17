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
        string path_user = "C:\\Users\\ADMIN\\Desktop\\test\\user.csv";
        public ucLogin()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
            using var reader = new StreamReader(path_user);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            List<User> _items = new List<User>();

            var records = csv.GetRecords<User>();

            foreach (var record in records)
            {
                _items.Add(new User { UserName = record.UserName, UserPassword = record.UserPassword, UserRole = record.UserRole, UserNameOp = record.UserNameOp, UserPasswordOp = record.UserPasswordOp });
            }

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
