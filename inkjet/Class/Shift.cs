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
using System.Collections;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;

namespace inkjet.Class
{
    public class Shift
    {
        [Name(name: "NO.")]
        public int ShiftNo { get; set; }

        [Name(name: "Shift Name")]
        public string ShiftName { get; set; }

        [Name(name: "Start")]
        public string Start { get; set; }

        [Name(name: "End")]
        public string End { get; set; }


        [Name(name: "Date Type")]
        public string DateType { get; set; }

        public static int running_id;
  
        public static List<Shift> ListShift()
        {
            List<Shift> list_shift = new List<Shift>();
            
            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\shift.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list_shift = csv.GetRecords<Shift>().ToList();
                    if (list_shift.Count > 0)
                    {
                        var lastItem = list_shift.LastOrDefault();
                        running_id = lastItem.ShiftNo + 1;
                    }
                    else
                    {
                        running_id = 1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
            return list_shift;
        }

        public static void Add_Shift(List<Shift> records)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                using (var stream = File.Open("C:\\Users\\ADMIN\\Desktop\\test\\Shift.csv", FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(records);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
        }

        public static List<Shift> Delete_Shift(int id)
        {
            List<Shift> list_shift = new List<Shift>();

            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\shift.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list_shift = csv.GetRecords<Shift>().ToList();
                    for (int i = 0; i < list_shift.Count; ++i)
                    {
                        if (list_shift[i].ShiftNo == id)
                        {
                            list_shift.RemoveAt(i);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }           
            return list_shift;
        }

        public static void Update_Shift(List<Shift> records)
        {
            try
            {
                using (var writer = new StreamWriter(@"C:\Users\ADMIN\Desktop\test\shift.csv"))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(records);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
        }
        public static bool Duplicate_Shift(string input_shift_name,string input_shift_time)
        {
            var chk_duplicate = false;
            List<Shift> list_shift = new List<Shift>();

            try
            {
            using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\shift.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                list_shift = csv.GetRecords<Shift>().ToList();

                if (list_shift.Count == 0)
                {
                    chk_duplicate = true;
                }

                for (int i = 0; i < list_shift.Count; ++i)
                {
                    if (list_shift[i].ShiftName != input_shift_name && list_shift[i].Start != input_shift_time)
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

                //for (int x = 0; x < list_shift.Count; ++x) 
                //    {
                //        if (list_shift[x].Start != input_shift_time)
                //        {
                //            chk_duplicate = true;
                //            continue;
                //        }
                //        else
                //        {
                //            chk_duplicate = false;
                //            break;
                //        }
                //    }

            }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
            return chk_duplicate;
        }

        public static void UpdateTime_Shift()
        {

            List<Shift> records;
            var timer_list = new List<string>();
  
            List<string> end_shift = new List<string>();
            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\shift.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {

                    records = csv.GetRecords<Shift>().ToList();
                    var lastItem = records.LastOrDefault();

                    for (int i = 0; i < records.Count; ++i)
                    {
                        DateTime date_updated = Convert.ToDateTime(records[i].Start).AddMinutes(-1); ; // 1/1/0001 12:00:00 AM
                        string date_str = date_updated.ToString("HH:mm");

                    end_shift.Add(date_str);
                    end_shift = end_shift.OrderBy(q => q).ToList();
                          
                }
                }
                                      
            var newList = records.OrderBy(x => x.Start).ToList();

            for(int i = 0;i < newList.Count; ++i)
            {


                if (i == end_shift.Count - 1)
                {
                    newList[i].End = end_shift[0];
                }
                else
                {
                    newList[i].End = end_shift[i+1];
                    }
            }
                //List<Shift> SortedList = newList.OrderBy(o => o.ShiftNo).ToList();
                Update_Shift(newList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
        }

        public static string Update_Shift(string input)
        {
            string result = "";
            List<Shift> shifts_list = Shift.ListShift();
            DateTime data = Convert.ToDateTime(input);
            //Console.WriteLine(input);
            //DateTime data = DateTime.ParseExact("yyyy-MM-dd HH:mm:ss:fff", input,CultureInfo.InvariantCulture);
            foreach (var e in shifts_list)
            {
                DateTime date_start = Convert.ToDateTime(e.Start);
                DateTime date_end = Convert.ToDateTime(e.End);

                if (data > date_start && data < date_end)
                {
                    //Console.WriteLine(e.ShiftName);
                    result = e.ShiftName;

                }
            }

            return result;
        }
    }

}
