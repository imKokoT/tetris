
namespace Pieces
{
    public class T : Piece
    {
        public T() : base(Block.Purple, Block.Hint) 
        {
            var c = color;
            pos = new(4, -1);
            _blocks = 
                new Block[,] { {0, c, 0 },
                               {0, c, c },
                               {0, c, 0 }};
        }
    }
}
