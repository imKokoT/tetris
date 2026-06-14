
namespace Pieces
{
    public class Z : Piece
    {
        public Z() : base(Block.Red, Block.Hint) 
        {
            var c = color;
            _blocks = 
                new Block[,] { {c, 0, 0 },
                               {c, c, 0 },
                               {0, c, 0 }};
        }
    }
}
