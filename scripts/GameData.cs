
using Godot;

public class GameData
{
    public static GameData Instance { get; private set; } = new GameData();
    public const float MIN_DELAY = 0.025f;

    public GameState State { get; set; }
    public int Score { get; set; }
    public int HighScore {  get; set; }
    public Grid GridData { get; set; } = new Grid();

    public float CurrentDelay = 1;
    public float UpdateDelay = 1;

    public void SaveData()
    {
        var config = new ConfigFile();

        config.SetValue("", "high_score", HighScore);

        if (OS.IsDebugBuild())
            config.Save("res://_saves//data.cfg");
        else
            config.Save("user://data.cfg");
    }
}

public enum GameState
{
    Play,
    Pause,
    Menu,
    GameOver
}
