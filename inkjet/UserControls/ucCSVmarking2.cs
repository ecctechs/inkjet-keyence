using CsvHelper;
using inkjet.Class;
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
using ECCLibary;

namespace inkjet.UserControls
{
    public partial class ucCSVmarking2 : UserControl
    {
        private Keyence keyenceDevice; // ประกาศตัวแปรสำหรับเก็บอ็อบเจกต์ของ Keyence
        public ucCSVmarking2()
        {
            InitializeComponent();
            
        }

        private static ucCSVmarking2 _instance;
        public static ucCSVmarking2 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCSVmarking2();
                return _instance;
            }
        }

        private void ucCSVmarking2_Load(object sender, EventArgs e)
        {
            getInkJet_DropDown();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    List<csvMarking> records = new List<csvMarking>().ToList();
                    using (var reader = new StreamReader(ofd.FileName))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        CsvbindingSource1.DataSource = csv.GetRecords<csvMarking>().ToList();
                        guna2TextBox1.Text = ofd.FileName;
                        foreach (csvMarking s in CsvbindingSource1.DataSource as List<csvMarking>)
                        {
                            records.Add(s);
                        }
                        CsvbindingSource1.DataSource = records;
                    }
                }
            }
        }

        public void getInkJet_DropDown()
        {
            List<Inkjet> records = Inkjet.ListInkjet();
            InkjetbindingSource1.DataSource = records;
        }

        private void guna2TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void guna2TextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string id = guna2ComboBox1.Text;
            List<string> ip_inkjet = get_inkjet_ip(id);

            if (guna2TextBox5.Text != "")
            {
                int program = Int32.Parse(guna2TextBox5.Text);
                if (program > 500)
                {
                    MessageBox.Show("Program is out of rang ! more then 500");
                    guna2TextBox5.Text = "";
                    guna2HtmlLabel5.Visible = false;
                }
                else
                {
                    //get_name_programs(ip_inkjet[0], program);
                }
            }
        }

        public static List<string> get_inkjet_ip(string inkjet_id)
        {
            List<string> listRange = new List<string>();
            List<Inkjet> records = Inkjet.ListInkjet();

            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].InkJetName == inkjet_id)
                {
                    listRange.Add(records[i].IPAdress);
                    listRange.Add(records[i].Status);
                }
            }
            return listRange;

        }
    }
}
