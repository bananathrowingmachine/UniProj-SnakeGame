// Written by bananathrowingmachine and [redacted] on November 17, 2024

namespace GUI.Client.Models
{
    /// <summary>
    ///     Used to represent 2D points in space.
    /// </summary>
    public class Point2D
    {
        /// <summary>
        ///     X point in space
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///     Y point in space
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Sets a point2D to (0, 0)
        /// </summary>
        public void SetToBasic()
        {
            X = Y = 0;
        }
    }
}