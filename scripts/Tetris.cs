using Godot;
using Pieces;
using System.Threading.Tasks;


public partial class Tetris : Node
{
    GridData _gridData;
    TileMapLayer _tileGrid;

    public override async void _Ready()
    {
        _gridData = GameData.Instance.GridData;      
        _tileGrid = GetNode("%grid") as TileMapLayer;
        _gridData.SpawnPiece(new I());

        _UpdateTiles();
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
        await Task.Delay(GameData.Instance.UpdateDelay);
        
        _UpdateTiles();
    }

    private void _UpdateTiles()
    {
        for (int x = 0; x < GridData.xMax; x++)
            for (int y = 0; y < GridData.yMax; y++)
                if (_gridData.world[x, y] != Block.None)
                    _tileGrid.SetCell(new Vector2I(x, y), 0, Vector2I.Zero, (int)_gridData.GetBlock(x,y));
                else
                    _tileGrid.SetCell(new Vector2I(x, y), -1);
    }
}
