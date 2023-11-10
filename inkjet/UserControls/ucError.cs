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
using Guna.UI2.WinForms;
using inkjet.Class;

namespace inkjet.UserControls
{
    public partial class ucError : UserControl
    {    
        public ucError()
        {
            InitializeComponent();
        }

        public void getShift_DropDown()
        {
            List<Shift> records = Shift.ListShift();
            records.Insert(0, new Shift() { ShiftNo = 0, ShiftName = "All" });
            shiftBindingSource.DataSource = records;
        }

        public void getInkJet_DropDown()
        {
            List<Inkjet> records = Inkjet.ListInkjet();
            records.Insert(0, new Inkjet() { InkJetID = 0, InkJetName = "All" });
            inkjetBindingSource.DataSource = records;
        }

        public void getError_Data(DateTime DateStart, DateTime DateEnd, string Shift, string Inkjet)
        {
            var date_start = DateStart;
            TimeSpan ts = new TimeSpan(00, 00, 0);
            date_start = date_start.Date + ts;

            var date_end = DateEnd;
            TimeSpan ts2 = new TimeSpan(23, 59, 59);
            date_end = date_end.Date + ts2;
            List<Error> record = Error.ListError_Search(date_start,date_end,Shift,Inkjet);          
            errorBindingSource.DataSource = record;
        }

        private void ucError_Load(object sender, EventArgs e)
        {
            getShift_DropDown();
            getInkJet_DropDown();
            getError_Data(mtDateStart.Value, mtDateEnd.Value, "All", "All");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var Shift_selected = mcbShift.Text;
            var Inkjet = mcbInkjet.Text;
            getError_Data(mtDateStart.Value, mtDateEnd.Value, Shift_selected, Inkjet);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var sw = new StreamWriter(sfd.FileName))
                    {
                        using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
                        {
                            csv.WriteHeader(typeof(Error));
                            foreach (Error s in errorBindingSource.DataSource as List<Error>)
                            {
                                csv.NextRecord();
                                csv.WriteRecord(s);
                            }
                        }
                    }
                    MessageBox.Show(this, "Your data has been successfully export.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
