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
using CsvHelper.Configuration;
using inkjet.Class;
using System.Text.RegularExpressions;
using static inkjet.Class.Email;

namespace inkjet.UserControls
{
    public partial class ucEmail : UserControl
    {
        static Regex validate_emailaddress = email_validation();
        public ucEmail()
        {
            InitializeComponent();
        }

        private static Regex email_validation()
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }

        private void ucEmail_Load(object sender, EventArgs e)
        {
            metroGrid1.DefaultCellStyle.SelectionBackColor = Color.DarkOrange;
            metroGrid1.DefaultCellStyle.SelectionForeColor = Color.Black;
            var combobox = (DataGridViewComboBoxColumn)metroGrid1.Columns["ErrorID"];

            combobox.DisplayMember = "Name";
            combobox.ValueMember = "ID";

            combobox.DataSource = GetAlarms();

            get_email();

    
        }

        public void get_email()
        {
            List<Email> records = Email.ListEmail();
            emailBindingSource.DataSource = records;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var input_email = txtEmail.Text;
            List<Email> list_email = new List<Email>();


            if (input_email.Trim() != "")
            {
                if (validate_emailaddress.IsMatch(txtEmail.Text) != true)
                {
                    MessageBox.Show("Invalid Email Address!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEmail.Focus();            
                }
                else
                {
                    var chk_duplicate = Email.Duplicate_Email(input_email);
                    Console.WriteLine(chk_duplicate);
                    if (chk_duplicate == true)
                    {
                        list_email.Add(new Email { EmailNo = Email.running_id, EmailName = input_email  });
                        Email.Add_Email(list_email);
                    }
                    else
                    {
                        MessageBox.Show(this, "Data is Already Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Email cannot be blank", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            get_email();
            metroGrid1.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[0].Value);

            if (MessageBox.Show(this, "Do you confirm to Delete the Email ?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                List<Email> records = Email.Delete_Email(id);
                Email.Update_Email(records);
            }

            get_email();
            metroGrid1.Show();
        }



        private List<alarm> GetAlarms() 
        {
            return new List<alarm>
            {
               new alarm{ Name = "All" , ID = 1},
               new alarm{ Name = "Error" , ID = 2},
               new alarm{ Name = "Warning" , ID = 3},
            };           
        }

        private void metroGrid1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            List<Email> record = emailBindingSource.DataSource as List<Email>;
            Email.Update_Email(record);
        }
    }
}
