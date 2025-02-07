using Pieces;

public class GridData
{
    public const int xMax = 10, yMax = 20;
    public Block[,] world = new Block[xMax, yMax];

    private Piece _piece;
    public Piece Piece => _piece;


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
    public void SpawnPiece(Piece piece)
    {
        // TODO: conditions to spawn piece or game over
        _piece = piece;
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
