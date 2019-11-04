using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPSensorReceiver
{
    class UDPSensorReceiver
    {
        static void Main(string[] args)
        {
            //Creates a UdpClient for reading incoming data.
            UdpClient udpServer = new UdpClient(11111);

            //Creates an IPEndPoint to record the IP Address and port number of the sender.  
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 9999);

            try
            {
                // Blocks until a message is received on this socket from a remote host (a client).
                Console.WriteLine("Server is blocked");
                while (true)
                {
                    Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                    //Server is now activated");

                    string receivedData = Encoding.ASCII.GetString(receiveBytes);
                    string[] data = receivedData.Split('\n');
                    string Sender = data[0];
                    string Location = data[1];
                    DateTime time = Convert.ToDateTime(data[2]);
                    double CO = Convert.ToDouble(data[3]);
                    double NOx = Convert.ToDouble(data[4]);
                    string particleLevel = data[5];

                    Console.WriteLine(receivedData);
                    //Console.WriteLine("Received from: " + clientName.ToString() + " " + text.ToString());

                    Console.WriteLine("This message was sent from " +
                                      RemoteIpEndPoint.Address.ToString() +
                                      " on their port number " +
                                      RemoteIpEndPoint.Port.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
