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
    public partial class ucShift : UserControl
    {
        public ucShift()
        {
            InitializeComponent();
        }

        private void ucShift_Load(object sender, EventArgs e)
        {
            get_shift();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[0].Value);

            if (MessageBox.Show(this, "Yes/Cancel", "Delete Data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                List<Shift> records;

                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\shift.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<Shift>().ToList();

                    for (int i = 0; i < records.Count; ++i)
                    {
                        if (records[i].ShiftNo == id)
                        {
                            records.RemoveAt(i);
                        }
                    }
                }

                using (var writer = new StreamWriter(@"C:\Users\ADMIN\Desktop\test\shift.csv"))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(records);
                }
            }
            get_shift();
            metroGrid1.Show();
        }

        public void get_shift()
        {
            using (var reader = new StreamReader("C:\\Users\\ADMIN\\Desktop\\test\\shift.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {

                var records = csv.GetRecords<Shift>().ToList();
                //records.Insert(0, new Shift() { ShiftNo = 0, ShiftName = "All" });
                shiftBindingSource.DataSource = records;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //var running_id = metroGrid1.Rows.Count;
            var input_shift_name = txtShiftName.Text.Trim();
            var input_shift_time = txtShiftTime.Text.Trim();
            var chk_duplicate = false;


            if (input_shift_name != "" && input_shift_time != "")
            {
                List<Shift> records;

                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\shift.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<Shift>().ToList();


                    if (records.Count == 0)
                    {
                        chk_duplicate = true;
                    }

                    for (int i = 0; i < records.Count; ++i)
                    {
                        if (records[i].ShiftName != input_shift_name)
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

                int running_id;
                if (records.Count > 0)
                {
                    var lastItem = records.LastOrDefault();
                    running_id = lastItem.ShiftNo + 1;
                }
                else
                {
                    running_id = 1;
                }


                        if (chk_duplicate == true)
                {
                    var records_add = new List<Shift>
                    {
                    new Shift { ShiftNo = running_id, ShiftName = input_shift_name , Start = input_shift_time , End = ""},
                     };
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        // Don't write the header again.
                        HasHeaderRecord = false,
                    };
                    using (var stream = File.Open("C:\\Users\\ADMIN\\Desktop\\test\\shift.csv", FileMode.Append))
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
            get_shift();
            metroGrid1.Show();
        }
    }
}
