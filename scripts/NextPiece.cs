using Godot;
using Pieces;


public partial class NextPiece : TileMapLayer
{
    Piece _prev;

    public override void _Process(double delta)
    {
        var gridData = GameData.Instance.Grid;
        if (gridData.NextPiece == _prev) return;
        
        _prev = gridData.NextPiece;
        Show(_prev.GetBlocksByRot(0), _prev.color);
    }

    private void Show(Block[,] blocks, Block color)
    {
        Clear();
        for (int x = 0; x < blocks.GetLength(0); x++)
            for (int y = 0; y < blocks.GetLength(1); y++)
                if (blocks[x,y] != Block.None) SetCell(new Vector2I(x, y), 0, Vector2I.Zero, (int)color);
    }
}
