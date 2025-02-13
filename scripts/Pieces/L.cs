
namespace Pieces
{
    public class L : Piece
    {
        public L(Block color = Block.Orange) : base(color) 
        {
            var c = color;
            pos = new(4, -1);
            _blocks = new Block[,] {{0, c, c },
                                    {0, c, 0 },
                                    {0, c, 0 }};
        }
    }
}
