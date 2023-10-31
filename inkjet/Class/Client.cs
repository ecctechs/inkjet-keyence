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
            List<string> IP_for_Update = new List<string>();

            using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Inkjet>().ToList();                                 
            }

            try
            {

                for (int y = 0; y < records.Count; ++y)
                {
                    Console.WriteLine(records[y].IPAdress);                   

                    string serverIP = records[y].IPAdress;
                    int port = 37022;
                    IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);

                    //foreach (IPAddress ipaddress in ipHost.AddressList)
                    //{
                    //    Console.WriteLine("IP{0}", ipaddress);
                    //}

                    Socket sender = new Socket(localEndPoint.AddressFamily,
                               SocketType.Stream, ProtocolType.Tcp);
                   

                    try
                    {
                        // Connect Socket to the remote 
                        // endpoint using method Connect()
                        sender.Connect(localEndPoint);

 
                            //else
                            //    throw new Exception("Timed out");

                            string input = sender.RemoteEndPoint.ToString();
                            int index = input.IndexOf(":");
                            if (index >= 0)
                                input = input.Substring(0, index);

                            //UpdateInkjetStatus(serverIP);

                            //We print EndPoint information
                            // that we are connected
                            Console.WriteLine("Socket connected to -> {0} ",
                                          sender.RemoteEndPoint.ToString());

                            //Creation of message that
                            // we will send to Server
                            byte[] messageSent = Encoding.ASCII.GetBytes("ZZ,2\r");
                            int byteSent = sender.Send(messageSent);

                            byte[] messageReceived = new byte[1024];

                            Console.WriteLine("Message from Server  -> {0}",
                                  Encoding.ASCII.GetString(messageReceived,
                                                             0, byteSent));

                            // Close Socket using 
                            // the method Close()
                            IP_for_Update.Add(input);

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
                        IP_for_Update.Add("0");
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    }

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
            UpdateInkjetStatus(IP_for_Update);
        }
        public static void UpdateInkjetStatus(List<string> p) 
        {
            

            List<Inkjet> records;
            using (var reader = new StreamReader(@"C:\Users\ADMIN\Desktop\test\inkjet.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                
                records = csv.GetRecords<Inkjet>().ToList();
    
                for (int i = 0; i < p.Count; ++i)
                {
                    if (p[i] == records[i].IPAdress)
                    {
                        records[i].Status = "Connected";
                    }
                    else
                    {
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
    }
}
