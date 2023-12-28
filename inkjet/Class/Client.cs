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
using System.Drawing;
using inkjet.UserControls;

namespace client
{
    public class Program
    {
        private static string old_programs = "";
        private static string old_status = "";

        public void set_programs(String name)
        {
            old_programs = name;
        }
        public static string get_programs()
        {
            return old_programs;
        }

        public void set_status(String name)
        {
            old_status = name;
        }
        public static string get_status()
        {
            return old_status;
        }

        public void Execute_Client()
        {          
            ExecuteClient();
        }

        public void ExecuteClient()
        {
            List<Error> records_error = new List<Error>();
            List<Inkjet> records_update = new List<Inkjet>();
            List<Datalog> records_datalog = new List<Datalog>();
            string new_status;

            List<Inkjet> records = Inkjet.ListInkjet();
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

                            //Console.WriteLine("--------------------------------" + respone_error_list[0]);
                            string ink_status = "Normal";
                            string solvent_status = "Normal";
                            string pump_status = "Normal";
                            string filter_status = "Normal";                         

                                for (int i = 1; i < respone_error_list.Count; i++)
                                {
                                    var date_error = DateTime.Now.ToString("d/M/yyyy");
                                    var time_error = DateTime.Now.ToString("HH:mm");
                                    int Error_Code = Int32.Parse(respone_error_list[i]);
                                    //int Error_Code = Int32.Parse("102");
                                    string shift_text = Shift.Update_Shift(time_error);
                                    List<Error> error_detail = ListError.Update_ErrorDetail(Error_Code);      

                                records_error.Add(new Error { InkJet = records[y].InkJetName, ErrorType = error_detail[0].ErrorType, ErrorCode = Error_Code, Detail = error_detail[0].Detail, Date = date_error, Time = time_error, Shift = shift_text, InKLevel = error_detail[0].InKLevel, SolventLevel = error_detail[0].SolventLevel, PumpLifetime = error_detail[0].PumpLifetime, FilterLifetime = error_detail[0].FilterLifetime, Status = "0" });
                                }

                            var real_status = (respone_status == "SB,01\r") ? "Printable" : "Stopped";             

                            string code_status = "";

                            if (records_error.Count > 0)
                            {
                                if (Int32.Parse(respone_error_list[1]) <= 99)
                                {
                                    code_status = "Error";
                                }
                                else
                                {
                                    code_status = "Warning";
                                }
                            }
                            else if (respone_status == "SB,04\r")
                            {
                                code_status = "Suspended";
                            }
                            else if (respone_status == "SB,05\r")
                            {
                                code_status = "Starting";
                            }
                            else if (respone_status == "SB,06\r")
                            {
                                code_status = "Shutting Down";
                            }
                            else if (respone_status == "SB,01\r")
                            {
                                code_status = "Printable";
                            }
                            else
                            {
                                code_status = "Stopped";
                            }

                            if (respone_programs_list[3] != get_programs() && get_programs() != "" && real_status == "Printable")
                            {
                                byte[] messageSent_qty = Encoding.ASCII.GetBytes("KH,3\r");
                                int byteSent_qty = sender.Send(messageSent_qty);
                                byte[] messageReceived_qty_current = new byte[1024];
                                int byteRecv_qty_current = sender.Receive(messageReceived_qty_current);
                                var respone_qty_current = Encoding.ASCII.GetString(messageReceived_qty_current, 0, byteRecv_qty_current);
                                var respone_qty_current_number = respone_qty_current.Split(',').ToList();

                                var date_log = DateTime.Now.ToString("d/M/yyyy HH:mm");
                                string shift_text = Shift.Update_Shift(date_log);
                                //var date_now = DateTime.Now.ToString("d/M/yyyy HH:mm");
                                Console.WriteLine("Change!!!");

                                records_datalog.Add(new Datalog { InkJet = records[y].InkJetName, Program = get_programs(), Qty = "777", DateStart = date_log, DateEnd = date_log, Shift = shift_text , qty_end = respone_qty_current_number[2] });
                                Update_Datalog(records_datalog);

                                records_datalog.Clear();
                                records_datalog.Add(new Datalog { InkJet = records[y].InkJetName, Program = respone_programs_list[3], Qty = "", DateStart = date_log, DateEnd = "", Shift = shift_text , qty_start = respone_qty_current_number[2] });
                                Datalog.Add_DataLog(records_datalog);

                                //records_datalog.Add(new Datalog { InkJet = records[y].InkJetName, Program = get_programs(), Qty = "777", DateStart = date_log, DateEnd = date_log, Shift = shift_text });
                                //Update_Datalog(records_datalog);
                            }
                         


                         


                            if (get_status() == "Stopped" && real_status == "Printable")
                            {
                                byte[] messageSent_qty = Encoding.ASCII.GetBytes("KH,3\r");
                                int byteSent_qty = sender.Send(messageSent_qty);
                                byte[] messageReceived_qty_current = new byte[1024];
                                int byteRecv_qty_current = sender.Receive(messageReceived_qty_current);
                                var respone_qty_current = Encoding.ASCII.GetString(messageReceived_qty_current, 0, byteRecv_qty_current);
                                var respone_qty_current_number = respone_qty_current.Split(',').ToList();


                                var date_log = DateTime.Now.ToString("d/M/yyyy HH:mm");
                                string shift_text = Shift.Update_Shift(date_log);
                                records_datalog.Add(new Datalog { InkJet = records[y].InkJetName, Program = get_programs(), Qty = "", DateStart = date_log, DateEnd = "", Shift = shift_text , qty_start = respone_qty_current_number[2] });
                                Datalog.Add_DataLog(records_datalog);
                            }
                           
                            //Console.WriteLine(get_status());
                            //Console.WriteLine(real_status);
                            if (get_status() == "Printable" && real_status == "Stopped")
                            {
                                byte[] messageSent_qty = Encoding.ASCII.GetBytes("KH,3\r");
                                int byteSent_qty = sender.Send(messageSent_qty);
                                byte[] messageReceived_qty_current = new byte[1024];
                                int byteRecv_qty_current = sender.Receive(messageReceived_qty_current);
                                var respone_qty_current = Encoding.ASCII.GetString(messageReceived_qty_current, 0, byteRecv_qty_current);
                                var respone_qty_current_number = respone_qty_current.Split(',').ToList();


                                var date_log = DateTime.Now.ToString("d/M/yyyy HH:mm");
                                string shift_text = Shift.Update_Shift(date_log);                               
                                records_datalog.Add(new Datalog { InkJet = records[y].InkJetName, Program = get_programs(), Qty = "", DateStart = date_log, DateEnd = date_log, Shift = shift_text ,qty_end = respone_qty_current_number[2] });
                                Update_Datalog(records_datalog);
                            }

                            new_status = "Connected";
                            records_update.Add(new Inkjet { IPAdress = input, Status = new_status ,Status_inkjet = code_status, Program = respone_programs_list[3] , Ink = ink_status , Solvent = solvent_status , Pump = pump_status , Filter = filter_status });

                            //Console.WriteLine("Message from Server -> {0}",
                            //      Encoding.ASCII.GetString(messageReceived,
                            //                                 0, byteRecv));
                           

                            set_programs(respone_programs_list[3]);
                            set_status(real_status);


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
            List<Inkjet> records = Inkjet.ListInkjet();


            for (int i = 0; i < p.Count; ++i)
                {
                    if (p[i].IPAdress == records[i].IPAdress)
                    {
                        if (p[i].Status != records[i].Status)
                        {
                            //SendEmail(records[i].IPAdress, "Connected");
                        }
                        records[i].Status = "Connected";

                    //Console.WriteLine("------------->>>>"+p[i].Status_inkjet);
                    //if (p[i].Status_inkjet == "SB,05\r")
                    //{
                    //    records[i].Status_inkjet = "Starting";
                    //}
                    //else if(p[i].Status_inkjet == "SB,06\r")
                    //{
                    //    records[i].Status_inkjet = "Shutting Down";
                    //}
                    //else if (p[i].Status_inkjet == "SB,01\r")
                    //{
                    //    records[i].Status_inkjet = "Printable";
                    //}
                    //else
                    //{
                    //    records[i].Status_inkjet = "Stopped";
                    //}
                    //records[i].Status_inkjet = (p[i].Status_inkjet == "SB,01\r") ? "Printable" : "Stopped";
                    records[i].Status_inkjet = p[i].Status_inkjet;
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
                            //SendEmail(records[i].IPAdress, "Not Connected");
                        }
                        records[i].Status = "Not Connected";
                    //records[i].Status_inkjet = p[i].Status_inkjet;
                        records[i].Status_inkjet = "Disconnect";
                        records[i].Program = p[i].Program;
                        records[i].Ink = "Disconnect";
                        records[i].Solvent = "Disconnect";
                        records[i].Pump = "Disconnect";
                        records[i].Filter = "Disconnect";
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

           
            if(p.Count > 0)
            {
                for(int i = 0; i < p.Count; i++)
                {
                    SendEmail(p[i].InkJet, p[i].ErrorType, p[i].ErrorCode, p[i].Detail, p[i].Date , p[i].Time );

                }   
            }
        }

        public static void SendEmail(string inkjetname,string type , int code, string detail_error , string date_error , string time_error)
        {
            List<Email> emails = Email.ListEmail();

            if (emails.Count > 0)
            {
                for (int i = 0; i < emails.Count; i++)
                {
                    //if (emails[i].ErrorID == 1 )
                    //{
                    //    send_email.Send_Email email = new send_email.Send_Email();
                    //    //string name = "theerasak789900@gmail.com";
                    //    string name = emails[i].EmailName;
                    //    string subject = "Alarm Error Code :" + code + " Inkjet Name :" + inkjetname;
                    //    string detail = "Inkjet Name :" + inkjetname + " || Error Type :" + type + " || Error Code :" + code + " || Error Detail" + detail_error + " || Date :" + date_error + time_error;
                    //    email.send(name, subject, detail);
                    //}
                    //else if (emails[i].ErrorID == 2 && code <= 99)
                    //{
                    //    send_email.Send_Email email = new send_email.Send_Email();
                    //    //string name = "theerasak789900@gmail.com";
                    //    string name = emails[i].EmailName;
                    //    string subject = "Alarm Error Code :" + code + " Inkjet Name :" + inkjetname;
                    //    string detail = "Inkjet Name :" + inkjetname + " || Error Type :" + type + " || Error Code :" + code + " || Error Detail" + detail_error + " || Date :" + date_error + time_error;
                    //    email.send(name, subject, detail);
                    //}
                    //else if (emails[i].ErrorID == 3 && code >= 100)
                    //{
                    //    send_email.Send_Email email = new send_email.Send_Email();
                    //    //string name = "theerasak789900@gmail.com";
                    //    string name = emails[i].EmailName;
                    //    string subject = "Alarm Error Code :" + code + " Inkjet Name :" + inkjetname;
                    //    string detail = "Inkjet Name :" + inkjetname + " || Error Type :" + type + " || Error Code :" + code + " || Error Detail" + detail_error + " || Date :" + date_error + time_error;
                    //    email.send(name, subject, detail);
                    //}
                    if (emails[i].ErrorID2 == false)
                    {
                        send_email.Send_Email email = new send_email.Send_Email();
                        //string name = "theerasak789900@gmail.com";
                        string name = emails[i].EmailName;
                        string subject = "Alarm Error Code :" + code + " Inkjet Name :" + inkjetname;
                        string detail = "Inkjet Name :" + inkjetname + " || Error Type :" + type + " || Error Code :" + code + " || Error Detail" + detail_error + " || Date :" + date_error + time_error;
                        email.send(name, subject, detail);
                    }
                    else if (emails[i].ErrorID2 == true && code <= 99)
                    {
                        send_email.Send_Email email = new send_email.Send_Email();
                        //string name = "theerasak789900@gmail.com";
                        string name = emails[i].EmailName;
                        string subject = "Alarm Error Code :" + code + " Inkjet Name :" + inkjetname;
                        string detail = "Inkjet Name :" + inkjetname + " || Error Type :" + type + " || Error Code :" + code + " || Error Detail" + detail_error + " || Date :" + date_error + time_error;
                        email.send(name, subject, detail);
                    }
                }
            }
        }

        public static void Update_Datalog(List<Datalog> records)
        {
            Console.WriteLine("UPDATEEEEEEEEEEE");
            List<Datalog> all_datalog = Datalog.ListDatalog();
            List<Datalog> update_data = new List<Datalog>();
             
            for (int i = 0; i < all_datalog.Count; ++i)
            {
                if (all_datalog[i].InkJet == records[0].InkJet && all_datalog[i].Program == records[0].Program && all_datalog[i].Qty == "" && all_datalog[i].DateEnd == "")
                {
                    int diff_qty = Int32.Parse(records[0].qty_end) - Int32.Parse(all_datalog[i].qty_start);
                    string diff_qty_convert = diff_qty.ToString();
                    all_datalog[i].Qty = diff_qty_convert;
                    all_datalog[i].Program = records[0].Program;
                    all_datalog[i].DateEnd = records[0].DateEnd;
                    all_datalog[i].InkJet = records[0].InkJet;
                    all_datalog[i].DateStart = all_datalog[i].DateStart;
                    all_datalog[i].Shift = records[0].Shift;
                    all_datalog[i].qty_end = records[0].qty_end;

                    if (diff_qty == 0)
                    {
                        all_datalog.RemoveAt(i);
                    }

                }
                else
                {
                    all_datalog[i].Qty = all_datalog[i].Qty;
                    all_datalog[i].Program = all_datalog[i].Program;
                    all_datalog[i].DateEnd = all_datalog[i].DateEnd;
                    all_datalog[i].InkJet = all_datalog[i].InkJet;
                    all_datalog[i].DateStart = all_datalog[i].DateStart;
                    all_datalog[i].Shift = all_datalog[i].Shift;
                }
            }
            Datalog.Update_Datalog(all_datalog);
        }
    }
}
