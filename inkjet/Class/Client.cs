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
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq.Expressions;

namespace client
{
    public class Program
    {

        // Main Method
        public void Execute_Client()
        {
            ExecuteClient();
        }

        // ExecuteClient() Method
        public static void ExecuteClient()
        {
            List<Inkjet> records;
            List<Inkjet> records_update = new List<Inkjet>();

            //var list = new List<Tuple<string, string>>();
            string new_status;

            using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Inkjet>().ToList();
            }
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



                            Console.WriteLine(respone_programs_list[3]);


   
                          

                            new_status = "Connected";
                            records_update.Add(new Inkjet { IPAdress = input, Status = new_status ,Status_inkjet = respone_status ,Program = respone_programs_list[3] });

                            //Console.WriteLine("Message from Server -> {0}",
                            //      Encoding.ASCII.GetString(messageReceived,
                            //                                 0, byteRecv));


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
                        new_status = "NOT Connected";
                        //list.Add(new Tuple<string, string>("0", new_status));
                        records_update.Add(new Inkjet { IPAdress = "0", Status = new_status , Status_inkjet = "Error" , Program = "" });
                    }

                }
            catch (Exception e)
            {
                new_status = "NOT Connected";
                Console.WriteLine(e.ToString());
                records_update.Add(new Inkjet { IPAdress = "0", Status = new_status , Status_inkjet = "Error" , Program = "" });
            }
        }

            UpdateInkjetStatus(records_update);
        }
        public static void UpdateInkjetStatus(List<Inkjet> p) 
        {
            List<Inkjet> records;
            using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                
                records = csv.GetRecords<Inkjet>().ToList();
    
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
                        //Console.WriteLine(p[i].Program);
                    }
                    else
                    {
                        if (p[i].Status != records[i].Status)
                        {
                            SendEmail(records[i].IPAdress, "NOT Connected");
                        }
                        records[i].Status = "NOT Connected";
                        records[i].Status_inkjet = p[i].Status_inkjet;
                        records[i].Program = p[i].Program;
                    }
                                    
                        records[i].InkJetID = records[i].InkJetID;
                        records[i].InkJetName = records[i].InkJetName;
                        records[i].IPAdress = records[i].IPAdress;
                        records[i].Ink = records[i].Ink;
                        records[i].Solvent = records[i].Solvent;
                        records[i].Pump = records[i].Pump;
                        records[i].Filter = records[i].Filter;
                        //records[i].Program = records[i].Program;
                    
                }
            }

            using (var writer = new StreamWriter(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
            using (var csv2 = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv2.WriteRecords(records);
            }
        }

        public static void SendEmail(string ip,string status)
        {
            send_email.Send_Email email = new send_email.Send_Email();
            string name = "theerasak789900@gmail.com";
            string subject = "Inkjet " + ip + "Status";
            string detail = status;
            email.send(name, subject, detail);
        }


    }
}
