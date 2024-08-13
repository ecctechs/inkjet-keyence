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
using ECCLibary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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

        private int count_temp = 0; // ประกาศตัวแปร count_temp ที่ใช้เก็บค่านับ
        private static int current_list = 0;

        private Socket sender; // ประกาศตัวแปร sender เป็นตัวแทนของคลาส Socket

        private System.Windows.Forms.Timer x = new System.Windows.Forms.Timer();

        private static List<CsvMarkingCreateCSV> record_success = new List<CsvMarkingCreateCSV>().ToList();
        private static List<CsvMarkingCreateCSV> record_not_finish = new List<CsvMarkingCreateCSV>().ToList();


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
            //command_count();
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
            else if (guna2TextBox5.Text == "")
            {
                MessageBox.Show("Please fill Programs number");
            }
            else if (guna2TextBox6.Text == "")
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
                Add_detail(inkjet_id, programs_id, block_id, start, type);
                On_Off_printer(inkjet_id, "SQ");
            }
            //start_prinrt();

        }

        public void start_prinrt()
        {
            inkjet_name = guna2ComboBox1.Text;
            programs_name = guna2TextBox5.Text;
            block_name = guna2TextBox6.Text;
            type_s = guna2ComboBox2.Text;
            string inkjet_id = guna2ComboBox1.Text;
            List<string> ip_inkjet = get_inkjet_ip(inkjet_id);


            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("กรุณาอัพโหลดไฟล์ CSV");
            }
            else if (ChkInkjet() == false)
            {
                MessageBox.Show("Inkjet status is not Printable");
            }
            else if (guna2TextBox5.Text == "")
            {
                MessageBox.Show("Please fill Programs number");
            }
            else if (guna2TextBox6.Text == "")
            {
                MessageBox.Show("Please fill Block No.");
            }
            else
            {
                if (ip_inkjet.Count > 0)
                {
                    //IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(ip_inkjet[0]), 37022);

                    //Socket sender = new Socket(localEndPoint.AddressFamily,
                    //           SocketType.Stream, ProtocolType.Tcp);

                    //sender.Connect(localEndPoint);

                    // สร้าง IPEndPoint จาก IP address และ port ที่ต้องการ
                    IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(ip_inkjet[0]), 37022);

                    // สร้าง socket และกำหนดค่าต่าง ๆ ตามที่ต้องการ
                    sender = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    // เชื่อมต่อ socket ไปยัง localEndPoint
                    sender.Connect(localEndPoint);


                    byte[] messageSent_pro = Encoding.ASCII.GetBytes("FW," + programs_name + "\r");
                    int byteSent_pro = sender.Send(messageSent_pro);
                    byte[] messageReceived_pro = new byte[1024];
                    int byteRecv_pro = sender.Receive(messageReceived_pro);
                    var respone_pro = Encoding.ASCII.GetString(messageReceived_pro, 0, byteRecv_pro);

                    On_Off_printer(inkjet_id, "SQ");

                    //int count = command_count();
                    //Console.WriteLine(count);

                    StartTimer();




                }
                else
                {
                    MessageBox.Show("ไม่พบ inkjet");
                }

            }
        }

        public int command_count()
        {
            try
            {
                // ส่งข้อมูล "KH,3\r" ผ่าน socket
                byte[] messageSent_qty = Encoding.ASCII.GetBytes("KH,3\r");
                int byteSent_qty = sender.Send(messageSent_qty);

                // รับข้อมูลที่ตอบกลับมาจาก socket
                byte[] messageReceived_qty_current = new byte[1024];
                int byteRecv_qty_current = sender.Receive(messageReceived_qty_current);
                string response_qty_current = Encoding.ASCII.GetString(messageReceived_qty_current, 0, byteRecv_qty_current);

                // แยกข้อมูลที่ได้รับมาด้วยการ split โดยใช้ ',' เป็นตัวแยก
                var response_qty_current_number = response_qty_current.Split(',').ToList();

                // ดึงค่า respone_qty_current_number[2] และบวก 1 เพื่อนำไปใช้งาน
                count_temp = count_temp ;

                txtCount.Text = count_temp.ToString();

                Console.WriteLine("count -->"+ count_temp);

                return count_temp;
            }
            catch (Exception ex)
            {
                // กรณีเกิดข้อผิดพลาดในการสื่อสารผ่าน socket
                MessageBox.Show("An error occurred: " + ex.Message);
                return -1; // หรือคืนค่าอื่น ๆ ที่ต้องการเมื่อเกิดข้อผิดพลาด
            }
        }

        // เริ่มต้นการทำงานของ Timer
        public void StartTimer()
        {
            Timer_sim();
            //x.Start();
        }

        // หยุดการทำงานของ Timer
        public void StopTimer()
        {
            x.Stop();
        }

        // เมื่อไม่ต้องการใช้ sender อีกต่อไป สามารถปิดการเชื่อมต่อ socket ได้
        public void CloseConnection()
        {
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
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

         
            if (guna2ComboBox1.Items.Count == 0)
            {
                MessageBox.Show("กรูณาเพิ่มเครื่อง inkjet ก่อน");
                guna2TextBox1.Text = "";
            }
            else
            {
                guna2ComboBox1.SelectedIndex = 0;
            }

            guna2TextBox5.Text = "";
            guna2TextBox6.Text = "";
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
        }

        //private void CsvButtonStop_Click(object sender, EventArgs e)
        //{
        //    x.Stop();
        //    guna2ComboBox1.Enabled = true;
        //    guna2TextBox5.ReadOnly = false;
        //    guna2TextBox6.ReadOnly = false;
        //    guna2ComboBox2.Enabled = true;
        //    guna2TextBox5.FillColor = Color.White;
        //    guna2TextBox6.FillColor = Color.White;
        //    CsvButtonStart.Show();
        //    CsvButtonStop.Hide();
        //    string inkjet_id = guna2ComboBox1.Text;
        //    On_Off_printer(inkjet_id, "SR");
        //    csvMarking_chk.set_running(false);

        //    using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
        //    {
        //        if (sfd.ShowDialog() == DialogResult.OK)
        //        {
        //            using (var sw = new StreamWriter(sfd.FileName))
        //            {
        //                using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
        //                {
        //                    csv.WriteHeader(typeof(CsvMarkingCreateCSV));
        //                    foreach (CsvMarkingCreateCSV s in record_success as List<CsvMarkingCreateCSV>)
        //                    {
        //                        csv.NextRecord();
        //                        csv.WriteRecord(s);
        //                    }
        //                }
        //            }
        //            MessageBox.Show(this, "Your data has been successfully export.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    int count_success = record_success.Count();
        //    int count_list_csv = list_csv.Count;
        //    DateTime st = DateTime.Now.AddYears(-543);
        //    string date_now = st.AddSeconds(-st.Second).ToString();

        //    for (int i = 0; count_success < count_list_csv; i++)
        //    {
        //        CsvMarkingCreateCSV newRecord = new CsvMarkingCreateCSV
        //        {
        //            Detail = list_csv[count_success].Detail,
        //            inkjet_name = inkjet_id,
        //            Timestamp = date_now // Example date value
        //        };

        //        record_not_finish.Add(newRecord);
        //    }


        //    using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
        //    {
        //        if (sfd.ShowDialog() == DialogResult.OK)
        //        {
        //            using (var sw = new StreamWriter(sfd.FileName))
        //            {
        //                using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
        //                {
        //                    csv.WriteHeader(typeof(CsvMarkingCreateCSV));
        //                    foreach (CsvMarkingCreateCSV s in record_not_finish as List<CsvMarkingCreateCSV>)
        //                    {
        //                        csv.NextRecord();
        //                        csv.WriteRecord(s);
        //                    }
        //                }
        //            }
        //            MessageBox.Show(this, "Your data has been successfully export.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //}

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

            // Export record_success to CSV file on Desktop
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileNameSuccess = Path.Combine(desktopPath, "record_success.csv");

            using (var swSuccess = new StreamWriter(fileNameSuccess))
            using (var csvSuccess = new CsvWriter(swSuccess, CultureInfo.InvariantCulture))
            {
                csvSuccess.WriteHeader<CsvMarkingCreateCSV>();
                csvSuccess.NextRecord();
                foreach (var s in record_success)
                {
                    csvSuccess.WriteRecord(s);
                    csvSuccess.NextRecord();
                }
            }
            MessageBox.Show(this, "Your data has been successfully exported to record_success.csv on Desktop.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Export record_not_finish to CSV file on Desktop
            string fileNameNotFinish = Path.Combine(desktopPath, "record_not_finish.csv");

            int count_success = record_success.Count;
            int count_list_csv = list_csv.Count;
            DateTime st = DateTime.Now.AddYears(-543);
            string date_now = st.AddSeconds(-st.Second).ToString();

            for (int i = count_success; i < count_list_csv; i++)
            {
                CsvMarkingCreateCSV newRecord = new CsvMarkingCreateCSV
                {
                    Detail = list_csv[i].Detail,
                    inkjet_name = inkjet_id,
                    Timestamp = "---" // Example date value
                };

                record_not_finish.Add(newRecord);
            }

            using (var swNotFinish = new StreamWriter(fileNameNotFinish))
            using (var csvNotFinish = new CsvWriter(swNotFinish, CultureInfo.InvariantCulture))
            {
                csvNotFinish.WriteHeader<CsvMarkingCreateCSV>();
                csvNotFinish.NextRecord();
                foreach (var s in record_not_finish)
                {
                    csvNotFinish.WriteRecord(s);
                    csvNotFinish.NextRecord();
                }
            }
            MessageBox.Show(this, "Your data has been successfully exported to record_not_finish.csv on Desktop.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void Add_detail(string inkjet_id, string programs_id, string block_id, int start, string type)
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

                    //Console.WriteLine("--->" + count_temp);

                    //start = count_temp;
                    txtCount.Text = respone_qty_current_number[2];

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
                                command = "BE," + programs_id + "," + block_id + "," + list_csv[start].Detail + "\r";
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


                            DateTime st = DateTime.Now.AddYears(-543);
                            string date_now = st.AddSeconds(-st.Second).ToString();

                            //CsvMarkingCreateCSV newRecord = new CsvMarkingCreateCSV
                            //{
                            //    Detail = list_csv[start].Detail,
                            //    inkjet_name = inkjet_id,
                            //    Timestamp = date_now // Example date value
                            //};

                            //record_success.Add(newRecord);
                        }
                        else if (start < list_csv.Count)
                        {
                            guna2TextBox2.Text = list_csv[start - 1].Detail;
                            guna2TextBox3.Text = list_csv[start].Detail;
                            guna2TextBox4.Text = start + "/" + list_csv.Count;
                            sim_data = sim_data + 1;

                            DateTime st = DateTime.Now.AddYears(-543);
                            string date_now = st.AddSeconds(-st.Second).ToString();

                            CsvMarkingCreateCSV newRecord = new CsvMarkingCreateCSV
                            {
                                Detail = list_csv[start - 1].Detail,
                                inkjet_name = inkjet_id,
                                Timestamp = date_now // Example date value
                            };

                            record_success.Add(newRecord);

                        }
                        else
                        {
                            x.Stop();
                            guna2TextBox2.Text = list_csv[start - 1].Detail;
                            guna2TextBox3.Text = "";
                            guna2TextBox4.Text = start + "/" + list_csv.Count;
                            //On_Off_printer(inkjet_id, "SQ");
                            MessageBox.Show("Success");
                            //sim_data = 0;
                            //start = 0;
                        }

                        current_qty = Int32.Parse(respone_qty_current_number[2]);
                    }
                    count_temp = count_temp + 1;
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

        //public void Add_detail(string inkjet_id, string programs_id, string block_id, int start, string type)
        //{
        //    List<string> ip_inkjet = get_inkjet_ip(inkjet_id);
        //    if (ip_inkjet.Count > 0)
        //    {
        //        if (ChkInkjet() == true && ip_inkjet[0] != "")
        //        {
        //            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(ip_inkjet[0]), 37022);

        //            Socket sender = new Socket(localEndPoint.AddressFamily,
        //                       SocketType.Stream, ProtocolType.Tcp);

        //            sender.Connect(localEndPoint);

        //            byte[] messageSent_pro = Encoding.ASCII.GetBytes("FW," + programs_name + "\r");
        //            int byteSent_pro = sender.Send(messageSent_pro);
        //            byte[] messageReceived_pro = new byte[1024];
        //            int byteRecv_pro = sender.Receive(messageReceived_pro);
        //            var respone_pro = Encoding.ASCII.GetString(messageReceived_pro, 0, byteRecv_pro);


        //            byte[] messageSent_qty = Encoding.ASCII.GetBytes("KH,3\r");
        //            int byteSent_qty = sender.Send(messageSent_qty);
        //            byte[] messageReceived_qty_current = new byte[1024];
        //            int byteRecv_qty_current = sender.Receive(messageReceived_qty_current);
        //            var respone_qty_current = Encoding.ASCII.GetString(messageReceived_qty_current, 0, byteRecv_qty_current);
        //            var respone_qty_current_number = respone_qty_current.Split(',').ToList();

        //            //Console.WriteLine(respone_qty_current_number[2]);

        //            //Console.WriteLine("--->" + count_temp);

        //            txtCount.Text = current_list.ToString() ;
        //            int current_count = Int32.Parse(txtCount.Text);

        //            Console.WriteLine("----------------------<<>>>"+current_count);

        //            if (list_csv.Count > 0)
        //            {
        //                if (current_list == 0)
        //                {
        //                    guna2TextBox2.Text = "--";
        //                    guna2TextBox3.Text = list_csv[current_list].Detail;
        //                    guna2TextBox4.Text = 0 + "/" + list_csv.Count;
        //                }
        //                else if(current_list > 0)
        //                {
        //                    current_list = current_list + 1;
        //                    guna2TextBox2.Text = list_csv[current_list - 1].Detail;
        //                    guna2TextBox3.Text = list_csv[current_list].Detail;
        //                    guna2TextBox4.Text = current_list + "/" + list_csv.Count;
        //                }
        //            }



        //            //    if (Int32.Parse(respone_qty_current_number[2]) != current_qty)
        //            //    {
        //            //        if (start < list_csv.Count)
        //            //        {
        //            //            string command;
        //            //            if (type == "String")
        //            //            {
        //            //                command = "FS," + programs_id + "," + block_id + ",0," + list_csv[start].Detail + "\r";
        //            //            }
        //            //            else
        //            //            {
        //            //                command = "BE," + programs_id + "," + block_id + "," + list_csv[start].Detail + "\r";
        //            //            }

        //            //            byte[] messageSent_status = Encoding.ASCII.GetBytes(command);
        //            //            Console.WriteLine(command);
        //            //            int byteSent_status = sender.Send(messageSent_status);
        //            //            byte[] messageReceived_status = new byte[1024];
        //            //            int byteRecv_status = sender.Receive(messageReceived_status);
        //            //            var respone_status = Encoding.ASCII.GetString(messageReceived_status, 0, byteRecv_status);
        //            //        }
        //            //        if (start == 0)
        //            //        {
        //            //            guna2TextBox2.Text = "--";
        //            //            guna2TextBox3.Text = list_csv[start].Detail;
        //            //            guna2TextBox4.Text = 0 + "/" + list_csv.Count;
        //            //            sim_data = sim_data + 1;

        //            //            DateTime st = DateTime.Now.AddYears(-543);
        //            //            string date_now = st.AddSeconds(-st.Second).ToString();

        //            //            CsvMarkingCreateCSV newRecord = new CsvMarkingCreateCSV
        //            //            {
        //            //                Detail = list_csv[start].Detail,
        //            //                inkjet_name = inkjet_id,
        //            //                Timestamp = date_now // Example date value
        //            //            };

        //            //            record_success.Add(newRecord);
        //            //        }
        //            //        else if (start < list_csv.Count)
        //            //        {
        //            //            guna2TextBox2.Text = list_csv[start - 1].Detail;
        //            //            guna2TextBox3.Text = list_csv[start].Detail;
        //            //            guna2TextBox4.Text = start + "/" + list_csv.Count;
        //            //            sim_data = sim_data + 1;

        //            //            DateTime st = DateTime.Now.AddYears(-543);
        //            //            string date_now = st.AddSeconds(-st.Second).ToString();

        //            //            CsvMarkingCreateCSV newRecord = new CsvMarkingCreateCSV
        //            //            {
        //            //                Detail = list_csv[start - 1].Detail,
        //            //                inkjet_name = inkjet_id,
        //            //                Timestamp = date_now // Example date value
        //            //            };

        //            //            record_success.Add(newRecord);
        //            //        }
        //            //        else
        //            //        {
        //            //            x.Stop();
        //            //            guna2TextBox2.Text = list_csv[start - 1].Detail;
        //            //            guna2TextBox3.Text = "";
        //            //            guna2TextBox4.Text = start + "/" + list_csv.Count;
        //            //            //On_Off_printer(inkjet_id, "SQ");
        //            //            MessageBox.Show("Success");
        //            //            //sim_data = 0;
        //            //            //start = 0;
        //            //        }

        //            //        current_qty = Int32.Parse(respone_qty_current_number[2]);
        //            //    }
        //            //    //count_temp = count_temp + 1;
        //            //    sender.Shutdown(SocketShutdown.Both);
        //            //    sender.Close();
        //            //}
        //            //else
        //            //{
        //            x.Stop();
        //            MessageBox.Show("Inkjet is not contected !");
        //        }
        //    }
        //}


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

  

        private void txtCount_TextChanged(object sender, EventArgs e)
        {
            //string inkjet_name = guna2ComboBox1.Text;
            //string programs_id = guna2TextBox5.Text;
            //string block_id = guna2TextBox6.Text;
            //string type = guna2ComboBox2.Text;
            //string inkjet_id = guna2ComboBox1.Text;

            //MessageBox.Show("change");
      

            //// Assuming dataGridView1 is your DataGridView control
            //foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            //{
            //    if (!row.IsNewRow) // Check if the row is not the new row for adding data
            //    {
            //        // Access the cell in the "detail" column
            //        DataGridViewCell cell = row.Cells["Detail"];

            //        // Check if the cell value is not null
            //        if (cell.Value != null)
            //        {
            //            string detailValue = cell.Value.ToString();
            //            Console.WriteLine("Detail value: " + detailValue);

            //            // You can perform any operation with the detailValue here
            //        }
            //    }
            //}

            //string command;
            //if (type == "String")
            //{
            //    command = "FS," + programs_id + "," + block_id + ",0," + list_csv[start].Detail + "\r";
            //}
            //else
            //{
            //    command = "BE," + programs_id + "," + block_id + "," + list_csv[start].Detail + "\r";
            //}
            ////Console.WriteLine(command);



            //byte[] messageSent_status = Encoding.ASCII.GetBytes(command);
            //Console.WriteLine(command);
            //int byteSent_status = sender.Send(messageSent_status);
            //byte[] messageReceived_status = new byte[1024];
            //int byteRecv_status = sender.Receive(messageReceived_status);
            //var respone_status = Encoding.ASCII.GetString(messageReceived_status, 0, byteRecv_status);

             
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Keyence.ip_connection("192.168.0.2","37022");
            //Keyence ky = new Keyence();
            //Keyence.test_function();

        }
    }
}
