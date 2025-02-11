using Godot;
using System.Threading.Tasks;


public partial class Tetris : Node
{
    public Timer GameLoopTimer { get; private set; } = new Timer();

    Grid _gridData;
    TileMapLayer _tileGrid;
    Control _gameOverGUI = GD.Load<PackedScene>("res://scenes/gameover.tscn").Instantiate<Control>();

    public override async void _Ready()
    {
        _gridData = GameData.Instance.GridData;    
        _tileGrid = GetNode<TileMapLayer>("%grid");

        // setup timer
        GameLoopTimer.WaitTime = GameData.Instance.CurrentDelay;
        AddChild(GameLoopTimer);

        // start game
        UpdateTiles();
        await Start();
    }

    /// <summary>
    /// Start game loop
    /// </summary>
    public async Task Start()
    {
        GameLoopTimer.Start();
        while (GameData.Instance.State != GameState.GameOver)
            await _Update();

        OnGameOver();
    }

    private void OnGameOver()
    {
        // TODO: save score

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

        //if (Input.IsActionPressed("move-down"))
        //    GameData.Instance.Score++;

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
