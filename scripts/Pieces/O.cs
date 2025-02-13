
namespace Pieces
{
    public class O : Piece
    {
        public O(Block color = Block.Yellow) : base(color) 
        {
            var c = color;
            _blocks = 
                new Block[,] {{c, c},
                              {c, c}};
        }
    }
}
