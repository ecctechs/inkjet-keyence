// A C# program for Client
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using inkjet.Class;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using static Guna.UI2.Native.WinApi;
using System.Net.NetworkInformation;
using System.Dynamic;
//using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq.Expressions;
using CsvHelper.Configuration;
using System.Linq.Dynamic;


namespace client
{
    public class Program
    {
        private static string old_programs = "";

        public void set_programs(String name)
        {
            old_programs = name;
        }
        public static string get_programs()
        {
            return old_programs;
        }

        public void Execute_Client()
        {          
            ExecuteClient();
        }

        public async void ExecuteClient()
        {
 
            List<Error> records_error = new List<Error>();
            List<Inkjet> records_update = new List<Inkjet>();
            List<Datalog> records_datalog = new List<Datalog>();
            string new_status;

            List<Inkjet> records = Inkjet.ListInkjet();
            //List<Inkjet> records = inkjet_list;
            for (int y = 0; y < records.Count; ++y)
            {
                try
            {
                    string serverIP = records[y].IPAdress;
                    int port = 37022;

                    var ping = new Ping();
                    var reply = ping.Send(records[y].IPAdress, 60 * 10); // 1 minute time out (in ms)
                   

                    IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);

                    Socket sender = new Socket(localEndPoint.AddressFamily,
                               SocketType.Stream, ProtocolType.Tcp);

                    Console.WriteLine("Status :  " + reply.Status + " || Time : " + reply.RoundtripTime.ToString() + " || Address : " + reply.Address);
                    Console.WriteLine("---------------------------------------------");


                    if (reply.Address != null)
                    {
                        try
                        {
                            //Connect Socket to the remote
                            //endpoint using method Connect()
                            sender.Connect(localEndPoint);

                            string input = sender.RemoteEndPoint.ToString();
                            int index = input.IndexOf(":");
                            if (index >= 0)
                                input = input.Substring(0, index);


                            //We print EndPoint information
                            // that we are connected
                            //Console.WriteLine("Socket connected to -> {0} ",
                            //              sender.RemoteEndPoint.ToString());

                            //Creation of message that
                            // we will send to Server
                            byte[] messageSent_status = Encoding.ASCII.GetBytes("SB\r");
                            int byteSent_status = sender.Send(messageSent_status);
                            byte[] messageReceived_status = new byte[1024];
                            int byteRecv_status = sender.Receive(messageReceived_status);
                            var respone_status = Encoding.ASCII.GetString(messageReceived_status, 0, byteRecv_status);


                            byte[] messageSent_programs_current = Encoding.ASCII.GetBytes("FR\r");
                            int byteSent_programs_current = sender.Send(messageSent_programs_current);
                            byte[] messageReceived_programs_current = new byte[1024];
                            int byteRecv_programs_current = sender.Receive(messageReceived_programs_current);
                            var respone_programs_current = Encoding.ASCII.GetString(messageReceived_programs_current, 0, byteRecv_programs_current);
                            var respone_programs_current_number = respone_programs_current.Split(',').ToList();
                            var respone_programs_current_number_1 = "FL," + respone_programs_current_number[1].Trim() + ",0";

                            byte[] messageSent_programs = Encoding.ASCII.GetBytes(respone_programs_current_number_1 + "\r");        
                            int byteSent_programs = sender.Send(messageSent_programs);
                            byte[] messageReceived_programs = new byte[1024];
                            int byteRecv_programs = sender.Receive(messageReceived_programs);
                            var respone_programs = Encoding.ASCII.GetString(messageReceived_programs, 0, byteRecv_programs);
                            var respone_programs_list = respone_programs.Split(',').ToList();


                            byte[] messageSent_error = Encoding.ASCII.GetBytes("EV\r");
                            int byteSent_error = sender.Send(messageSent_error);
                            byte[] messageReceived_error = new byte[1024];
                            int byteRecv_error = sender.Receive(messageReceived_error);
                            var respone_error = Encoding.ASCII.GetString(messageReceived_error, 0, byteRecv_error);
                            var respone_error_list = respone_error.Split(',').ToList();

                            string ink_status = "";
                            string solvent_status = "";
                            string pump_status = "";
                            string filter_status = "";
                   
                            for (int i = 1; i < respone_error_list.Count; i++)
                            {
                                if (respone_error_list[i] == "101")
                                {
                                    ink_status = "Empty";
                                }
                                else if (respone_error_list[i] == "140")
                                {
                                    ink_status = "Low";
                                }
                                else
                                {
                                    ink_status = "Normal";
                                }

                                if (respone_error_list[i] == "102")
                                {
                                    solvent_status = "Empty";
                                }
                                else if (respone_error_list[i] == "141")
                                {
                                    solvent_status = "Low";
                                }
                                else
                                {
                                    solvent_status = "Normal";
                                }

                                if (respone_error_list[i] == "25")
                                {
                                    pump_status = "Error";
                                }
                                else if (respone_error_list[i] == "117")
                                {
                                    pump_status = "Warning";
                                }
                                else
                                {
                                    pump_status = "Normal";
                                }

                                if (respone_error_list[i] == "32" || respone_error_list[i] == "34")
                                {
                                    filter_status = "Error";
                                }
                                else if (respone_error_list[i] == "129" || respone_error_list[i] == "131")
                                {
                                    filter_status = "Warning";
                                }
                                else
                                {
                                    filter_status = "Normal";
                                }
                                var date_error = DateTime.Now.ToString("d/M/yyyy");
                                var time_error = DateTime.Now.ToString("HH:mm");
                                int Error_Code = Int32.Parse(respone_error_list[i]);
                                string shift_text = Shift.Update_Shift(time_error);
                                
                                records_error.Add(new Error { InkJet = records[y].InkJetName, ErrorType = "Error", ErrorCode = Error_Code, Detail = respone_error_list[i], Date = date_error, Time = time_error, Shift = shift_text, InKLevel = "InKLevel", SolventLevel = solvent_status, PumpLifetime = pump_status, FilterLifetime = filter_status, Status = "0" });                             
                            }

                            if (respone_error_list.Count == 1)
                            {
                                ink_status = "Normal";
                                solvent_status = "Normal";
                                pump_status = "Normal";
                                filter_status = "Normal";
                            }

                            if (respone_programs_list[3] != get_programs() && get_programs() != "")
                            {
                                var date_log = DateTime.Now.ToString("d/M/yyyy HH:mm");
                                string shift_text = Shift.Update_Shift(date_log);
                                //var date_now = DateTime.Now.ToString("d/M/yyyy HH:mm");
                                Console.WriteLine("Change!!!");
                                records_datalog.Add(new Datalog { InkJet = records[y].InkJetName, Program = get_programs(), Qty = "99999" , DateStart = date_log, DateEnd = date_log, Shift = shift_text });
                                Datalog.Add_DataLog(records_datalog);
                            }

                            
                            new_status = "Connected";
                            records_update.Add(new Inkjet { IPAdress = input, Status = new_status ,Status_inkjet = respone_status ,Program = respone_programs_list[3] , Ink = ink_status , Solvent = solvent_status , Pump = pump_status , Filter = filter_status });

                            //Console.WriteLine("Message from Server -> {0}",
                            //      Encoding.ASCII.GetString(messageReceived,
                            //                                 0, byteRecv));
                           

                            set_programs(respone_programs_list[3]);

                          
                            //Close Socket using
                            //the method Close()
                            sender.Shutdown(SocketShutdown.Both);
                            sender.Close();
                        }


                        // Manage of Socket's Exceptions
                        catch (ArgumentNullException ane)
                        {
                            Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                        }

                        catch (SocketException se)
                        {
                            Console.WriteLine("SocketException : {0}", se.ToString());

                        }

                        catch (Exception e)
                        {
                            Console.WriteLine("Unexpected exception : {0}", e.ToString());
                        }

                    }
                    else {
                        new_status = "Not Connected";
                        records_update.Add(new Inkjet { IPAdress = "0", Status = new_status , Status_inkjet = "Error" , Program = "" });
                    }

                }
            catch (Exception e)
            {
                new_status = "Not Connected";
                Console.WriteLine(e.ToString());
                records_update.Add(new Inkjet { IPAdress = "0", Status = new_status , Status_inkjet = "Error" , Program = "" });
            }
        }

            UpdateInkjetStatus(records_update);
            UpdateErrorHistry(records_error);
        }
        public static void UpdateInkjetStatus(List<Inkjet> p) 
        {
            //List<Inkjet> records = inkjet_list;
            List<Inkjet> records = Inkjet.ListInkjet();

            for (int i = 0; i < p.Count; ++i)
                {
                    if (p[i].IPAdress == records[i].IPAdress)
                    {
                        if (p[i].Status != records[i].Status)
                        {
                            SendEmail(records[i].IPAdress, "Connected");
                        }
                        records[i].Status = "Connected";
                        records[i].Status_inkjet = (p[i].Status_inkjet == "SB,01\r") ? "Printable" : "Stopped";
                        records[i].Program = p[i].Program;
                        records[i].Ink = p[i].Ink;
                        records[i].Solvent = p[i].Solvent;
                        records[i].Pump = p[i].Pump;
                        records[i].Filter = p[i].Filter;

                    }
                    else
                    {
                        if (p[i].Status != records[i].Status)
                        {
                            SendEmail(records[i].IPAdress, "Not Connected");
                        }
                        records[i].Status = "Not Connected";
                        records[i].Status_inkjet = p[i].Status_inkjet;
                        records[i].Program = p[i].Program;
                        records[i].Ink = "";
                        records[i].Solvent = "";
                        records[i].Pump = "";
                        records[i].Filter = "";
                    }
                                    
                        records[i].InkJetID = records[i].InkJetID;
                        records[i].InkJetName = records[i].InkJetName;
                        records[i].IPAdress = records[i].IPAdress;                                                          
                }
            Inkjet.Update_Inkjet(records);
        }

        public static void UpdateErrorHistry(List<Error> p)
        {

            // เช็คใน error แต่ละ row ว่ามี error code กับ status เป็น false ไหม ถ้ามี ให้ไป update row นั้นเป็น false
            // แต่ถ้าไม่มี error ตัวนั้น ให้ไป update status เป็น true
          
            List<Error> records = Error.ListError();
            List<Error> update_list = new List<Error>();
            var potentialCandidates = records.Except(p, new ErrorComparer.PersonComparer());

            foreach (var candidate in potentialCandidates)
            {
                //Console.WriteLine(candidate.ErrorCode);              
                update_list.Add(candidate);
               
            }
            for(int i = 0; i < update_list.Count; i++)
            {
                //Console.WriteLine(update_list[i].ErrorCode);
                if (update_list[i].Status == "0")
                {
                    update_list[i].Status = "1";
                }
            }

            var innerJoin = from s in records // outer sequence
                            join st in p //inner sequence 
                            on s.ErrorType equals st.ErrorType // key selector 
                            select new
                            { // result selector 
                                InkJet = st.InkJet,
                                ErrorType = s.ErrorType,
                                ErrorCode = s.ErrorCode,
                                ErrorCode2 = st.ErrorCode,
                                Detail = s.Detail,
                                Date = s.Date,
                                Time = s.Time,
                                Shift = s.Shift,
                                InKLevel = s.InKLevel,
                                SolventLevel = s.SolventLevel,
                                PumpLifetime = s.PumpLifetime,
                                FilterLifetime = s.FilterLifetime,
                                Status = st.Status,
                                Status2 = s.Status,
                            };

            foreach (var e in innerJoin)
            {
                if (e.Status2 == "0" && e.ErrorCode == e.ErrorCode2)
                {
                    var itemToRemove = p.Single(r => r.ErrorCode == e.ErrorCode);
                    p.Remove(itemToRemove);
                    
                }
                
            }
            Error.Update_Error(records);
            Error.Add_Error(p);
        }

        public static void SendEmail(string ip,string status)
        {
            List<Email> emails = Email.ListEmail();

            if (emails.Count > 0)
            {
                for (int i = 0; i < emails.Count; i++)
                {
                    send_email.Send_Email email = new send_email.Send_Email();
                    //string name = "theerasak789900@gmail.com";
                    string name = emails[i].EmailName;
                    string subject = "Inkjet " + ip + "Status";
                    string detail = status;
                    email.send(name, subject, detail);
                }
            }
        }
    }
}
