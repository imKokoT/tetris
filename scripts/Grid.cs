using Godot;
using Pieces;
using System.Linq;


public class Grid
{
    public const int xMax = 10, yMax = 20;
    public Block[,] world = new Block[xMax, yMax];

    private Piece _piece;
    private Piece _nextPiece = Piece.GenerateRandPiece();
    public Piece Piece => _piece;
    public Piece NextPiece => _nextPiece;

    private bool _filledBefore = false;
    private int _fillMultiplier = 1; 

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
        if (!_nextPiece.CanMoveAt(Vector2I.Zero))
        {
            GameData.Instance.State = GameState.GameOver;
            GD.Print("GAME OVER!");
            return;
        }
        
        _piece = _nextPiece;
        _nextPiece = Piece.GenerateRandPiece();
    }

    public void PlacePiece()
    {
        // place piece
        for (int x = 0; x < _piece.Blocks.GetLength(0); x++)
            for (int y = 0; y < _piece.Blocks.GetLength(1); y++)
                if (_piece.Blocks[x,y] != Block.None)
                    world[_piece.pos.X + x, _piece.pos.Y + y] = _piece.Blocks[x, y];
        _piece = null;

        // count filed lines
        if (_filledBefore)
            _fillMultiplier++;
        else
            _fillMultiplier = 1;

        int filled = 0;
        for (int y = 0; y < yMax; y++)
        {
            if (world.GetRow(y).Contains(Block.None)) continue;

            filled++;
            for (int dy = y; dy > 0; dy--)
                world.SetRow(world.GetRow(dy - 1), dy);
        }
        _filledBefore = filled > 0;
        GameData.Instance.Score += filled > 0 ? 100 * (int)Mathf.Pow(2, filled) * _fillMultiplier : 0;
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

static class BlockExtensions
{
    public static Block[] GetRow(this Block[,] grid, int rowIndex)
    {
        Block[] row = new Block[grid.GetLength(0)];
        for (int col = 0; col < grid.GetLength(0); col++)
            row[col] = grid[col, rowIndex];
        return row;
    }

    public static void SetRow(this Block[,] grid, Block[] row, int rowIndex) {
        for (int col = 0; col < grid.GetLength(0); col++)
            grid[col, rowIndex] = row[col];
    }

    public static Block[,] TransformRight(this Block[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        Block[,] tmp = new Block[cols, rows];

        for (int row = 0; row < rows; row++)
            for (int col = 0; col < cols; col++)
                tmp[col, rows - 1 - row] = grid[row, col];

        return tmp;
    }

    public static Block[,] TransformLeft(this Block[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        Block[,] tmp = new Block[cols, rows];

        for (int row = 0; row < rows; row++)
            for (int col = 0; col < cols; col++)
                tmp[cols - 1 - col, row] = grid[row, col];

        return tmp;
    }
}
