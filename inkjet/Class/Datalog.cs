using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Windows.Forms;
using CsvHelper.Configuration.Attributes;
using inkjet.UserControls;
using CsvHelper.Configuration;

namespace inkjet.Class
{
    public class Datalog
    {
        [Name(name: "Ink Jet")]
        public string InkJet { get; set; }

        [Name(name: "Program")]
        public string Program { get; set; }

        [Name(name: "Qty")]
        public string Qty { get; set; }

        [Name(name: "DateTime Start")]
        public string DateStart { get; set; }

        [Name(name: "DateTime End / Last Updated")]
        public string DateEnd { get; set; }

        [Name(name: "Shift")]
        public string Shift { get; set; }

        [Name(name: "qty_start")]
        public string qty_start { get; set; }

        [Name(name: "qty_end")]
        public string qty_end { get; set; }

        public static List<Datalog> ListDatalog()
        {
            List<Datalog> list_datalog = new List<Datalog>();

            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\Inkjet\Data\data_log.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list_datalog = csv.GetRecords<Datalog>().ToList();                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //MessageBox.Show(e.ToString());
            }
            return list_datalog;
        }

        public static List<Datalog> ListData_Search(DateTime dateStart, DateTime DateEnd, string Shift, string inkjet)
        {

            var list_data_query = new List<Datalog>();

            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\Inkjet\Data\data_log.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var record = new Datalog
                    {
                        InkJet = csv.GetField("Ink Jet"),
                        Program = csv.GetField("Program"),
                        Qty = csv.GetField("Qty"),
                        DateStart = csv.GetField<string>("DateTime Start"),
                        DateEnd = csv.GetField<string>("DateTime End / Last Updated"),
                        Shift = csv.GetField("Shift"),
                        qty_start = csv.GetField("qty_start"),
                        qty_end = csv.GetField("qty_end"),
                    };
                        //Console.WriteLine(record.DateStart);
                        var convert_date2 = DateTime.Parse(record.DateStart);
                        //var convert_date = DateTime.ParseExact(record.DateStart, "dd/MM/yyyy hh:mm", System.Globalization.CultureInfo.InvariantCulture);
                    if (convert_date2 >= dateStart && convert_date2 <= DateEnd)
                    {
                        if (inkjet == "All")
                        {
                            if (Shift == "All")
                            {
                                list_data_query.Add(record);
                            }
                            else if (Shift == record.Shift)
                            {
                                    list_data_query.Add(record);
                            }
                        }
                        else if (inkjet == record.InkJet)
                        {
                            if (Shift == "All")
                            {
                                    list_data_query.Add(record);
                            }
                            else if (Shift == record.Shift)
                            {
                                    list_data_query.Add(record);
                            }
                        }
                    }
                
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //MessageBox.Show(e.ToString());
            }
            return list_data_query;
        }

        public static void Add_DataLog(List<Datalog> records)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                using (var stream = File.Open("C:\\Users\\ADMIN\\Desktop\\Inkjet\\Data\\data_log.csv", FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(records);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //MessageBox.Show(e.ToString());
            }
        }

        public static void Update_Datalog(List<Datalog> records)
        {
            try
            {
                using (var writer = new StreamWriter(@"C:\Users\ADMIN\Desktop\Inkjet\Data\data_log.csv"))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(records);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //MessageBox.Show(e.ToString());
            }
        }

    }

}
