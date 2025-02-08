using Godot;
using Pieces;


public class GridData
{
    public const int xMax = 10, yMax = 20;
    public Block[,] world = new Block[xMax, yMax];

    private Piece _piece;
    private Piece _nextPiece = Piece.GenerateRandPiece();
    public Piece Piece => _piece;
    public Piece NextPiece => _nextPiece;


    # region methods
    public Block GetBlock(int x, int y)
    {
        if (x < 0 || x >= xMax || y < 0 || y >= yMax)
            return Block.Wall;
        if (_piece != null && _piece.HasBlockAtPos(x,y))
            return _piece.color;
        return world[x, y];
    }

    public Block GetWorldBlock(int x, int y)
    {
        if (x < 0 || x >= xMax || y < 0 || y >= yMax)
            return Block.Wall;
        return world[x, y];
    }

    /// <summary>
    /// attempt to spawn piece
    /// </summary>
    public void SpawnPiece()
    {
        // TODO: conditions to spawn piece or game over        
        _piece = _nextPiece;
        _nextPiece = Piece.GenerateRandPiece();
    }

    public void PlacePiece()
    {
        for (int x = 0; x < _piece.Blocks.GetLength(0); x++)
            for (int y = 0; y < _piece.Blocks.GetLength(1); y++)
                if (_piece.Blocks[x,y] != Block.None)
                    world[_piece.pos.X + x, _piece.pos.Y + y] = _piece.Blocks[x, y];
        _piece = null;
    }

    #endregion
}


public enum Block
{
    Wall = -1,
    None,
    Red,
    Green,
    Blue,
    Yellow,
    Orange,
    Cyan,
    Purple,
}
