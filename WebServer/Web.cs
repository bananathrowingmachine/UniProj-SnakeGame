// Written by bananathrowingmachine and [redacted] on December 2, 2024

using SnakeGame.Networking;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Sockets;

namespace WebServer
{
    /// <summary>
    /// Used to support a webpage displaying snake game leaderboards
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class Web
    {
        /// <summary>
        /// Contains information to connect to MySQL
        /// </summary>
        private const string ConnectionString = "server=[redacted];" +
                                               "database=[redacted];" +
                                               "id=[redacted];" +
                                               "password=[redacted]";

        /// <summary>
        /// Used to indicate connection was successful
        /// </summary>
        private const string HttpOkHeader =
            "HTTP/1.1 200 OK\r\n" +
            "Connection: close\r\n" +
            "Content-Type: text/html; charset=UTF=-8\r\n" +
            "\r\n";

        /// <summary>
        ///   The main program.
        /// </summary>
        /// <returns> A Task. Not really used. </returns>
        private static void Main()
        {
            Server.StartServer(HandleConnect, 80);
            Console.Read(); // don't stop the program.
        }


        /// <summary>
        ///   <pre>
        ///     When a new connection is established, enter a loop that receives from and
        ///     replies to a client.
        ///   </pre>
        /// </summary>
        ///
        private static void HandleConnect(HttpNetworkConnection client)
        {
            try
            {
                string request = client.ReadLine();
                Console.WriteLine(request);
            

                if (request.Contains("GET /games?gid="))
                {
                    string[] requestParts = request.Split(' ');
                    client.Send(GIdPage(requestParts[1][11..]));
                }
                else if (request.Contains("GET /games"))
                {
                    client.Send(GamesPage());
                }
                else if (request.Contains("GET /"))
                {
                    client.Send(HttpOkHeader + "<html>\r\n<h3>Welcome to the Snake Games Database!</h3>\r\n<a href=\"/games\">View Games</a>\r\n</html>");
                }

                client.Disconnect();
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Used to display the information on the games page
        /// </summary>
        /// <returns> A HTML string to display </returns>
        private static string GamesPage()
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = conn.CreateCommand();
                    command.CommandText = "SELECT * FROM Games";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        string response = "<html>";
                        response += "<h2>Games List</h2>";
                        response += "<table border='1'>";
                        response += "<tr><th>Game ID</th><th>Start Time</th><th>End Time</th></tr>";

                        while (reader.Read())
                        {
                            string gameId = reader["ID"].ToString()!;
                            string startTime = reader["Start Time"].ToString()!;
                            string endTime = reader["End Time"].ToString()!;

                            response += "<tr>";
                            response += $"<td><a href='/games?gid={gameId}'> {gameId} </a></td>";
                            response += $"<td>{startTime}</td>";
                            response += $"<td>{endTime}</td>";
                            response += "</tr>";
                        }

                        response += "</table>";
                        response += "</html>";

                        return HttpOkHeader + response;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// Used to display the players page
        /// </summary>
        /// <param name="gameNumber"> The ID of the desired game </param>
        /// <returns> A HTML string to display </returns>
        private static string GIdPage(string gameNumber)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = conn.CreateCommand();
                    command.CommandText = $"SELECT * FROM Players WHERE (`Game ID`) = {gameNumber}";

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        string response = "<html>";
                        response += $"<h2>Stats for Game {gameNumber}</h2>";
                        response += "<table border='1'>";
                        response += "<tr><th>Player ID</th><th>Player Name</th><th>Max Score</th><th>Enter Time</th><th>Leave Time</th></tr>";

                        while (reader.Read())
                        {
                            string playerId = reader["ID"].ToString()!;
                            string playerName = reader["Name"].ToString()!;
                            string maxScore = reader["Max Score"].ToString()!;
                            string startTime = reader["Enter Time"].ToString()!;
                            string endTime = reader["Leave Time"].ToString()!;

                            response += "<tr>";
                            response += $"<td>{playerId}</td>";
                            response += $"<td>{playerName}</td>";
                            response += $"<td>{maxScore}</td>";
                            response += $"<td>{startTime}</td>";
                            response += $"<td>{endTime}</td>";
                            response += "</tr>";
                        }

                        response += "</table>";
                        response += "</html>";

                        return HttpOkHeader + response;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }
    }

    /// <summary>
    ///   Represents a server task that waits for connections on a given
    ///   port and calls the provided delegate when a connection is made.
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    static class Server
    {

        /// <summary>
        ///   Wait on a TcpListener for new connections. Alert the main program
        ///   via a callback (delegate) mechanism.
        /// </summary>
        /// <param name="handleConnect">public
        ///   Handler for what the user wants to do when a connection is made.
        ///   This should be run asynchronously via a new thread.
        /// </param>
        /// <param name="port"> The port (e.g., 11000) to listen on. </param>
        public static void StartServer(Action<HttpNetworkConnection> handleConnect, int port)
        {
            TcpListener listener = new(IPAddress.Any, port);
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                HttpNetworkConnection connection = new HttpNetworkConnection(client);
                new Thread(() => handleConnect(connection)).Start();
            }
        }
    }

}
