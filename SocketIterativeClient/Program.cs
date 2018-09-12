using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketIterativeClient
{
    class Program
    {
        private static TcpClient _clientSocket = null;
        private static Stream _nstream = null;
        private static StreamWriter _sWriter = null;
        private static StreamReader _sReader = null;
        static void Main(string[] args)
        {
            try
            {
                // Destination host address , You assume that you are hosted your server somewhere else hosting service and
                // wanna to check IP address of this machine
                string name = "localhost";
                IPAddress[] adds = Dns.GetHostEntry(name).AddressList;
                Console.WriteLine(adds[0]);
                Console.WriteLine(adds[1]);

                using (_clientSocket = new TcpClient(adds[1].ToString(), 6789))
                {
                    using (_nstream = _clientSocket.GetStream())
                    {
                        // stream pipeline
                        _sWriter = new StreamWriter(_nstream) { AutoFlush = true };
                        _sReader = new StreamReader(_nstream);

                        Console.WriteLine("Client ready to send bytes of data to server...");
                        Console.WriteLine(" Your message send to server 100 times");
                        // Clients wants to send 5 times data to Server , it is need for loop
                        for (int i = 0; i < 3; i++)
                        {
                            Thread.Sleep(1000);
                            string clientMsg = "Zuhair";
                            Console.WriteLine("Current Date:" + DateTime.Now);
                            Console.WriteLine("Counting no:" + i);
                            // write data
                            _sWriter.WriteLine(clientMsg);
                            // read data
                            string rdMsgFromServer = _sReader.ReadLine();
                            if (rdMsgFromServer != null)
                            {
                                Console.WriteLine(".....................................................");
                                Console.WriteLine("Client recieved Message from Server:" + rdMsgFromServer);
                                Console.WriteLine(".....................................................");

                            }
                        }

                    }
                }
                Console.WriteLine("Press enter to stop the client!");
                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }
        }

    }
}
