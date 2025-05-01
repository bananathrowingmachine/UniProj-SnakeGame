// Written by bananathrowingmachine and [redacted] on November 17, 2024

namespace GUI.Client.Models
{
    /// <summary>
    /// The Model part of MVC, represents all objects in the "game"
    /// </summary>
    public class World
    {
        /// <summary>
        /// The players in the game
        /// </summary>
        private Dictionary<int, Snake> Snakes { get; }

        /// <summary>
        /// The powerups in the game
        /// </summary>
        private Dictionary<int, Powerup> Powerups { get; }

        /// <summary>
        /// The powerups in the game
        /// </summary>
        private Dictionary<int, Wall> Walls { get; }

        /// <summary>
        /// The size of a single side of the square world
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Creates a new world with the given size
        /// </summary>
        public World()
        {
            Snakes = new Dictionary<int, Snake>();
            Powerups = new Dictionary<int, Powerup>();
            Walls = new Dictionary<int, Wall>();
            Size = 0;
        }

        /// <summary>
        /// Shallow copy constructor
        /// </summary>
        /// <param name="world">The world object to be copied</param>
        public World(World world)
        {
            Snakes = new Dictionary<int, Snake>(world.Snakes);
            Powerups = new Dictionary<int, Powerup>(world.Powerups);
            Walls = new Dictionary<int, Wall>(world.Walls);
            Size = world.Size;
        }

        /// <summary>
        /// Sets the size of the world.
        /// </summary>
        /// <param name="size">The size to set the world.</param>
        public void SetSize(int size)
        {
            Size = size;
        }
        
        /// <summary>
        /// Add a wall to the world
        /// </summary>
        /// <param name="wall">The wall to be added</param>
        public void AddWall(Wall wall)
        {
            Walls[wall.GetWallId()] = wall;
        }

        /// <summary>
        /// Add a snake to the world
        /// </summary>
        /// <param name="snake">The snake to be added</param>
        public void AddSnake(Snake snake)
        {
            Snakes[snake.GetSnakeId()] = snake;
        }

        /// <summary>
        /// Add a powerup to the world
        /// </summary>
        /// <param name="powerup">The powerup to be added</param>
        public void AddPowerup(Powerup powerup)
        {
            Powerups[powerup.GetPowerId()] = powerup;
        }

        /// <summary>
        /// Remove a snake from the world
        /// </summary>
        /// <param name="snake">The snake to be removed</param>
        public void RemoveSnake(Snake snake)
        {
            Snakes.Remove(snake.GetSnakeId());
        }

        /// <summary>
        /// Remove a snake from the world by id
        /// </summary>
        /// <param name="snakeId">The snake to be removed</param>
        public void RemoveSnake(int snakeId)
        {
            Snakes.Remove(snakeId);
        }

        /// <summary>
        /// Remove a powerup from the world
        /// </summary>
        /// <param name="powerup">The powerup to be removed</param>
        public void RemovePowerup(Powerup powerup)
        {
            Powerups.Remove(powerup.GetPowerId());
        }

        /// <summary>
        /// Retrieve a stored snake object
        /// </summary>
        /// <param name="snakeId">The ID of the snake</param>
        /// <returns>The stored snake object which is attached to the ID</returns>
        public Snake GetSnake(int snakeId)
        {
            return Snakes[snakeId];
        }

        /// <summary>
        /// Retrieve a stored wall object
        /// </summary>
        /// <param name="wallId">The ID of the wall</param>
        /// <returns>The stored wall object which is attached to the ID</returns>
        public Wall GetWall(int wallId)
        {
            return Walls[wallId];
        }

        /// <summary>
        /// Retrieve a stored powerup object
        /// </summary>
        /// <param name="powerupId">The ID of the powerup</param>
        /// <returns>The stored powerup object which is attached to the ID</returns>
        public Powerup GetPowerup(int powerupId)
        {
            return Powerups[powerupId];
        }

        /// <summary>
        /// Get the amount of snakes in the world
        /// </summary>
        /// <returns>Returns the count as an int</returns>
        public int SnakeCount()
        {
            return Snakes.Count;
        }

        /// <summary>
        /// Get the amount of powerups in the world
        /// </summary>
        /// <returns>Returns the count as an int</returns>
        public int PowerupCount()
        {
            return Powerups.Count;
        }

        /// <summary>
        /// Get the list of all wall dictionary keys stored, which is all wall ID's
        /// </summary>
        /// <returns>Returns a list of all wall keys</returns>
        public List<int> WallKeys()
        {
            return Walls.Keys.ToList();
        }

        /// <summary>
        /// Get the list of all snake dictionary keys stored, which is all snake ID's
        /// </summary>
        /// <returns>Returns a list of all snake keys</returns>
        public List<int> SnakeKeys()
        {
            return Snakes.Keys.ToList();
        }

        /// <summary>
        /// Get the list of all powerup dictionary keys stored, which is all powerup ID's
        /// </summary>
        /// <returns>Returns a list of all powerup keys</returns>
        public List<int> PowerupKeys()
        {
            return Powerups.Keys.ToList();
        }

        /// <summary>
        /// Gets a list of all snake objects
        /// </summary>
        /// <returns>A list of all snake objects</returns>
        public List<Snake> GetSnakes()
        {
            return Snakes.Values.ToList();
        }
    }
}