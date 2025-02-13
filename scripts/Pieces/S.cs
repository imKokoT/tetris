
namespace Pieces
{
    public class S : Piece
    {
        public S(Block color = Block.Green) : base(color) 
        {
            var c = color;
            _blocks = 
                new Block[,] { {0, c, 0 },
                               {c, c, 0 },
                               {c, 0, 0 }};
        }
    }
}
