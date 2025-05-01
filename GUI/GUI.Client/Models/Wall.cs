// Written by bananathrowingmachine and [redacted] on November 17, 2024

using System.Text.Json.Serialization;

namespace GUI.Client.Models
{
    /// <summary>
    /// Used to represent a wall.
    /// </summary>
    public class Wall
    {
        /// <summary>
        /// The wall's unique ID
        /// </summary>
        [JsonPropertyName("wall")]
        [JsonInclude]
        private int WallId { get; set; }

        /// <summary>
        /// One endpoint of a wall
        /// </summary>
        [JsonPropertyName("p1")]
        [JsonInclude]
        private Point2D P1 { get; set; }

        /// <summary>
        /// Other endpoint of a wall
        /// </summary>
        [JsonPropertyName("p2")]
        [JsonInclude]
        private Point2D P2 { get; set; }

        /// <summary>
        /// Used to create a new wall
        /// </summary>
        /// <param name="id"> The wall's ID </param>
        /// <param name="p1"> One wall endpoint </param>
        /// <param name="p2"> The other wall endpoint </param>
        public Wall(int id, Point2D p1, Point2D p2)
        {
            WallId = id;
            P1 = p1;
            P2 = p2;
        }

        /// <summary>
        /// Default Wall constructor.
        /// </summary>
        public Wall()
        {
            WallId = 0;
            P1 = new Point2D();
            P2 = new Point2D();
        }

        /// <summary>
        /// Gets the first point of the wall
        /// </summary>
        /// <returns>Returns a Point2D object</returns>
        public Point2D GetPointOne()
        {
            return P1;
        }

        /// <summary>
        /// Gets the second point of the wall
        /// </summary>
        /// <returns>Returns a Point2D object</returns>
        public Point2D GetPointTwo()
        {
            return P2;
        }

        /// <summary>
        /// Gets the ID of the powerup
        /// </summary>
        /// <returns>Returns the ID of the powerup</returns>
        public int GetWallId()
        {
            return WallId;
        }
    }
}