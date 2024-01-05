using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;
using inkjet.Class;
using static Guna.UI2.Native.WinApi;

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
            metroGrid1.DefaultCellStyle.SelectionBackColor = Color.DarkOrange;
            metroGrid1.DefaultCellStyle.SelectionForeColor = Color.Black;
            get_shift();

            DateTime dt = DateTimeStart.Value;
            dt = dt.AddSeconds(-dt.Second);
            DateTimeStart.Value = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[0].Value);

            if (MessageBox.Show(this, "Do you confirm to Delete the Shift ?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                List<Shift> records = Shift.Delete_Shift(id);
                Shift.Update_Shift(records);
            }
            Shift.UpdateTime_Shift();
            get_shift();
            metroGrid1.Show();
        }

        public void get_shift()
        {
            List<Shift> records = Shift.ListShift();
            shiftBindingSource.DataSource = records;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var input_shift_name = txtShiftName.Text;
            //DateTime input_shift_time = DateTimeStart.Value;
            var input_shift_time = DateTimeStart.Text;

            DateTime date_start = Convert.ToDateTime(input_shift_time); // 1/1/0001 12:00:00 AM
            DateTime date_end = Convert.ToDateTime(input_shift_time).AddMinutes(-1); ; // 1/1/0001 12:00:00 AM
            string date_str = date_start.ToString("HH:mm");
            string date_str_end = date_end.ToString("HH:mm");
            var date_now_type = date_start.ToString("tt", CultureInfo.InvariantCulture);

            List<Shift> list_shift = new List<Shift>();
            var running_id = Shift.running_id;

            if (input_shift_name.Trim() != "" && input_shift_time != null)
            {
                var chk_duplicate = Shift.Duplicate_Shift(input_shift_name, date_str);
                if (chk_duplicate == true)
                {
                    list_shift.Add(new Shift { ShiftNo = running_id, ShiftName = input_shift_name, Start = date_str, End = "" , DateType = date_now_type });
                    Shift.Add_Shift(list_shift);
                  
                        Shift.UpdateTime_Shift();
                 
                }
                else
                {
                    MessageBox.Show(this, "Data is Already Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(this, "Please fill in Shift Name", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }     
            get_shift();
            metroGrid1.Show();
        }
    }
}
