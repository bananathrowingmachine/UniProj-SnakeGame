// Written by bananathrowingmachine and [redacted] on November 22, 2024

// ReSharper disable once CheckNamespace
using GUI.Client.Models;
using MySql.Data.MySqlClient;

// ReSharper disable once CheckNamespace
namespace SnakeGame.Networking;

/// <summary>
/// Used to record the game session and connected players.
/// </summary>
public class ScoreSaver
{
    /// <summary>
    /// The connection string.
    /// Your uID login name serves as both your database name and your uid
    /// </summary>
    private const string ConnectionString = "server=atr.eng.utah.edu;" +
  "database=u1402304;" +
  "uid=u1402304;" +
  "password=SnakeGamePassword";

    /// <summary>
    /// Records the time when a game start up occurs
    /// </summary>
    private readonly string _startTime;

    /// <summary>
    /// Records the time when player connection occurs
    /// </summary>
    private readonly string _snakeJoinTime;

    /// <summary>
    /// The current world
    /// </summary>
    private World _world;

    /// <summary>
    /// The ID of the current game
    /// </summary>
    private UInt64 _gameId;

    /// <summary>
    /// Used to create a new ScoreSaver object.
    /// </summary>
    public ScoreSaver(World theWorld)
    {
        _world = theWorld;
        _startTime = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
        _snakeJoinTime = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
        AddGame();
    }

    /// <summary>
    /// Adds a new game into the database.
    /// </summary>
    public void AddGame()
    {
        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"INSERT INTO Games (`Start Time`) " +
                               $"VALUES ('{_startTime}');";
                command.ExecuteNonQuery();

                _gameId = LatestGameId(conn);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    /// <summary>
    /// Adds a new game into the database.
    /// </summary>
    /// <param name="endTime"> The time the game ended. </param>
    public void UpdateGame(string endTime)
    {
        foreach (Snake snake in _world.GetSnakes())
            UpdatePlayerLeaveTime(snake);

        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"REPLACE INTO Games (`ID`, `Start Time`, `End Time`) " +
                               $"VALUES ('{_gameId}', '{_startTime}', '{endTime}');";
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    /// <summary>
    /// Adds a new player into the database.
    /// </summary>
    /// <param name="snake"> The snake of the player </param>
    public void AddPlayer(Snake snake)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"INSERT INTO Players (`ID`, `Name`, `Max Score`, `Enter Time`, `Game ID`) " +
                                               $"VALUES ('{snake.GetSnakeId()}', '{snake.GetName()}', '{0}', '{snake.GetJoinTime()}', '{_gameId}');";

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    /// <summary>
    /// Updates a player's score in the database.
    /// </summary>
    /// <param name="snake"> The snake of the player </param>
    public void UpdatePlayerScore(Snake snake)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"REPLACE INTO Players (`ID`, `Name`, `Max Score`, `Enter Time`, `Game ID`) " +
                               $"VALUES ('{snake.GetSnakeId()}', '{snake.GetName()}', '{snake.GetMax()}', '{snake.GetJoinTime()}', '{_gameId}');";

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    /// <summary>
    /// Updates a player's leave time in the database.
    /// </summary>
    /// <param name="snake"> The snake of the player </param>
    public void UpdatePlayerLeaveTime(Snake snake)
    {
        UpdatePlayerLeaveTime(snake, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
    }

    /// <summary>
    /// Updates a player's leave time in the database.
    /// </summary>
    /// <param name="snake"> The snake of the player </param>
    /// <param name="endTime"> The time the player leaves </param>
    public void UpdatePlayerLeaveTime(Snake snake, string endTime)
    {
        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"REPLACE INTO Players (`ID`, `Name`, `Max Score`, `Enter Time`, `Leave Time`, `Game ID`) " +
                                                      $"VALUES ('{snake.GetSnakeId()}', '{snake.GetName()}', '{snake.GetMax()}', '{snake.GetJoinTime()}', '{endTime}', '{_gameId}');";

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    /// <summary>
    /// Used to get the ID of the latest game.
    /// </summary>
    /// <returns> The ID of the last game added </returns>
    private static UInt64 LatestGameId(MySqlConnection conn)
    {
        try
        {
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT last_insert_id();";

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                return (UInt64)reader[0];
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}