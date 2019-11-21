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
            double CO = 0;
            double NOx = 0;
            string level = " ";
            int number = 0;
            double sumCO = 0;
            double sumNOx = 0;

            //Creates a UdpClient for reading incoming data.
            UdpClient udpServer = new UdpClient(11111);

            //This IPEndPoint will allow you to read datagrams sent from any ip-source on port 11111.  
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 11111);

            try
            {
                // Blocks until a message is received on this socket from a remote host (a client).
                Console.WriteLine("Server is blocked");
                while (true)
                {
                    Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                    //Server is now activated");

                    string receivedData = Encoding.ASCII.GetString(receiveBytes);

                    string[] dataLines = receivedData.Split('\n');

                    string[] list1 = dataLines[3].Split(':');
                    string text1 = list1[1];
                    string[] list2 = dataLines[4].Split(':');
                    string text2 = list2[1];
                    string[] list3 = dataLines[5].Split(':');
                    string text3 = list3[1];

                    CO = Double.Parse(text1.Trim());
                    NOx = Int32.Parse(text2.Trim());
                    level = text3;
                    sumCO = sumCO + CO;
                    sumNOx = sumNOx + NOx;

                    number++;

                    /*
                    string Sender = data[0];
                    string Location = data[1];
                    DateTime time = Convert.ToDateTime(data[2]);
                    double CO = Convert.ToDouble(data[3]);
                    double NOx = Convert.ToDouble(data[4]);
                    string particleLevel = data[5];
                    
                    //Console.WriteLine(receivedData);
                    Console.WriteLine(Sender + " \n" + Location + " \n" + time + " \n" + CO + " \n" + NOx + " \n" + particleLevel);
                    //Console.WriteLine("Received from: " + clientName.ToString() + " " + text.ToString());
                    
                    Console.WriteLine("This message was sent from " +
                                      RemoteIpEndPoint.Address.ToString() +
                                      " on their port number " +
                                      RemoteIpEndPoint.Port.ToString());
                    */
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Average CO: " + sumCO / number);
            Console.WriteLine("Average NOx: " + sumNOx / number);
        }
    }
}
