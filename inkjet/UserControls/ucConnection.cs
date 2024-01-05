using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using CsvHelper;
using Guna.UI2.WinForms;
using inkjet.Class;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace inkjet.UserControls
{
    public partial class ucConnection : UserControl
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        public void InitTimer()
        {
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 5 seconds.
            myTimer.Interval = 5000;
            myTimer.Stop();
            myTimer.Start();
        }
        public ucConnection()
        {
            InitializeComponent();
        }

        private void ucConnection_Load(object sender, EventArgs e)
        {
            metroGrid1.DefaultCellStyle.SelectionBackColor = Color.DarkOrange;
            metroGrid1.DefaultCellStyle.SelectionForeColor = Color.Black;           
            get_Connection();
            Statu_Color();
            InitTimer();            
        }

        public void get_Connection()
        {
            List<Inkjet> records = Inkjet.ListInkjet();
            inkjetBindingSource.DataSource = records;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TimerClient.aTimer.Stop();
            var id = Convert.ToInt32(metroGrid1.Rows[metroGrid1.CurrentRow.Index].Cells[0].Value);
           
            if (MessageBox.Show(this, "Do you confirm to Delete the Inkjet ?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                List<Inkjet> records = Inkjet.Delete_Inkjet(id);
                Inkjet.Update_Inkjet(records);
            }

            get_Connection();
            Statu_Color();
            metroGrid1.Show();
            TimerClient.aTimer.Start();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            TimerClient.aTimer.Stop();
            using (AddEditConnection frm = new AddEditConnection(new Inkjet()))
            {
                //frm.ShowDialog();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    get_Connection();

                    Statu_Color();
                    metroGrid1.Show();
                    TimerClient.aTimer.Start();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TimerClient.aTimer.Stop();
            Inkjet obj = inkjetBindingSource.Current as Inkjet;

            int selectedrowindex = metroGrid1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = metroGrid1.Rows[selectedrowindex];
            string cellValue = Convert.ToString(selectedRow.Cells[2].Value);
            string gg = ucCSVmarking.inkjet_name;
            if (cellValue != gg) {

                if (obj != null)
                {
                    using (AddEditConnection frm = new AddEditConnection(obj))
                    {

                        if (frm.ShowDialog() == DialogResult.Cancel)
                        {
                            get_Connection();
                            Statu_Color();
                            metroGrid1.Show();
                            TimerClient.aTimer.Start();
                        }

                        else
                        {
                            get_Connection();
                            Statu_Color();
                            metroGrid1.Show();
                            TimerClient.aTimer.Start();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Cant edit because Inkjet is Running");
            }
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            get_Connection();
            Statu_Color();
            metroGrid1.Show();
        }

  
  

        public void Statu_Color()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();          
            style.ForeColor = Color.Green;
            style.SelectionForeColor = Color.Green;
            style.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //style.Font = "Century Gothic, 10.2pt, style = Bold";

            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            style2.ForeColor = Color.Red;
            style2.SelectionForeColor = Color.Red;
            style2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            foreach (DataGridViewRow row in metroGrid1.Rows)
            {
                string status = row.Cells[3].Value.ToString();
                if (status == "Connected")
                {                 
                    row.Cells[3].Style = style;            
                }
                else
                {
                    row.Cells[3].Style = style2;
                }
            }
        }
    }
}
