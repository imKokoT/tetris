using Godot;
using System;

public class GameData
{
    public static GameData Instance { get; private set; } = new GameData();

    public GameState State { get; set; }
    public int Score { get; set; }
    public GridData GridData { get; set; } = new GridData();
}

public enum GameState
{
    Play,
    Pause,
    Menu
}
