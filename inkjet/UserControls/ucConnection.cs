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
    public partial class ucConnection : UserControl
    {
        public ucConnection()
        {
            InitializeComponent();
            client.Program program = new client.Program();
            program.Execute_Client();
        }

        private void ucConnection_Load(object sender, EventArgs e)
        {
            get_Connection();
        }

        private void get_Connection()
        {
            List<Inkjet> records;

            using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Inkjet>().ToList();
                inkjetBindingSource.DataSource = records;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[0].Value);

            if (MessageBox.Show(this, "Yes/Cancel", "Delete Data", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                List<Inkjet> records;

                using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<Inkjet>().ToList();
                    int row = metroGrid1.RowCount;
                    for (int i = 0; i < records.Count; ++i)
                    {
                        if (records[i].InkJetID == id)
                        {
                            records.RemoveAt(i);
                        }
                    }
                }

                using (var writer = new StreamWriter(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(records);
                }
            }
            get_Connection();
            metroGrid1.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (AddEditConnection frm = new AddEditConnection(new Inkjet()))
            {
                //frm.ShowDialog();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    get_Connection();
                    metroGrid1.Show();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Inkjet obj = inkjetBindingSource.Current as Inkjet;

            if (obj != null)
            {
                using (AddEditConnection frm = new AddEditConnection(obj))
                {

                    if (frm.ShowDialog() == DialogResult.Cancel)
                    {
                        get_Connection();
                        metroGrid1.Show();
                    }

                    else 
                    { 
                        get_Connection();
                        metroGrid1.Show();
                    }
                }
            }

        
        }
    }
}
