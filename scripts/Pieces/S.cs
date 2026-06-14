
namespace Pieces
{
    public class S : Piece
    {
        public S() : base(Block.Green, Block.Hint) 
        {
            var c = color;
            _blocks = 
                new Block[,] { {0, c, 0 },
                               {c, c, 0 },
                               {c, 0, 0 }};
        }
    }
}
