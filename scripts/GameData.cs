
public class GameData
{
    public static GameData Instance { get; private set; } = new GameData();
    public const int MIN_DELAY = 20;

    public GameState State { get; set; }
    public int Score { get; set; }
    public GridData GridData { get; set; } = new GridData();

    public int CurrentDelay = 1000;
    public int UpdateDelay = 1000;
}

public enum GameState
{
    Play,
    Pause,
    Menu,
    GameOver
}
