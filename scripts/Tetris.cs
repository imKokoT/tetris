using Godot;
using Pieces;
using System;
using System.Threading.Tasks;


public partial class Tetris : Node
{
    GridData _gridData;
    TileMapLayer _tileGrid;

    public override async void _Ready()
    {
        _gridData = GameData.Instance.GridData;    
        _tileGrid = GetNode("%grid") as TileMapLayer;

        UpdateTiles();
        await Start();
    }

    /// <summary>
    /// Start game loop
    /// </summary>
    public async Task Start()
    {
        while (GameData.Instance.State != GameState.GameOver)
            await _Update();
    }


    private async Task _Update()
    {
        var piece = _gridData.Piece;
        await Task.Delay(GameData.Instance.CurrentDelay);

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
    }

    public void UpdateTiles()
    {
        for (int x = 0; x < GridData.xMax; x++)
            for (int y = 0; y < GridData.yMax; y++)
            {
                var current = _gridData.GetBlock(x, y);
                if (current != Block.None)
                    _tileGrid.SetCell(new Vector2I(x, y), 0, Vector2I.Zero, (int)current);
                else
                    _tileGrid.SetCell(new Vector2I(x, y), -1);
            }
    }
}
