using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatClientConsole // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        class MyContinuousClient
        {
            TcpClient client = null;

            public void Run()
            {
                Console.WriteLine("========클라이언트=======");
                Console.WriteLine("1. 서버 연결");
                Console.WriteLine("2. Message 보내기");
                Console.WriteLine("=========================");

                while (true)
                {
                    string key = Console.ReadLine();
                    int order = 0;

                    if (int.TryParse(key, out order))
                    {
                        switch (order)
                        {
                            case 1:
                                {
                                    if (client != null)
                                    {
                                        Console.WriteLine("이미 연결되어있습니다.");
                                    }
                                    else
                                    {
                                        Connect();
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if(client == null)
                                    {
                                        Console.WriteLine("먼저 서버와 연결해주세요.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("보낼 Message를 입력해주세요.");
                                        while (true)
                                        {
                                            SendMessage();
                                        }                                        
                                    }
                                    break;
                                }
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못 입력하셨습니다.");
                    }
                }
            }

            private void Connect()
            {
                client = new TcpClient();
                client.Connect("127.0.0.2", 9999);
                Console.WriteLine("서버 연결 성공. Message를 입력해주세요.");
            }
            private void SendMessage()
            {                
                string message = Console.ReadLine();
                byte[] byteData = new byte[message.Length];
                byteData = Encoding.Default.GetBytes(message);

                client.GetStream().Write(byteData, 0, byteData.Length);
                Console.WriteLine("전송 성공");
            }

        }

        static void Main(string[] args)
        {
            MyContinuousClient client = new MyContinuousClient();
            client.Run();
        }
    }
}