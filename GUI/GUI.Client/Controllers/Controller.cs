// Written by bananathrowingmachine and [redacted] on November 17, 2024

using GUI.Client.Models;

// ReSharper disable once CheckNamespace
namespace SnakeGame.Networking;

/// <summary>
/// Small static class that converts key presses to direct strings to be sent to the server
/// </summary>
public static class Controller
{
    /// <summary>
    /// The actual controller, using keys and the dir given by the server
    /// </summary>
    public static string KeyPress(string key, Point2D? dir)
    {
        switch (key)
        {
            case "w":
                if (dir!.Y == 0)
                    return "{\"moving\":\"up\"}";
                break;
            case "a":
                if (dir!.X == 0)
                    return "{\"moving\":\"left\"}";
                break;
            case "s":
                if (dir!.Y == 0)
                    return "{\"moving\":\"down\"}";
                break;
            case "d":
                if (dir!.X == 0)
                    return "{\"moving\":\"right\"}";
                break;
        }
        return "";
    }
}