using System;
using Entities;
namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port;

            Console.WriteLine("+-----------------------------------------------------------------------+");
            Console.WriteLine("|                           Servidor de chat                            |");
            Console.WriteLine("+-----------------------------------------------------------------------+");
            Console.Write("Ingrese en que puerto desea iniciar el servidor: ");

            while (!int.TryParse(Console.ReadLine(), out port))
            {
                Console.Write("Error, reingrese por favor: ");
            }

            Server server = new Server(port);
            server.Run();

            string message;

            while (true)
            {
                if (server.ServerMessage.Count > 0)
                {
                    message = server.ServerMessage.Dequeue();
                    if (message.Contains("[ DONE ]"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        
                    }
                    else if (message.Contains("[ ERROR ]"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if(message.Contains("[ INFO ]"))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.WriteLine(message);
                    Console.ResetColor();
                }

                if (Server.messagesQueue.Count > 0)
                {
                    message = Server.messagesQueue.Dequeue();
                    Console.WriteLine(message);
                    foreach (User item in server.ClientList)
                    {
                        item.SendMessage(message);
                    }
                }
            }
        }
    }
}
