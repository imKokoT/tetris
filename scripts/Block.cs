
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

static class BlockEnumExtensions
{
    public static Block[] GetRow(this Block[,] grid, int rowIndex)
    {
        Block[] row = new Block[grid.GetLength(0)];
        for (int col = 0; col < grid.GetLength(0); col++)
            row[col] = grid[col, rowIndex];
        return row;
    }

    public static Block[] GetCol(this Block[,] grid, int colIndex)
    {
        Block[] col = new Block[grid.GetLength(1)];
        for (int row = 0; row < grid.GetLength(1); row++)
            col[row] = grid[colIndex, row];
        return col;
    }

    public static void SetRow(this Block[,] grid, Block[] row, int rowIndex)
    {
        for (int col = 0; col < grid.GetLength(0); col++)
            grid[col, rowIndex] = row[col];
    }

    public static Block[,] TransformLeft(this Block[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        Block[,] tmp = new Block[cols, rows];

        for (int row = 0; row < rows; row++)
            for (int col = 0; col < cols; col++)
                tmp[col, rows - 1 - row] = grid[row, col];

        return tmp;
    }

    public static Block[,] TransformRight(this Block[,] grid)
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
