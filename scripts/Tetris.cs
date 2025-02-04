using Godot;


public enum Block
{
    None,
    Red,
    Green,
    Blue,
    Yellow,
    Orange,
    Cyan,
    Purple
}

public class GridData
{
    public const int xMax = 10, yMax = 20;
    public Block[,] data = new Block[xMax,yMax];
    
}

public partial class Tetris : Node
{

    GridData _gridData;
    TileMapLayer _tileGrid;

    public override void _Ready()
    {
        _gridData = GameData.Instance.GridData;

        _gridData.data[0,0] = Block.Red;
        _gridData.data[1,1] = Block.Green;
        _gridData.data[2,2] = Block.Blue;
        _gridData.data[3,3] = Block.Yellow;
        _gridData.data[4,4] = Block.Cyan;
        _gridData.data[5,5] = Block.Purple;
        _gridData.data[6,6] = Block.Orange;
        
        _tileGrid = GetNode("%grid") as TileMapLayer;

        _UpdateTiles();
    }

    public void Start()
    {

    }

    private void _Update()
    {
        _UpdateTiles();
    }

    private void _UpdateTiles()
    {
        for (int x = 0; x < GridData.xMax; x++)
            for (int y = 0; y < GridData.yMax; y++)
                if (_gridData.data[x, y] != Block.None)
                    _tileGrid.SetCell(new Vector2I(x, y), 0, Vector2I.Zero, (int)_gridData.data[x, y]);
                else
                    _tileGrid.SetCell(new Vector2I(x, y), -1);
    }
}
