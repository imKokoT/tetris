
namespace Pieces
{
    public class J : Piece
    {
        public J(Block color = Block.Blue) : base(color) 
        {
            var c = color;
            _blocks = 
                new Block[,] { {c, c },
                               {0, c },
                               {0, c },
                               {0, c }};
        }
    }
}
