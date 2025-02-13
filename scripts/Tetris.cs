using Godot;
using System.Threading.Tasks;


public partial class Tetris : Node
{
    public Timer GameLoopTimer { get; private set; } = new Timer();

    Grid _gridData;
    TileMapLayer _tileGrid;
    Control _gameOverGUI = GD.Load<PackedScene>("res://scenes/gameover.tscn").Instantiate<Control>();
    Control _onStartGUI;

    public override async void _Ready()
    {
        _gridData = GameData.Instance.GridData;    
        _tileGrid = GetNode<TileMapLayer>("%grid");
        _onStartGUI = GetNode<Control>("%GUI/OnStart");

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
        GameLoopTimer.Start();
        while (GameData.Instance.State != GameState.GameOver)
            await _Update();

        // game over
        OnGameOver();
    }

    private void OnGameOver()
    {
        if (GameData.Instance.Score > GameData.Instance.HighScore)
        {
            GD.Print($"new high score {GameData.Instance.Score}!");
            GameData.Instance.HighScore = GameData.Instance.Score;
            GameData.Instance.SaveData();
        }

        GameLoopTimer.Stop();
        GetNode("%GUI").AddChild(_gameOverGUI);
    }

    private async Task _Update()
    {
        var piece = _gridData.Piece;

        if (piece != null)
        {

            if (piece.CanMoveAt(Vector2I.Down))
                piece.pos.Y++;
            else
                _gridData.PlacePiece();
        }
        else
        {
            await Task.Delay(1000);
            _gridData.SpawnPiece();
        }

        UpdateTiles();

        await ToSignal(GameLoopTimer, "timeout");
        GameLoopTimer.WaitTime = GameData.Instance.CurrentDelay;
        GameLoopTimer.Start();
    }

    public void UpdateTiles()
    {
        for (int x = 0; x < Grid.xMax; x++)
            for (int y = 0; y < Grid.yMax; y++)
            {
                var current = _gridData.GetBlock(x, y);
                if (current != Block.None)
                    _tileGrid.SetCell(new Vector2I(x, y), 0, Vector2I.Zero, (int)current);
                else
                    _tileGrid.SetCell(new Vector2I(x, y), -1);
            }
    }
}
