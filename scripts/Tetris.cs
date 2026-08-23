using Godot;
using System.Threading.Tasks;


public partial class Tetris : Node
{
    [Export]public bool debug = false;

    public Timer GameLoopTimer { get; private set; } = new Timer();
    public TileMapLayer tileGrid;

    Grid _gridData;
    AudioStreamPlayer _player;

    Control _gameOverGUI = GD.Load<PackedScene>("res://scenes/gameover.tscn").Instantiate<Control>();
    Control _onStartGUI;

    AudioStream _tickSound = GD.Load<AudioStream>("res://sounds/tick.ogg");
    AudioStream _placeSound = GD.Load<AudioStream>("res://sounds/place.ogg");

    public override async void _Ready()
    {
        _gridData = GameData.Instance.Grid;    
        tileGrid = GetNode<TileMapLayer>("%grid");
        _onStartGUI = GetNode<Control>("%GUI/OnStart");
        _player = new AudioStreamPlayer {
            VolumeDb = -2
        };
        AddChild(_player);

        // setup timer
        GameLoopTimer.WaitTime = GameData.Instance.CurrentDelay;
        AddChild(GameLoopTimer);

        // start game
        Input.MouseMode = Input.MouseModeEnum.Captured;
        UpdateTiles();
        await Start();
    }

    /// <summary>
    /// Start game loop
    /// </summary>
    public async Task Start()
    {
        // on start gui cutscene
        GameData.Instance.State = GameState.Cutscene;
        _onStartGUI.Visible = true;
        await ToSignal(_onStartGUI.GetNode<AnimationPlayer>("AnimationPlayer"), "animation_finished");

        // main game
        GameData.Instance.State = GameState.Play;
        GameLoopTimer.WaitTime = 0.01;
        GameLoopTimer.Start();
        while (GameData.Instance.State != GameState.GameOver)
            await _Update();

        // game over
        OnGameOver();
    }

    private void OnGameOver()
    {
        var resultText = $"- score: {GameData.Instance.Score}\n" +
                         $"- level: {GameData.Instance.Level}\n" +
                         $"- tricks: {GameData.Instance.Tricks}\n" +
                         $"- opened gaps: {GameData.Instance.OpenedGaps}\n" +
                         $"- lines filled: {GameData.Instance.LinesFilled}\n";
        _gameOverGUI.GetNode<Label>("stats").Text =resultText;
        GD.Print(resultText);

        if (GameData.Instance.Score > GameData.Instance.HighScore)
        {
            GD.Print($"new high score {GameData.Instance.Score}!");
            _gameOverGUI.GetNode<Label>("high-score").Visible = true;
        }

        GameData.Instance.HighScore = GameData.Instance.Score > GameData.Instance.HighScore ? 
            GameData.Instance.Score : GameData.Instance.HighScore;
        GameData.Instance.HighLevel = GameData.Instance.Level > GameData.Instance.HighLevel ? 
            GameData.Instance.Level : GameData.Instance.HighLevel;
        GameData.Instance.HighTricks = GameData.Instance.Tricks > GameData.Instance.HighTricks ? 
            GameData.Instance.Tricks : GameData.Instance.HighTricks;
        GameData.Instance.HighOpenedGaps = GameData.Instance.OpenedGaps > GameData.Instance.HighOpenedGaps ? 
            GameData.Instance.OpenedGaps : GameData.Instance.HighOpenedGaps;
        GameData.Instance.HighLinesFilled = GameData.Instance.LinesFilled > GameData.Instance.HighLinesFilled ? 
            GameData.Instance.LinesFilled : GameData.Instance.HighLinesFilled;
        GameData.Instance.SaveData();
        

        GameLoopTimer.Stop();
        GetNode("%GUI").AddChild(_gameOverGUI);
    }

    public void PredictHint()
    {
        var piece = _gridData.Piece;
        if (piece == null) return;

        // reset hint pos
        piece.HintPos = new(0, piece.pos.Y);

        //predict hint position
        while (piece.CanHintMoveAt(Vector2I.Down))
            piece.HintPos = new(piece.HintPos.X, piece.HintPos.Y+1);

    }

    private async Task _Update()
    {
        await ToSignal(GameLoopTimer, "timeout");

        var piece = _gridData.Piece;

        // spawn new peace
        if (piece == null)
        {
            _gridData.SpawnPiece();
            PredictHint();
            
            UpdateTiles();
            GameLoopTimer.WaitTime = 1;
            GameLoopTimer.Start();
            return;
        }

        PredictHint();

        if (piece.CanMoveAt(Vector2I.Down))
        {
            _player.Stream = _tickSound;
            _player.Play();
            piece.pos.Y++;
            GameData.Instance.rotationBonusMultiplier = 0; // reset rotation bonus every tick
        }
        else
        {
            _gridData.PlacePiece();
            _player.Stream = _placeSound;
            _player.Play();
        }
        UpdateTiles();

        GameLoopTimer.WaitTime = GameData.Instance.CurrentDelay;
        GameLoopTimer.Start();
    }

    public void UpdateTiles()
    {
        for (int x = 0; x < Grid.xMax; x++)
            for (int y = 0; y < Grid.yMax; y++)
            {
                var current = _gridData.GetBlock(x, y);
                if (debug && current == Block.Gap)
                    tileGrid.SetCell(new Vector2I(x, y), 0, Vector2I.Zero, (int)Block.Hint);
                else if (current != Block.None)
                    tileGrid.SetCell(new Vector2I(x, y), 0, Vector2I.Zero, (int)current);
                else
                    tileGrid.SetCell(new Vector2I(x, y), -1);
            }
    }
}
