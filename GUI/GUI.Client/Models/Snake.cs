// Written by bananathrowingmachine and [redacted] on November 17, 2024

using System.Text.Json.Serialization;

namespace GUI.Client.Models
{
    /// <summary>
    /// Used to represent a snake.
    /// </summary>
    public class Snake
    {
        /// <summary>
        /// Contains snake's ID
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("snake")]
        private int Id { get; set; }

        /// <summary>
        /// Contains player's name
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("name")]
        private string Name { get; set; }

        /// <summary>
        /// Represents body of snake
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("body")]
        private List<Point2D> Body { get; set; }

        /// <summary>
        /// Represents snake's orientation
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("dir")]
        private Point2D? Dir { get; set; }

        /// <summary>
        /// Records player's score
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("score")]
        private int Score { get; set; }

        /// <summary>
        /// Indicates if snake died on current frame
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("died")]
        private bool Died { get; set; }

        /// <summary>
        /// Indicates if snake is dead or alive
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("alive")]
        private bool Alive { get; set; }

        /// <summary>
        /// Indicates if player was disconnected on current frame
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("dc")]
        private bool Dc { get; set; }

        /// <summary>
        /// Indicates if player joined on current frame
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("join")]
        private bool Join { get; set; }

        /// <summary>
        /// Records the snake's all time record
        /// </summary>
        [JsonIgnore]
        private int MaxScore { get; set; }

        /// <summary>
        /// Records the snake's join time
        /// </summary>
        [JsonIgnore]
        private string JoinTime { get; set; }

        /// <summary>
        /// Creates a default constructor for Snake.
        /// </summary>
        public Snake()
        {
            Id = 0;
            Name = string.Empty;
            Body = new List<Point2D>();
            Dir = new Point2D();
            Score = 0;
            Died = false;
            Alive = true;
            Dc = false;
            Join = false;
            MaxScore = 0;
            JoinTime = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
        }

        /// <summary>
        /// Copy constructor to create a new Snake object based on an existing Snake object.
        /// </summary>
        public Snake(Snake other)
        {
            Id = other.Id;
            Name = other.Name;
            Body = new List<Point2D>(other.Body);
            Dir = new Point2D();
            Score = other.Score;
            Died = other.Died;
            Alive = other.Alive;
            Dc = other.Dc;
            Join = other.Join;
            MaxScore = other.MaxScore;
            JoinTime = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
        }

        /// <summary>
        /// Getter for the direction of the snake
        /// </summary>
        /// <returns>Snake dir as a Point2D</returns>
        public Point2D? GetDir()
        {
            return Dir;
        }

        /// <summary>
        /// Getter for body points
        /// </summary>
        /// <param name="bodyIndex">The index needed</param>
        /// <returns>A point2D in the snake body represented by the index</returns>
        public Point2D GetBodySegment(int bodyIndex)
        {
            return Body[bodyIndex];
        }

        /// <summary>
        /// Getter for amount of points in the snake body
        /// </summary>
        /// <returns>A int with the amount of points in the body</returns>
        public int NumberOfSegments()
        {
            return Body.Count;
        }

        /// <summary>
        /// Getter for the snake ID
        /// </summary>
        /// <returns>A int of the snake ID</returns>
        public int GetSnakeId()
        {
            return Id;
        }

        /// <summary>
        /// Getter for if the snake disconnected on that frame
        /// </summary>
        /// <returns>A bool of if the snake disconnected</returns>
        public bool GetDisconnected()
        {
            return Dc;
        }

        /// <summary>
        /// Returns the snake's current score
        /// </summary>
        /// <returns> The snake's score </returns>
        public int GetScore()
        {
            return Score;
        }

        /// <summary>
        /// Returns the snake's current death status
        /// </summary>
        /// <returns> The snake's death status </returns>
        public bool GetDeathStatus()
        {
            return Died;
        }

        /// <summary>
        /// Returns the snake's current alive status
        /// </summary>
        /// <returns> The snake's alive status </returns>
        public bool GetAliveStatus()
        {
            return Alive;
        }

        /// <summary>
        /// Gets the name of a snake
        /// </summary>
        /// <returns> Snake's name </returns>
        public string GetName()
        {
            return Name;
        }

        /// <summary>
        /// Gets the max score of a snake
        /// </summary>
        /// <returns> Snake's max score </returns>
        public int GetMax()
        {
            return MaxScore;
        }

        /// <summary>
        /// Sets the snake's new max score
        /// </summary>
        /// <param name="newMax"> The new max score </param>
        public void SetMax(int newMax)
        {
            MaxScore = newMax;
        }

        /// <summary>
        /// Gets the join time of a snake
        /// </summary>
        /// <returns> The join time of the snake </returns>
        public string GetJoinTime()
        {
            return JoinTime;
        }

        /// <summary>
        /// Sets join time for a snake
        /// </summary>
        public void SetJoinTime(string joinTime)
        {
            JoinTime = joinTime;
        }
    }
}