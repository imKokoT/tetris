
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


    public GameData()
    {
        LoadData();
    }

    public void SaveData()
    {
        var config = new ConfigFile();

        config.SetValue("", "high_score", HighScore);

        if (OS.IsDebugBuild() && !OS.HasFeature("template"))
            config.Save("res://_saves//data.cfg");
        else
            config.Save("user://data.cfg");

        GD.Print("saved data.cfg");
    }

    public void LoadData()
    {
        var config = new ConfigFile();
        Error err;

        if (OS.IsDebugBuild() && !OS.HasFeature("template"))
            err = config.Load("res://_saves//data.cfg");
        else
            err = config.Load("user://data.cfg");

        if (err != Error.Ok)
        {
            GD.PushWarning("failed to load data.cfg");
            return;
        }

        HighScore = (int)config.GetValue("", "high_score");

        GD.Print("loaded data.cfg");
    }
}

public enum GameState
{
    Play,
    Pause,
    Menu,
    GameOver
}
