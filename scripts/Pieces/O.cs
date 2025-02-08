
namespace Pieces
{
    public class O : Piece
    {
        public O(Block color = Block.Yellow) : base(color) 
        {
            var c = color;
            _posRotationalModifier = new(0, 0);
            _blocks = new Block[][,]{
                new Block[,] {{c, c},
                              {c, c}},
                new Block[,] {{c, c},
                              {c, c}},
                new Block[,] {{c, c},
                              {c, c}},
                new Block[,] {{c, c},
                              {c, c}},
            };
        }
    }
}
