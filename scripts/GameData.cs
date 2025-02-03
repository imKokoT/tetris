using Godot;
using System;

public class GameData
{
    public static GameData Instance { get; private set; } = new GameData();

    public GameState State { get; set; }
    public int Score { get; set; }
}

public enum GameState
{
    Play,
    Pause,
    Menu
}
