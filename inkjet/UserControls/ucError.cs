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
    public partial class ucError : UserControl
    {
        string path_error_csv = "C:\\Users\\ADMIN\\Desktop\\test\\error2.csv";
        string path_inkjet_csv = "C:\\Users\\ADMIN\\Desktop\\test\\inkjet.csv";
        string path_shift_csv = "C:\\Users\\ADMIN\\Desktop\\test\\shift.csv";

        public ucError()
        {
            InitializeComponent();
        }

        public void getShift_DropDown()
        {
            using (var reader = new StreamReader(path_shift_csv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<Shift>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var record = new Shift
                    {
                        ShiftNo = csv.GetField<int>("NO."),
                        ShiftName = csv.GetField("Shift Name"),

                    };

                    records.Add(record);
                }
                records.Insert(0, new Shift() { ShiftNo = 0, ShiftName = "All" });
                shiftBindingSource.DataSource = records;
            }
        }

        public void getInkJet_DropDown()
        {
            using (var reader = new StreamReader(path_inkjet_csv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<Inkjet>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var record = new Inkjet
                    {
                        InkJetID = csv.GetField<int>("InkJet ID"),
                        InkJetName = csv.GetField("InkJet Name"),
                    };

                    records.Add(record);
                }
                records.Insert(0, new Inkjet() { InkJetID = 0, InkJetName = "All" });
                inkjetBindingSource.DataSource = records;
            }
        }

        public void getError_Data(DateTime DateStart, DateTime DateEnd, string PathErrorCsv, string Shift, string Inkjet)
        {
            var date_start = DateStart;
            TimeSpan ts = new TimeSpan(00, 00, 0);
            date_start = date_start.Date + ts;

            var date_end = DateEnd;
            TimeSpan ts2 = new TimeSpan(23, 59, 59);
            date_end = date_end.Date + ts2;

            using (var reader = new StreamReader(PathErrorCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<Error>();
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var record = new Error
                    {
                        InkJet = csv.GetField("Ink Jet"),
                        ErrorType = csv.GetField("Error Type"),
                        ErrorCode = csv.GetField("Error Code"),
                        Detail = csv.GetField("Detail"),
                        Date = csv.GetField<string>("Date"),
                        Time = csv.GetField("Time"),
                        Shift = csv.GetField("Shift"),
                        InKLevel = csv.GetField("InK Level"),
                        SolventLevel = csv.GetField("Solvent level"),
                        PumpLifetime = csv.GetField("Pump Life time"),
                        FilterLifetime = csv.GetField("Filter Life time"),
                    };

                    var convert_date = DateTime.ParseExact(record.Date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    if (convert_date >= date_start && convert_date <= date_end)
                    {
                        if (Inkjet == "All")
                        {
                            if (Shift == "All")
                            {
                                records.Add(record);
                            }
                            else if (Shift == record.Shift)
                            {
                                records.Add(record);
                            }
                        }
                        else if (Inkjet == record.InkJet)
                        {
                            if (Shift == "All")
                            {
                                records.Add(record);
                            }
                            else if (Shift == record.Shift)
                            {
                                records.Add(record);
                            }
                        }
                    }
                }
                errorBindingSource.DataSource = records;
            }

        }

        private void ucError_Load(object sender, EventArgs e)
        {
            getShift_DropDown();
            getInkJet_DropDown();
            getError_Data(mtDateStart.Value, mtDateEnd.Value, path_error_csv, "All", "All");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var Shift_selected = mcbShift.Text;
            var Inkjet = mcbInkjet.Text;
            Console.WriteLine(Shift_selected);

            getError_Data(mtDateStart.Value, mtDateEnd.Value, path_error_csv, Shift_selected, Inkjet);
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
