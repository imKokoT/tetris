
namespace Pieces
{
    public class L : Piece
    {
        public L(Block color = Block.Orange) : base(color) 
        {
            var c = color;
            _blocks = new Block[,] {{c, c },
                                    {c, 0 },
                                    {c, 0 },
                                    {c, 0 }};
        }
    }
}
