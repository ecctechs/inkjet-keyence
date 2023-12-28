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
using CsvHelper.Configuration;

namespace inkjet.Class
{
    public class Error
    {
        //[Name(name: "Error ID")]
        //public int ErrorID { get; set; }

        [Name(name: "Ink Jet")]
        public string InkJet { get; set; }

        [Name(name: "Error Type")]
        public string ErrorType { get; set; }

        [Name(name: "Error Code")]
        public int ErrorCode { get; set; }

        [Name(name: "Detail")]
        public string Detail { get; set; }

        [Name(name: "Date")]
        public string Date { get; set; }

        [Name(name: "Time")]
        public string Time { get; set; }

        [Name(name: "Shift")]
        public string Shift { get; set; }

        [Name(name: "InK Level")]
        public string InKLevel { get; set; }

        [Name(name: "Solvent level")]
        public string SolventLevel { get; set; }

        [Name(name: "Pump Life time")]
        public string PumpLifetime { get; set; }

        [Name(name: "Filter Life time")]
        public string FilterLifetime { get; set; }

        [Name(name: "Status")]
        public string Status { get; set; }

        
    

        public static List<Error> ListError()
        {          
            List<Error> list_error = new List<Error>();

            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\Inkjet\Data\error.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list_error = csv.GetRecords<Error>().ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //MessageBox.Show(e.ToString());
            }
            return list_error;
        }

        public static List<Error> ListError_Search(DateTime dateStart , DateTime DateEnd , string Shift , string inkjet)
        {

            var list_error_query = new List<Error>();

            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\Inkjet\Data\error.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    //var records = new List<Error>();
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        var record = new Error
                        {
                            InkJet = csv.GetField("Ink Jet"),
                            ErrorType = csv.GetField("Error Type"),
                            ErrorCode = csv.GetField<int>("Error Code"),
                            Detail = csv.GetField("Detail"),
                            Date = csv.GetField<string>("Date"),
                            Time = csv.GetField("Time"),
                            Shift = csv.GetField("Shift"),
                            InKLevel = csv.GetField("InK Level"),
                            SolventLevel = csv.GetField("Solvent level"),
                            PumpLifetime = csv.GetField("Pump Life time"),
                            FilterLifetime = csv.GetField("Filter Life time"),
                            Status = csv.GetField("Status"),
                        };
                        //var convert_date = DateTime.ParseExact(record.Date, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        var convert_date2 = DateTime.Parse(record.Date);
                        //Console.WriteLine(convert_date2 + ">="+date_start+"&&"+convert_date+"<="+date_end);
                        if (convert_date2 >= dateStart && convert_date2 <= DateEnd)
                        {
                            if (inkjet == "All")
                            {
                                if (Shift == "All")
                                {
                                    list_error_query.Add(record);
                                }
                                else if (Shift == record.Shift)
                                {
                                    list_error_query.Add(record);
                                }
                            }
                            else if (inkjet == record.InkJet)
                            {
                                if (Shift == "All")
                                {
                                    list_error_query.Add(record);
                                }
                                else if (Shift == record.Shift)
                                {
                                    list_error_query.Add(record);
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
            return list_error_query;
        }

        public static void Update_Error(List<Error> records)
        {
            try
            {
                using (var writer = new StreamWriter(@"C:\Users\ADMIN\Desktop\Inkjet\Data\error.csv"))
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

        public static void Add_Error(List<Error> records)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                using (var stream = File.Open("C:\\Users\\ADMIN\\Desktop\\Inkjet\\Data\\error.csv", FileMode.Append))
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

    }
}
