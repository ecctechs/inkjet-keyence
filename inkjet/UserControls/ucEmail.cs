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
            get_email();
        }

        public void get_email()
        {
            using (var reader = new StreamReader("C:\\Users\\ADMIN\\Desktop\\test\\email.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Email>();
                emailBindingSource.DataSource = records;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //var running_id = metroGrid1.Rows.Count;
            var input_email = txtEmail.Text.Trim();
            var chk_duplicate = false;

            if (input_email != "")
            {
                List<Email> records;

                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\email.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<Email>().ToList();

                    if (records.Count == 0)
                    {
                        chk_duplicate = true;
                    }

                    for (int i = 0; i < records.Count; i++)
                    {
         
                        if (records[i].EmailName != input_email)
                        {
                            chk_duplicate = true;
                            continue;
                        }
                        else
                        {
                            chk_duplicate = false;
                            break;
                        }
                    }


                }
                Console.WriteLine(records.Count);
                int running_id;
                if (records.Count > 0)
                {
                    var lastItem = records.LastOrDefault();
                    running_id = lastItem.EmailNo + 1;
                }
                else
                    running_id = 1;
                {

                };

                if (chk_duplicate == true)
                {
                    var records_add = new List<Email>
                    {
                    new Email { EmailNo = running_id, EmailName = input_email },
                     };
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        // Don't write the header again.
                        HasHeaderRecord = false,
                    };
                    using (var stream = File.Open("C:\\Users\\ADMIN\\Desktop\\test\\email.csv", FileMode.Append))
                    using (var writer = new StreamWriter(stream))
                    using (var csv = new CsvWriter(writer, config))
                    {
                        csv.WriteRecords(records_add);
                    }


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
                List<Email> records;

                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\email.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<Email>().ToList();
                    int row = metroGrid1.RowCount;
                    for (int i = 0; i < records.Count; ++i)
                    {
                        if (records[i].EmailNo == id)
                        {
                            records.RemoveAt(i);
                        }
                    }
                }

                using (var writer = new StreamWriter(@"C:\Users\ADMIN\Desktop\test\email.csv"))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(records);
                }
            }
            get_email();
            metroGrid1.Show();
        }
    }
}
