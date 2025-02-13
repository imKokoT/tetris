
namespace Pieces
{
    public class T : Piece
    {
        public T(Block color = Block.Purple) : base(color) 
        {
            var c = color;
            _blocks = 
                new Block[,] { {c, 0 },
                               {c, c },
                               {c, 0 }};
        }
    }
}
