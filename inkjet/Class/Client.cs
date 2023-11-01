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

            try
            {

                for (int y = 0; y < records.Count; ++y)
                {

                    string serverIP = records[y].IPAdress;
                    int port = 37022;
                
                    var ping = new Ping();
                    var reply = ping.Send(records[y].IPAdress, 60 * 10); // 1 minute time out (in ms)
                    Console.WriteLine("Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address);
                   
                    IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);

                    Socket sender = new Socket(localEndPoint.AddressFamily,
                               SocketType.Stream, ProtocolType.Tcp);

                    Console.WriteLine("Status :  " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address);

                    break;
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
                            Console.WriteLine("Socket connected to -> {0} ",
                                          sender.RemoteEndPoint.ToString());

                            //Creation of message that
                            // we will send to Server
                            byte[] messageSent = Encoding.ASCII.GetBytes("DD\r");
                            int byteSent = sender.Send(messageSent);

                            // Data buffer
                            byte[] messageReceived = new byte[1024];


                            // We receive the message using 
                            // the method Receive(). This 
                            // method returns number of bytes
                            // received, that we'll use to 
                            // convert them to string
                            int byteRecv = sender.Receive(messageReceived);
                            Console.WriteLine("Message from Server -> {0}",
                                  Encoding.ASCII.GetString(messageReceived,
                                                             0, byteRecv));
    
                            new_status = "Connected";                                                  
                            //list.Add(new Tuple<string, string>(input, new_status));
                            
                            records_update.Add(new Inkjet { IPAdress = input , Status = new_status });

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
                        records_update.Add(new Inkjet { IPAdress = "0", Status = new_status });
                    }
                }
            }
            catch (Exception e)
            {
                new_status = "NOT Connected";
                Console.WriteLine(e.ToString());
                records_update.Add(new Inkjet { IPAdress = "0", Status = new_status });
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
                    }
                    else
                    {
                        if (p[i].Status != records[i].Status)
                        {
                            SendEmail(records[i].IPAdress, "NOT Connected");
                        }
                        records[i].Status = "NOT Connected";                       
                    }
                                    
                        records[i].InkJetID = records[i].InkJetID;
                        records[i].InkJetName = records[i].InkJetName;
                        records[i].IPAdress = records[i].IPAdress;
                        records[i].Status = records[i].Status;
                        records[i].Status_inkjet = records[i].Status_inkjet;
                        records[i].Ink = records[i].Ink;
                        records[i].Solvent = records[i].Solvent;
                        records[i].Pump = records[i].Pump;
                        records[i].Filter = records[i].Filter;
                        records[i].Program = records[i].Program;
                    
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
