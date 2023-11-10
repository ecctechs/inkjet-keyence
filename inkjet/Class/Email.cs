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
    public class Email
    {
        [Name(name: "No.")]
        public int EmailNo { get; set; }

        [Name(name: "Email")]
        public string EmailName { get; set; }

        public static int running_id;
        public static List<Email> ListEmail()
        {
            List<Email> list_email = new List<Email>();

            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\email.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list_email = csv.GetRecords<Email>().ToList();
                    if (list_email.Count > 0)
                    {
                        var lastItem = list_email.LastOrDefault();
                        running_id = lastItem.EmailNo + 1;
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
            return list_email;
        }

        public static void Add_Email(List<Email> records)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                using (var stream = File.Open("C:\\Users\\ADMIN\\Desktop\\test\\email.csv", FileMode.Append))
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

        public static List<Email> Delete_Email(int id)
        {
            List<Email> list_email = new List<Email>();

            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\email.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list_email = csv.GetRecords<Email>().ToList();
                    for (int i = 0; i < list_email.Count; ++i)
                    {
                        if (list_email[i].EmailNo == id)
                        {
                            list_email.RemoveAt(i);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
            return list_email;
        }

        public static void Update_Email(List<Email> records)
        {
            try
            {
                using (var writer = new StreamWriter(@"C:\Users\ADMIN\Desktop\test\email.csv"))
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

        public static bool Duplicate_Email(string input_email)
        {
            var chk_duplicate = true;
            List<Email> list_email = new List<Email>();

            try
            {
                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\email.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                list_email = csv.GetRecords<Email>().ToList();

                if (list_email.Count == 0)
                {
                    chk_duplicate = true;
                }

                for (int i = 0; i < list_email.Count; i++)
                {

                    if (list_email[i].EmailName != input_email)
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
            return chk_duplicate;
        }
    }
}
