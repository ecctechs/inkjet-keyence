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

namespace inkjet.UserControls
{
    public partial class ucEmail : UserControl
    {
        public ucEmail()
        {
            InitializeComponent();
        }

        private void ucEmail_Load(object sender, EventArgs e)
        {
            metroGrid1.DefaultCellStyle.SelectionBackColor = Color.DarkOrange;
            metroGrid1.DefaultCellStyle.SelectionForeColor = Color.Black;
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
                var chk_duplicate = Email.Duplicate_Email(input_email);
                Console.WriteLine(chk_duplicate);
                if (chk_duplicate == true)
                {
                    list_email.Add(new Email { EmailNo = Email.running_id, EmailName = input_email });
                    Email.Add_Email(list_email);
                }
                else
                {
                    MessageBox.Show(this, "Data is Already Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(this, "Please Fill All Data", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            get_email();
            metroGrid1.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[0].Value);

            if (MessageBox.Show(this, "Yes/Cancel", "Delete Data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                List<Email> records = Email.Delete_Email(id);
                Email.Update_Email(records);
            }

            get_email();
            metroGrid1.Show();
        }
    }
}
