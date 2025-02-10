
public class GameData
{
    public static GameData Instance { get; private set; } = new GameData();
    public const float MIN_DELAY = 0.025f;

    public GameState State { get; set; }
    public int Score { get; set; }
    public Grid GridData { get; set; } = new Grid();

    public float CurrentDelay = 1;
    public float UpdateDelay = 1;
}

public enum GameState
{
    Play,
    Pause,
    Menu,
    GameOver
}
