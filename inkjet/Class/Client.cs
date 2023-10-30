// A C# program for Client
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;



namespace client
{
    public class Program
    {

        // Main Method
        public void Execute_Client(string message)
        {
            ExecuteClient(message);
        }

        // ExecuteClient() Method
        public static void ExecuteClient(string message)
        {

            try
            {
             
                string serverIP = "192.168.0.101";
                int port = 9004;
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);


                Socket sender = new Socket(localEndPoint.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    // Connect Socket to the remote 
                    // endpoint using method Connect()
                    sender.Connect(localEndPoint);

                    // We print EndPoint information 
                    // that we are connected
                    Console.WriteLine("Socket connected to -> {0} ",
                                  sender.RemoteEndPoint.ToString());

                    // Creation of message that
                    // we will send to Server
                    byte[] messageSent = Encoding.ASCII.GetBytes("test222\r");
                    int byteSent = sender.Send(messageSent);

                    byte[] messageReceived = new byte[1024];

                    Console.WriteLine("Message from Server  -> {0}",
                          Encoding.ASCII.GetString(messageReceived,
                                                     0, byteSent));


                    // Close Socket using 
                    // the method Close()
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

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }
    }
}
