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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace inkjet.UserControls
{
    public partial class ucData : UserControl
    {
    
        public ucData()
        {
            InitializeComponent();
        }
        public void getInkJet_DropDown()
        {
            List<Inkjet> records = Inkjet.ListInkjet();
            records.Insert(0, new Inkjet() { InkJetID = 0, InkJetName = "All" });
            inkjetBindingSource.DataSource = records;        
        }

        public void getShift_DropDown()
        {
            List<Shift> records = Shift.ListShift();
            records.Insert(0, new Shift() { ShiftNo = 0, ShiftName = "All" });
            shiftBindingSource.DataSource = records;
        }

        public void getDatalog_Data(DateTime DateStart, DateTime DateEnd, string Shift, string Inkjet)
        {
            var date_start = DateStart;
            TimeSpan ts = new TimeSpan(00, 00, 0);
            date_start = date_start.Date + ts;

            var date_end = DateEnd;
            TimeSpan ts2 = new TimeSpan(23, 59, 59);
            date_end = date_end.Date + ts2;
            List<Datalog> record = Datalog.ListData_Search(date_start, date_end, Shift, Inkjet);    
            datalogBindingSource.DataSource = record;
        }

        private void ucData_Load(object sender, EventArgs e)
        {
            mtDateStart.Value = DateTime.Now;
            mtDateEnd.Value = DateTime.Now;
            getShift_DropDown();
            getInkJet_DropDown();
            getDatalog_Data(mtDateStart.Value, mtDateEnd.Value, "All", "All");
            InitTimer();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var Shift_selected = mcbShift.Text;
            var Inkjet = mcbInkjet.Text;

            getDatalog_Data(mtDateStart.Value, mtDateEnd.Value, Shift_selected, Inkjet);
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var sw = new StreamWriter(sfd.FileName))
                    {
                        using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
                        {
                            csv.WriteHeader(typeof(Datalog));
                            foreach (Datalog s in datalogBindingSource.DataSource as List<Datalog>)
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

        static System.Windows.Forms.Timer myTimer_data = new System.Windows.Forms.Timer();
        public void InitTimer()
        {
            myTimer_data = new System.Windows.Forms.Timer();
            myTimer_data.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 5 seconds.
            myTimer_data.Interval = 5000;
            myTimer_data.Stop();
            myTimer_data.Start();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            var Shift_selected = mcbShift.Text;
            var Inkjet = mcbInkjet.Text;

            getDatalog_Data(mtDateStart.Value, mtDateEnd.Value, Shift_selected, Inkjet);
        }
    }
}
