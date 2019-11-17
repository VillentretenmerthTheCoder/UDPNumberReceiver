using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UDPNumberReceiver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Creates a UdpClient for reading incoming data.
            UdpClient udpServer = new UdpClient(9999);

            //Creates an IPEndPoint to record the IP Address and port number of the sender.  
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 9999);
            //IPEndPoint object will allow us to read datagrams sent from another source.

            try
            {
                // Blocks until a message is received on this socket from a remote host (a client).
                Console.WriteLine("Server is blocked");
                float SumCO = 0;
                float SumNOx = 0;
                while (true)
                {
                    Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                    //Server is now activated");

                    string receivedData = Encoding.ASCII.GetString(receiveBytes);
                    if (receivedData == "Stop")
                    {
                        Console.WriteLine("The broadcast has been stopped!");
                        break;
                    }
                    else
                    {

                        string[] data = receivedData.Split(' ');
                        string CO = data[11];
                        string NOx = data[15];
                        string Date = data[8];
                        string ParticleLevel = data[19];
                        SumCO = SumCO + float.Parse(CO);
                        SumNOx = SumNOx + float.Parse(NOx);
                        Console.WriteLine(receivedData + $"\r\n Sum of all CO: {SumCO}\r\n Sum of all NOx: {SumNOx}\r\n");

                        Worker worker = new Worker();
                        //Posting data to Rest Service.
                        SensorData sensordata = new SensorData(Date, CO, NOx, ParticleLevel);
                        string content = JsonConvert.SerializeObject(sensordata);
                        await worker.PostDataAsync(content);
                        
                        
                        
                        //IList<SensorData> cList = await worker.GetAllDataAsync();

                        //foreach (SensorData i in cList)
                        //{
                        //    Console.WriteLine(i.ToString());
                        //}


                    }
                }
                }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            


        }
    }
}
