
namespace Pieces
{
    public class O : Piece
    {
        public O() : base(Block.Yellow, Block.Hint) 
        {
            var c = color;
            _blocks = 
                new Block[,] {{c, c},
                              {c, c}};
        }
    }
}
