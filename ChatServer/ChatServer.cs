// Written by bananathrowingmachine and [redacted] on November 8, 2024

using SnakeGame.Networking;

// ReSharper disable once CheckNamespace
namespace SnakeGame.Chatting;

/// <summary>
///   A simple ChatServer that handles clients separately and replies with a static message.
/// </summary>
public class ChatServer
{

    /// <summary>
    ///   Creates a record of all clients that have connected to the server, along with their preferred names.
    /// </summary>
    private static Dictionary<NetworkConnection, string> _connectionNames = new ();

    /// <summary>
    ///   The main program.
    /// </summary>
    /// <returns> A Task. Not really used. </returns>
    private static void Main()
    {
        Server.StartServer(HandleConnect, 11_000);
        Console.Read(); // don't stop the program.
    }


    /// <summary>
    ///   <pre>
    ///     When a new connection is established, enter a loop that receives from and
    ///     replies to a client.
    ///   </pre>
    /// </summary>
    ///
    private static void HandleConnect(NetworkConnection connection)
    {
        string? name = null;

        // handle all messages until disconnect.
        try
        {
            while (true)
            {
                var message = connection.ReadLine();

                if(message.Equals(string.Empty))
                    continue;

                if (name is null)
                {
                    name = message;
                    lock (_connectionNames)
                        _connectionNames.Add(connection, name);
                }
                else
                {
                    if (_connectionNames.TryGetValue(connection, out var connectionName))
                    {
                        string chatMessage = connectionName + ": " + message;
                        foreach (NetworkConnection connectedClient in _connectionNames.Keys)
                            connectedClient.Send(chatMessage);
                    }
                }
            }
        }
        catch (Exception)
        {
            lock (_connectionNames)
                if (name is not null)
                    _connectionNames.Remove(connection);
        }
    }
}