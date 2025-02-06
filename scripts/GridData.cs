
public class GridData
{
    public const int xMax = 10, yMax = 20;
    public Block[,] data = new Block[xMax, yMax];

    public Block GetBlock(int x, int y)
    {
        if (x < 0 || x > xMax || y < 0 || y > yMax)
            return Block.Wall;
        return data[x, y];
    }
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
