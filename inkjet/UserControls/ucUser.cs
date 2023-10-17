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
    public partial class ucUser : UserControl
    {
        string path_user = "C:\\Users\\ADMIN\\Desktop\\test\\user.csv";

        public ucUser()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UserSetting.Instance.Close();
        }

        private void ucUser_Load(object sender, EventArgs e)
        {
            txtUsernameAdmin.Text = User.getUser();
            txtPasswordAdmin.Text = User.getUserPassword();
            txtUsernameOp.Text = User.getUserNameOp();
            txtPasswordOp.Text = User.getUserPasswordOp();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUsernameAdmin.Text.Trim() == "" || txtPasswordAdmin.Text.Trim() == "")
            {
                MessageBox.Show(this, "Please fill Username and Password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var records = new List<User>();
                using (var reader = new StreamReader(path_user))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        var record = new User
                        {
                            UserId = csv.GetField<int>("User ID"),
                            UserName = csv.GetField("User Name"),
                            UserPassword = csv.GetField("User Password"),
                            UserRole = csv.GetField("User Role"),
                            UserNameOp = csv.GetField("User Name Operator"),
                            UserPasswordOp = csv.GetField("User Password Operator"),
                        };
                        Console.WriteLine(record.UserId);

                        if (User.getUser() == record.UserName)
                        {
                            record.UserName = txtUsernameAdmin.Text;
                            record.UserPassword = txtPasswordAdmin.Text;
                            record.UserNameOp = txtUsernameOp.Text;
                            record.UserPasswordOp = txtPasswordOp.Text;
                        }
                        records.Add(record);
                    }

                }
                using (var writer = new StreamWriter(path_user))
                using (var csv2 = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv2.WriteRecords(records);
                }
                MessageBox.Show(this, "Username and Password Update", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                User usr = new User();
                usr.setUser(this.txtUsernameAdmin.Text);
                usr.setUserPassword(this.txtPasswordAdmin.Text);
                this.ParentForm.DialogResult = DialogResult.OK;
            }
        }
    }
}
