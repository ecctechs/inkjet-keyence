using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using CsvHelper;
using System.Windows.Forms;

namespace inkjet.Class
{
    public class ListError
    {

        [Name(name: "Error Code")]
        public int ErrorCode { get; set; }

        [Name(name: "Error Detail")]
        public string ErrorDetail { get; set; }

        [Name(name: "Error Type")]
        public string ErrorType { get; set; }

        public static List<ListError> ListError_list()
        {
            List<ListError> list_error = new List<ListError>();

            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\Inkjet\Data\list-error.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list_error = csv.GetRecords<ListError>().ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
            return list_error;
        }

        public static List<Error> Update_ErrorDetail(int input)
        {         
            List<Error> d = new List<Error>();
            List<ListError> error = ListError.ListError_list();
            var ink_status = "";
            var solvent_status = "";
            var pump_status = "";
            var filter_status = "";

            for (int i = 0; i < error.Count; i++)
            {
                if (input == error[i].ErrorCode)
                {
                    if (error[i].ErrorDetail.Contains("Empty") && error[i].ErrorDetail.Contains("Ink"))
                    {
                        ink_status = "Empty";
                    }
                    else if (error[i].ErrorDetail.Contains("Low") && error[i].ErrorDetail.Contains("Ink"))
                    {
                        ink_status = "Low";
                    }
                    else
                    {
                        ink_status = "Normal";
                    }

                    if(error[i].ErrorDetail.Contains("Empty") && error[i].ErrorDetail.Contains("Solvent"))
                    {
                        solvent_status = "Empty";
                    }
                    else if (error[i].ErrorDetail.Contains("Low") && error[i].ErrorDetail.Contains("Solvent"))
                    {
                        solvent_status = "Low";
                    }
                    else
                    {
                        solvent_status = "Normal";
                    }

                    if (error[i].ErrorDetail.Contains("Error") && error[i].ErrorDetail.Contains("Pump"))
                    {
                        pump_status = "Error";
                    }
                    else if (error[i].ErrorDetail.Contains("Warning") && error[i].ErrorDetail.Contains("Pump"))
                    {
                        pump_status = "Warning";
                    }
                    else
                    {
                        pump_status = "Normal";
                    }

                    if (error[i].ErrorDetail.Contains("Error") && error[i].ErrorDetail.Contains("Filter"))
                    {
                        filter_status = "Error";
                    }
                    else if (error[i].ErrorDetail.Contains("Warning") && error[i].ErrorDetail.Contains("Filter"))
                    {
                        filter_status = "Warning";
                    }
                    else
                    {
                        filter_status = "Normal";
                    }
  
                    d.Add(new Error { ErrorCode = input, Detail = error[i].ErrorDetail, ErrorType = error[i].ErrorType , InKLevel = ink_status, SolventLevel = solvent_status, PumpLifetime = pump_status, FilterLifetime = filter_status });        
                }
            }
            return d;
        }
    }
}
