// Written by bananathrowingmachine and [redacted] on November 22, 2024

using System.Text.Json;
using GUI.Client.Models;

// ReSharper disable once CheckNamespace
namespace SnakeGame.Networking;

/// <summary>
/// A simple handler for receiving server data.
/// </summary>
[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public class ConnectionHandler
{
    /// <summary>
    /// Current instance of the world
    /// </summary>
    private World _world = new();

    /// <summary>
    /// Current instance of the network
    /// </summary>
    private readonly NetworkConnection _network;

    /// <summary>
    /// Contains data to record scores and games
    /// </summary>
    private ScoreSaver? _currentSaver;

    /// <summary>
    /// The network connection object
    /// </summary>
    /// <param name="connection"> The current player's connection. </param>
    public ConnectionHandler(NetworkConnection connection)
    {
        _network = connection;
    }

    /// <summary>
    /// Connects to the server, starting the handshake. 
    /// </summary>
    /// <returns> The current game world. </returns>
    public World Connect(out int clientId)
    {
        int.TryParse(_network.ReadLine(), out clientId);
        int.TryParse(_network.ReadLine(), out int worldSize);
        lock (_world)
        {
            _world.SetSize(worldSize);
        }

        lock (_world)
            _currentSaver = new ScoreSaver(_world);

        //Used to deserialize wall information
        string currentLine = _network.ReadLine();
        while (currentLine[2..6] == "wall" || currentLine == string.Empty)
        {
            if (currentLine != string.Empty)
            {
                Wall wall = JsonSerializer.Deserialize<Wall>(currentLine)!;
                lock (_world)
                    _world.AddWall(wall);
            }
            currentLine = _network.ReadLine();
        }

        //Deserializes remaining incoming JSON information
        new Thread(() => ReceivingLoop(currentLine)).Start();

        lock (_world)
            return _world;
    }

    /// <summary>
    /// Receives data from the server to deserialize in a loop.
    /// </summary>
    /// <param name="firstLine">The first line received, as this loop reads the next line at the end.</param>
    private void ReceivingLoop(string firstLine)
    {
        string currentLine = firstLine;
        Dictionary<int, int> scoreTracker = new Dictionary<int, int>();

        while (true)
        {
            try
            {
                lock (_world)
                {
                    if (currentLine[2..7] == "snake")
                    {
                        Snake snake = JsonSerializer.Deserialize<Snake>(currentLine)!;

                        if (_world.SnakeKeys().Contains(snake.GetSnakeId()))
                        {
                            snake.SetJoinTime(_world.GetSnake(snake.GetSnakeId()).GetJoinTime());

                            if (snake.GetScore() > scoreTracker[snake.GetSnakeId()])
                            {
                                scoreTracker[snake.GetSnakeId()] = snake.GetScore();

                                snake.SetMax(scoreTracker[snake.GetSnakeId()]);

                                _currentSaver!.UpdatePlayerScore(snake);
                            }
                            else
                            {
                                snake.SetMax(scoreTracker[snake.GetSnakeId()]);
                            }
                        }
                        else
                        {
                            _currentSaver!.AddPlayer(snake);
                            scoreTracker[snake.GetSnakeId()] = 0;
                            snake.SetJoinTime(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                        }

                        if (snake.GetDisconnected()) //this is separate in the rare chance that a snake gets a new max score when it disconnects
                            _currentSaver!.UpdatePlayerLeaveTime(snake);

                        _world.AddSnake(snake);
                    }
                    else if (currentLine[2..7] == "power")
                    {
                        Powerup powerup = JsonSerializer.Deserialize<Powerup>(currentLine)!;
                        _world.AddPowerup(powerup);
                    }
                }

                currentLine = _network.ReadLine();
            }
            catch
            {
                _network.Disconnect();
                return;
            }

        }
    }

    /// <summary>
    /// Used to get disconnection time
    /// </summary>
    /// <returns> Time of disconnection </returns>
    public Action<String> GameDisconnectMethod()
    {
        return _currentSaver!.UpdateGame;
    }

    /// <summary>
    /// Used to disconnect multiple snakes after host leaves
    /// </summary>
    /// <returns> Returns the time of host disconnection </returns>
    public Action<Snake, string> MassSnakeDisconnectMethod()
    {
        return _currentSaver!.UpdatePlayerLeaveTime;
    }
}