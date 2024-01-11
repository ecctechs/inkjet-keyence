using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using inkjet.Class;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web.UI.WebControls;
using System.Threading;
using Guna.UI2.WinForms;
using System.Timers;

namespace inkjet.UserControls
{
    public partial class ucCSVmarking : UserControl
    {

        private static List<csvMarking> list_csv = new List<csvMarking>().ToList();
        private static List<Inkjet> list_inkjet = new List<Inkjet>().ToList();
        public static string inkjet_name = "";
        private static string programs_name = "";
        private static string block_name = "";
        private static string type_s = "";
        private static int sim_data = 0;
        private static int current_qty = 0;
 

        private System.Windows.Forms.Timer x = new System.Windows.Forms.Timer();

        private static ucCSVmarking _instance;
        public static ucCSVmarking Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCSVmarking();
                return _instance;
            }
        }
        public ucCSVmarking()
        {
            InitializeComponent();
        }

        private void Timer_sim()
        {
            x.Interval = (1000);
            x.Tick += new EventHandler(xMethod);
            x.Start();
        }

        private void xMethod(object sender, System.EventArgs e)
        {
            chk_qty_change();
        }
        private void ucCSVmarking_Load(object sender, EventArgs e)
        {
            getInkJet_DropDown();
            
        }

        public void CsvButtonStart_Click(object sender, EventArgs e)
        {
            inkjet_name = guna2ComboBox1.Text;
            programs_name = guna2TextBox5.Text;
            block_name = guna2TextBox6.Text;
            type_s = guna2ComboBox2.Text;

            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("กรุณาอัพโหลดไฟล์ CSV");
            }
            else if (ChkInkjet() == false)
            {
                MessageBox.Show("Inkjet status is not Printable");
            }
            else if(guna2TextBox5.Text == "" )
            {
                MessageBox.Show("Please fill Programs number");
            }
            else if(guna2TextBox6.Text == "")
            {
                MessageBox.Show("Please fill Block No.");
            }
            else 
            {

                Timer_sim();
                CsvButtonStart.Hide();
                CsvButtonStop.Show();

                guna2ComboBox1.Enabled = false;
                guna2ComboBox2.Enabled = false;
                guna2TextBox5.ReadOnly = true;
                guna2TextBox6.ReadOnly = true;
                guna2TextBox5.FillColor = Color.Gray;
                guna2TextBox6.FillColor = Color.Gray;

                string inkjet_id = guna2ComboBox1.Text;
                string programs_id = guna2TextBox5.Text;
                string block_id = guna2TextBox6.Text;
                string type = guna2ComboBox2.Text;
                int start = 0;
                
                csvMarking_chk.set_running(true);
                Add_detail(inkjet_id,programs_id,block_id,start, type);
                On_Off_printer(inkjet_id, "SQ");


            }
         
        }

        public void getInkJet_DropDown()
        {
            List<Inkjet> records = Inkjet.ListInkjet();
            InkjetbindingSource1.DataSource = records;
            list_inkjet = records;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {            
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
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
                        list_csv = records;
                        reset_csv_data();
                    }
                }
            }
         }

        public void reset_csv_data()
        {
            x.Stop();
            inkjet_name = "";
            CsvButtonStart.Show();
            CsvButtonStop.Hide();
            guna2ComboBox1.SelectedIndex = 0;
            guna2TextBox5.Text = "";
            guna2TextBox6.Text = "";
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
        }
         
        private void CsvButtonStop_Click(object sender, EventArgs e)
        {
            x.Stop();
            guna2ComboBox1.Enabled = true;
            guna2TextBox5.ReadOnly = false;
            guna2TextBox6.ReadOnly = false;
            guna2ComboBox2.Enabled = true;
            guna2TextBox5.FillColor = Color.White;
            guna2TextBox6.FillColor = Color.White;
            CsvButtonStart.Show();
            CsvButtonStop.Hide();
            string inkjet_id = guna2ComboBox1.Text;
            On_Off_printer(inkjet_id, "SR");
            csvMarking_chk.set_running(false);

        }

        public bool ChkInkjet()
        {
            string inkjet_chk = inkjet_name;          
            bool chk = false;
            List<Inkjet> records = Inkjet.ListInkjet();

           
            for (int i = 0; i < records.Count; i++)
            {
                //&& records[i].Status == "Connected"
                //Console.WriteLine(records[i].InkJetName+"=="+ inkjet_chk+"$$"+ records[i].Status+ "Connected");
                //if (records[i].InkJetName.Trim() == inkjet_chk && records[i].Status == "Connected")
                //{
                //    chk = true;
                //    continue;
                //}
                //else
                //{
                //    chk = false;
                //    break;
                //}
                if (records[i].InkJetName.Trim() == inkjet_chk)
                {
                    if (records[i].Status == "Connected")
                    {
                        chk = true;
                        continue;
                    }
                    else
                    {
                        chk = false;
                        break;
                    }
                }
            }
            return chk;
            
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
        public void Add_detail(string inkjet_id, string programs_id , string block_id , int start , string type)
        {
            List<string> ip_inkjet = get_inkjet_ip(inkjet_id);
            //try
            //{
            //    Console.WriteLine("ip_inkjet" + ip_inkjet[0]);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //    //MessageBox.Show(e.ToString());
            //}
            if (ip_inkjet.Count > 0)
            {
                if (ChkInkjet() == true && ip_inkjet[0] != "")
                {
                    IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(ip_inkjet[0]), 37022);

                    Socket sender = new Socket(localEndPoint.AddressFamily,
                               SocketType.Stream, ProtocolType.Tcp);

                    sender.Connect(localEndPoint);

                    byte[] messageSent_pro = Encoding.ASCII.GetBytes("FW," + programs_name + "\r");
                    int byteSent_pro = sender.Send(messageSent_pro);
                    byte[] messageReceived_pro = new byte[1024];
                    int byteRecv_pro = sender.Receive(messageReceived_pro);
                    var respone_pro = Encoding.ASCII.GetString(messageReceived_pro, 0, byteRecv_pro);


                    byte[] messageSent_qty = Encoding.ASCII.GetBytes("KH,3\r");
                    int byteSent_qty = sender.Send(messageSent_qty);
                    byte[] messageReceived_qty_current = new byte[1024];
                    int byteRecv_qty_current = sender.Receive(messageReceived_qty_current);
                    var respone_qty_current = Encoding.ASCII.GetString(messageReceived_qty_current, 0, byteRecv_qty_current);
                    var respone_qty_current_number = respone_qty_current.Split(',').ToList();

                    //Console.WriteLine(respone_qty_current_number[2]);

                    //Console.WriteLine(current_qty);


                    if (Int32.Parse(respone_qty_current_number[2]) != current_qty)
                    {
                        if (start < list_csv.Count)
                        {

                            //string command = "FS," + programs_id + "," + block_id + ",0," + list_csv[start].Detail + "\r";
                            //string command = "BR," + list_csv[start].Detail + "\r";
                            string command;
                            if (type == "String")
                            {
                                 command = "FS," + programs_id + "," + block_id + ",0," + list_csv[start].Detail + "\r";
                            }
                            else
                            {
                                 command = "BE,"+ programs_id + ","+ block_id + "," + list_csv[start].Detail + "\r";
                            }
                            //Console.WriteLine(command);



                            byte[] messageSent_status = Encoding.ASCII.GetBytes(command);
                            Console.WriteLine(command);
                            int byteSent_status = sender.Send(messageSent_status);
                            byte[] messageReceived_status = new byte[1024];
                            int byteRecv_status = sender.Receive(messageReceived_status);
                            var respone_status = Encoding.ASCII.GetString(messageReceived_status, 0, byteRecv_status);

                        }
                        if (start == 0)
                        {
                            guna2TextBox2.Text = "--";
                            guna2TextBox3.Text = list_csv[start].Detail;
                            guna2TextBox4.Text = 0 + "/" + list_csv.Count;
                            sim_data = sim_data + 1;
                        }
                        else if (start < list_csv.Count)
                        {
                            guna2TextBox2.Text = list_csv[start - 1].Detail;
                            guna2TextBox3.Text = list_csv[start].Detail;
                            guna2TextBox4.Text = start + "/" + list_csv.Count;
                            sim_data = sim_data + 1;
                        }
                        else
                        {
                            x.Stop();
                            guna2TextBox2.Text = list_csv[start - 1].Detail;
                            guna2TextBox3.Text = "";
                            guna2TextBox4.Text = start + "/" + list_csv.Count;
                            //On_Off_printer(inkjet_id, "SQ");
                            MessageBox.Show("Success");
                        }
                        current_qty = Int32.Parse(respone_qty_current_number[2]);
                    }
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                else
                {
                    x.Stop();
                    MessageBox.Show("Inkjet is not contected !");
                }
            }
        }


        public void chk_qty_change()
        {
            string inkjet_id = inkjet_name;
            string programs_id = programs_name;
            string block_id = block_name;
            string type = type_s;
            int start = sim_data ;
           
            Add_detail(inkjet_id, programs_id, block_id , start , type);         
        }

        public static void On_Off_printer(string inkjet_id , string command)
        {
            List<string> ip_inkjet = get_inkjet_ip(inkjet_id);

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(ip_inkjet[0]), 37022);

            Socket sender = new Socket(localEndPoint.AddressFamily,
                       SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(localEndPoint);

            byte[] messageSent_status = Encoding.ASCII.GetBytes(command+"\r");
            int byteSent_status = sender.Send(messageSent_status);
            byte[] messageReceived_status = new byte[1024];
            int byteRecv_status = sender.Receive(messageReceived_status);
            var respone_status = Encoding.ASCII.GetString(messageReceived_status, 0, byteRecv_status);

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

        private void guna2TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void guna2TextBox6_KeyPress(object sender, KeyPressEventArgs e)
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
                    get_name_programs(ip_inkjet[0], program);
                }
            }
        }

        public void get_name_programs(string id,int program)
        {
            var ping = new Ping();
            var reply = ping.Send(id, 60 * 10); // 1 minute time out (in ms)

            if (reply.Address != null)
            {

                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(id), 37022);

                Socket sender = new Socket(localEndPoint.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                sender.Connect(localEndPoint);

                var respone_programs_current_number_1 = "FL," + program + ",0";

                byte[] messageSent_programs = Encoding.ASCII.GetBytes(respone_programs_current_number_1 + "\r");
                int byteSent_programs = sender.Send(messageSent_programs);
                byte[] messageReceived_programs = new byte[1024];
                int byteRecv_programs = sender.Receive(messageReceived_programs);
                var respone_programs = Encoding.ASCII.GetString(messageReceived_programs, 0, byteRecv_programs);
                var respone_programs_list = respone_programs.Split(',').ToList();

                Console.WriteLine(respone_programs_list[3]);

                Console.WriteLine("FL," + program + ",0");
                guna2HtmlLabel5.Text = respone_programs_list[3];
                guna2HtmlLabel5.Visible = true;

            }
            else
            {
                guna2HtmlLabel5.Visible = false;
            }


        }

        private void guna2TextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            if (guna2TextBox6.Text != "")
            {
                int block_no = Int32.Parse(guna2TextBox6.Text);
                string type = guna2ComboBox2.Text;
                //Console.WriteLine(type);
                if (type == "String")
                {
                    if (block_no > 256)
                    {
                        MessageBox.Show("Block No. is out of rang ! more then 256");
                        guna2TextBox6.Text = "";
                    }
                }
                else
                {
                    if (block_no > 4)
                    {
                        MessageBox.Show("Block No. is out of rang ! more then 4");
                        guna2TextBox6.Text = "";
                    }
                }
            }
        }

        private void guna2ComboBox1_DropDownClosed(object sender, EventArgs e)
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
                    get_name_programs(ip_inkjet[0], program);
                }
            }
        }
    }
}
