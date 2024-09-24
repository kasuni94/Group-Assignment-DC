using System;
using System.ServiceModel;
using GamingLobbyServerInterface;

namespace GamingLobbyServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Game Server is starting...");

            ServiceHost host = null;
            string url = "net.tcp://localhost:8101/BusinessService"; // Use localhost for development
            NetTcpBinding tcpBinding = new NetTcpBinding();

            try
            {

                host = new ServiceHost(typeof(GameServer));

              
                host.AddServiceEndpoint(typeof(GameServerInterface), tcpBinding, url);

               
                host.Open();
                Console.WriteLine("Game Server is now online at {0}", url);

                bool keepRunning = true;
                while (keepRunning)
                {
                    var input = Console.ReadLine();
                    if (input?.ToLower() == "exit")
                    {
                        Console.WriteLine("Shutting down server...");
                        keepRunning = false;
                    }
                }
            }
            catch (CommunicationException commEx)
            {

                Console.WriteLine("Communication error: " + commEx.Message);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                if (host != null)
                {
                    try
                    {
                        host.Close();
                        Console.WriteLine("Game Server has shut down successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to close the host: " + ex.Message);
                    }
                }
            }
        }
    }
}
