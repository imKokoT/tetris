using Godot;

public class GameData
{
    public const float MIN_DELAY = 0.040f, DEFAULT_DELAY = 1;

    public static GameData Instance { get; private set; } = new GameData();
    public static void Recreate() => Instance = new GameData();

    public GameState State { get; set; }
    public int rotationBonusMultiplier { get; set; }
    public Grid Grid { get; set; } = new Grid();

    public float CurrentDelay = DEFAULT_DELAY;
    public float UpdateDelay = DEFAULT_DELAY;

    public int Score { get; set; }
    public int HighScore { get; set; }
    public int Level { get; set; }
    public int HighLevel { get; set; }
    public int Tricks { get; set; }
    public int HighTricks { get; set; }
    public int OpenedGaps { get; set; }
    public int HighOpenedGaps { get; set; }
    public int LinesFilled { get; set; }
    public int HighLinesFilled { get; set; }

    public GameData()
    {
        LoadData();
    }

    public void SaveData()
    {
        var config = new ConfigFile();

        config.SetValue("stat", "high_score", HighScore);
        config.SetValue("stat", "high_level", HighLevel);
        config.SetValue("stat", "high_tricks", HighTricks);
        config.SetValue("stat", "high_opened_gaps", HighOpenedGaps);
        config.SetValue("stat", "high_lines_filed", HighLinesFilled);

        if (OS.IsDebugBuild() && !OS.HasFeature("template"))
            config.Save("res://_saves//data.cfg");
        else
            config.SaveEncryptedPass("user://data.cfg", "92xA[s*Qt-A_BH&WtaX@");

        GD.Print("saved data.cfg");
    }

    public void LoadData()
    {
        var config = new ConfigFile();
        Error err;

        if (OS.IsDebugBuild() && !OS.HasFeature("template"))
            err = config.Load("res://_saves//data.cfg");
        else
            err = config.LoadEncryptedPass("user://data.cfg", "92xA[s*Qt-A_BH&WtaX@");

        if (err != Error.Ok)
        {
            GD.PushWarning("failed to load data.cfg");
            return;
        }

        HighScore = (int)config.GetValue("stat", "high_score", 0);
        HighLevel = (int)config.GetValue("stat", "high_level", 0);
        HighTricks = (int)config.GetValue("stat", "high_tricks", 0);
        HighOpenedGaps = (int)config.GetValue("stat", "high_opened_gaps", 0);
        HighLinesFilled = (int)config.GetValue("stat", "high_lines_filed", 0);

        GD.Print("loaded data.cfg");
    }
}

public enum GameState
{
    Play,
    Pause,
    GameOver,
    Cutscene
}
