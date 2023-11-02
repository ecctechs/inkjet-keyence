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
using CsvHelper.Configuration;
using CsvHelper;
using inkjet.Class;

namespace inkjet
{
    public partial class AddEditConnection : Form
    {
        public AddEditConnection(Inkjet obj)
        {
            InitializeComponent();
            bindingSource1.DataSource = obj;
            //Console.WriteLine(obj.IPAdress);

            if (obj.IPAdress != null)
            {
                string ip1 = obj.IPAdress.Substring(0, 3); // substr1 will contain "Hello"
                string ip2 = obj.IPAdress.Substring(4, 3);
                string ip3 = obj.IPAdress.Substring(8, 1);
                string ip4 = obj.IPAdress.Substring(10, 1);
                string ip_conbine = ip1 + "." + ip2 + "." + ip3 + "." + ip4;
                //Console.WriteLine(ip_conbine + "full :"+ obj.IPAdress);

                txtIP1.Text = ip1;
                txtIP2.Text = ip2;
                txtIP3.Text = ip3;
                txtIP4.Text = ip4;

                string inkjet_id = Convert.ToString(obj.InkJetID);
                txtEditID.Text = inkjet_id;
            }
        }

        private void btnCancal_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(id_edit);
            if (txtIP1.Text.Trim() != "" && txtIP2.Text.Trim() != "" && txtIP3.Text.Trim() != "" && txtIP4.Text.Trim() != ""
                && txtIP1.Text.Length == 3 && txtIP2.Text.Length == 3 && txtIP3.Text.Length == 1 && txtIP4.Text.Length == 1)
            {
                var chk_duplicate = true;
                var full_ip = txtIP1.Text + "." + txtIP2.Text + "." + txtIP3.Text + "." + txtIP4.Text;
                //Console.WriteLine(full_ip);
                //Console.WriteLine(txtIP1.Text);

                List<Inkjet> records;

                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<Inkjet>().ToList();

                    for (int i = 0; i < records.Count; i++)
                    {
                        if (records[i].IPAdress == full_ip && txtEditID.Text == "" )
                        {
                            chk_duplicate = false;
                            break;
                        }
                        else
                        {
                            chk_duplicate = true;
                        }
                    }

                    for (int i = 0; i < records.Count; i++)
                    {

                        if (records[i].IPAdress == full_ip && records[i].InkJetID.ToString() != txtEditID.Text)
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
                int running_id;
                if (records.Count > 0)
                {
                    var lastItem = records.LastOrDefault();
                    running_id = lastItem.InkJetID + 1;
                }
                else
                    running_id = 1;

                Console.WriteLine(chk_duplicate);
                if (chk_duplicate == true)
                {
                    if (txtEditID.Text == "")
                    {
                        var records_add = new List<Inkjet>
                    {
                    new Inkjet { InkJetID = running_id, InkJetName = txtInkjetName.Text , IPAdress = full_ip},
                     };
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            // Don't write the header again.
                            HasHeaderRecord = false,
                        };
                        using (var stream = File.Open("C:\\Users\\ADMIN\\Desktop\\test\\inkjet.csv", FileMode.Append))
                        using (var writer = new StreamWriter(stream))
                        using (var csv = new CsvWriter(writer, config))
                        {
                            csv.WriteRecords(records_add);
                        }
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        var record_edit = new List<Inkjet>();
                        using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            //var records = new List<User>();
                            csv.Read();
                            csv.ReadHeader();

                            while (csv.Read())
                            {
                                var record = new Inkjet
                                {
                                    InkJetID = csv.GetField<int>("InkJet ID"),
                                    InkJetName = csv.GetField("InkJet Name"),
                                    IPAdress = csv.GetField("IP Address"),
                                    Status = csv.GetField("Status"),
                                    Status_inkjet = csv.GetField("Status_inkjet"),
                                    Ink = csv.GetField("Ink"),
                                    Solvent = csv.GetField("Solvent"),
                                    Pump = csv.GetField("Pump"),
                                    Filter = csv.GetField("Filter"),
                                    Program = csv.GetField("Program"),
                                };
                                Console.WriteLine(record.InkJetID);

                                if (txtEditID.Text == record.InkJetID.ToString())
                                {
                                    record.InkJetID = Convert.ToInt32(txtEditID.Text);
                                    record.InkJetName = txtInkjetName.Text;
                                    record.IPAdress = full_ip;
                                }
                                record_edit.Add(record);
                            }

                        }
                        using (var writer = new StreamWriter(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
                        using (var csv2 = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            csv2.WriteRecords(record_edit);
                        }
                        this.Close();
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Data is Already Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(this, "Please Fill All Data");
            }
        }

        private void txtIP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }

        private void txtIP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtIP3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtIP4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
