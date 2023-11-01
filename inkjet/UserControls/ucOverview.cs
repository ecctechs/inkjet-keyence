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
using System.Timers;
using System.Windows.Forms;
using CsvHelper;
using Guna.UI2.WinForms;
using inkjet.Class;
using TheArtOfDevHtmlRenderer.Adapters.Entities;
using WinForms = System.Windows.Forms;

namespace inkjet.UserControls
{

    public partial class ucOverview : UserControl
    {
        public ucOverview()
        {
            InitializeComponent();           
        }

        private void get_item()
        {
            
            flowLayoutPanel1.Controls.Clear();
            List<Inkjet> records;

            using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Inkjet>().ToList();
            }

            for (int i = 0; i < records.Count; i++)
            {

                itemOverview item = new itemOverview();
                item.guna2HtmlLabel9.Text = records[i].InkJetName;
                item.guna2HtmlLabel10.Text = records[i].IPAdress;
                item.guna2HtmlLabel11.Text = records[i].Status_inkjet;
                item.guna2HtmlLabel12.Text = records[i].Program;
                item.guna2HtmlLabel13.Text = records[i].Ink;
                item.guna2HtmlLabel14.Text = records[i].Solvent;
                item.guna2HtmlLabel15.Text = records[i].Pump;
                item.guna2HtmlLabel16.Text = records[i].Filter;

                if (records[i].Status_inkjet == "Printable")
                {
                    item.guna2GradientPanel1.FillColor = Color.PaleGreen;
                    item.guna2GradientPanel1.FillColor2 = Color.PaleGreen;
                    item.guna2GradientPanel1.BorderColor = Color.Green;
                }
                else if (records[i].Status_inkjet == "Stopped")
                {
                    item.guna2GradientPanel1.FillColor = Color.LightGray;
                    item.guna2GradientPanel1.FillColor2 = Color.LightGray;
                    item.guna2GradientPanel1.BorderColor = Color.Gray;
                }
                else if (records[i].Status_inkjet == "Warning")
                {
                    item.guna2GradientPanel1.FillColor = Color.SandyBrown;
                    item.guna2GradientPanel1.FillColor2 = Color.SandyBrown;
                    item.guna2GradientPanel1.BorderColor = Color.DarkGoldenrod;
                }
                else if (records[i].Status_inkjet == "Error")
                {
                    item.guna2GradientPanel1.FillColor = Color.Salmon;
                    item.guna2GradientPanel1.FillColor2 = Color.Salmon;
                    item.guna2GradientPanel1.BorderColor = Color.DarkRed;
                }

                flowLayoutPanel1.Controls.Add(item);
                
                //panel1.Dock = DockStyle.Fill;
                //panel1.Controls.Clear();
                //panel1.Controls.Add(item);
                //item.Dock = DockStyle.Top;
                //item.BringToFront();

                //flowLayoutPanel1.Controls.Add(new Guna2Button() { Text = "Button name" });
                //flowLayoutPanel1.Controls.Add(new Guna2Shapes() { Text = "Button name" , Shape  = Guna.UI2.WinForms.Enums.ShapeType.Rounded });
            }            
        }
        private void ucOverview_Load(object sender, EventArgs e)
        {
            //timer1.Stop();
            //timer1.Start();
            //if (timer1.Enabled)
            //{
            //    RefreshMyForm();
            //}
        }

        public void RefreshMyForm()
        {
            //update form with latest Data
            get_item();
            client.Program program = new client.Program();
            program.Execute_Client();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshMyForm();
        }

        //private void timer1_Tick(object sender, EventArgs e) //ถ้าครบ 5 นาที
        //{
        //    RefreshMyForm();
        //    //timer1.Start();
        //}
    }
}
