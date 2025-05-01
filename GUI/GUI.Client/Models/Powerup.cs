// Written by bananathrowingmachine and [redacted] on November 17, 2024

using System.Text.Json.Serialization;

namespace GUI.Client.Models
{
    /// <summary>
    /// Used to represent powerup objects.
    /// </summary>
    public class Powerup
    {
        /// <summary>
        /// Contains powerup's unique ID
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("power")]
        private int Power { get; set; }

        /// <summary>
        /// Represents the location of the powerup
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("loc")]
        private Point2D Loc { get; set; }

        /// <summary>
        /// Indicates if the powerup has died
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("died")]
        private bool Died { get; set; }

        /// <summary>
        /// Used to create a new powerup object
        /// </summary>
        /// <param name="power"> The ID of the powerup</param>
        /// <param name="loc"> Location of the powerup </param>
        public Powerup(int power, Point2D loc)
        {
            Power = power;
            Loc = loc;
        }

        /// <summary>
        /// Creates a default constructor for Powerup.
        /// </summary>
        public Powerup()
        {
            Power = 0;
            Loc = new Point2D();
            Died = false;
        }

        /// <summary>
        /// Returns death state of the powerup
        /// </summary>
        /// <returns>Boolean of the powerup death state</returns>
        public bool HasDied()
        {
            return Died;
        }

        /// <summary>
        /// Gets the location of the powerup
        /// </summary>
        /// <returns>Returns a Point2D representation of the power</returns>
        public Point2D GetLocation()
        {
            return Loc;
        }

        /// <summary>
        /// Gets the ID of the powerup
        /// </summary>
        /// <returns>Returns the ID of the powerup</returns>
        public int GetPowerId()
        {
            return Power;
        }
    }
}