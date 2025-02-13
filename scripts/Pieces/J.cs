
namespace Pieces
{
    public class J : Piece
    {
        public J(Block color = Block.Blue) : base(color) 
        {
            var c = color;
            pos = new(4, -1);
            _blocks = 
                new Block[,] { {0, c, 0 },
                               {0, c, 0 },
                               {0, c, c }};
        }
    }
}
