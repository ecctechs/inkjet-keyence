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
    public class Inkjet
    {
        [Name(name: "InkJet ID")]
        public int InkJetID { get; set; }

        [Name(name: "InkJet Name")]
        public string InkJetName { get; set; }

        [Name(name: "IP Address")]
        public string IPAdress { get; set; }

        [Name(name: "Status")]
        public string Status { get; set; }

        [Name(name: "Status_inkjet")]
        public string Status_inkjet { get; set; }

        [Name(name: "Ink")]
        public string Ink { get; set; }

        [Name(name: "Solvent")]
        public string Solvent { get; set; }

        [Name(name: "Pump")]
        public string Pump { get; set; }

        [Name(name: "Filter")]
        public string Filter { get; set; }

        [Name(name: "Program")]
        public string Program
        {
            get; set;
        }

        public static int running_id;

        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


        public static List<Inkjet> ListInkjet()
        {
            List<Inkjet> list_inkjet = new List<Inkjet>();

            try
            {
                using (var reader = new StreamReader(path + "\\Inkjet\\Data\\inkjet.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                { 
                    list_inkjet = csv.GetRecords<Inkjet>().ToList();
                    if (list_inkjet.Count > 0)
                    {
                        var lastItem = list_inkjet.LastOrDefault();
                        running_id = lastItem.InkJetID + 1;
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
                //MessageBox.Show(e.ToString());
            }
            return list_inkjet;
        }

        public static List<Inkjet> Delete_Inkjet(int id) 
        {
           
            List<Inkjet> list_inkjet = new List<Inkjet>();

            try
            {
                using (var reader = new StreamReader(path + "\\Inkjet\\Data\\inkjet.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list_inkjet = csv.GetRecords<Inkjet>().ToList();

                    for (int i = 0; i < list_inkjet.Count; ++i)
                    {
                        if (list_inkjet[i].InkJetID == id)
                        {
                            list_inkjet.RemoveAt(i);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //MessageBox.Show(e.ToString());
            }
            return list_inkjet;
        }

        public static void Update_Inkjet(List<Inkjet> records)
        {
            try
            {
                using (var writer = new StreamWriter(path + "\\Inkjet\\Data\\inkjet.csv"))
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

        public static void Add_Inkjet(List<Inkjet> records)
        {         
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                using (var stream = File.Open(path + "\\Inkjet\\Data\\inkjet.csv", FileMode.Append))
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

        public static bool Duplicate_Inkjet(string full_ip ,string txtEditID )
        {
            List<Inkjet> list_inkjet = new List<Inkjet>();
            var chk_duplicate = true;
            try
            {
                using (var reader = new StreamReader(path + "\\Inkjet\\Data\\inkjet.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    list_inkjet = csv.GetRecords<Inkjet>().ToList();
                    for (int i = 0; i < list_inkjet.Count; i++)
                    {
                        if (list_inkjet[i].IPAdress == full_ip && txtEditID == "")
                        {
                            chk_duplicate = false;
                            break;
                        }
                        else
                        {
                            chk_duplicate = true;
                        }
                    }

                    for (int i = 0; i < list_inkjet.Count; i++)
                    {

                        if (list_inkjet[i].IPAdress == full_ip && list_inkjet[i].InkJetID.ToString() != txtEditID )
                        {
                            chk_duplicate = false;
                            //MetroFramework.MetroMessageBox.Show(this, "Data is Already Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                        else
                        {
                            chk_duplicate = true;
                        }
                        //Console.WriteLine(records[i].IPAdress + "==" + full_ip + "&&" + txtEditID.Text + "!=" + records[i].InkJetID);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //MessageBox.Show(e.ToString());
            }
            return chk_duplicate;
        }
    }
}
