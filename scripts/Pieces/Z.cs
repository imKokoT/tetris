
namespace Pieces
{
    public class Z : Piece
    {
        public Z(Block color = Block.Red) : base(color) 
        {
            var c = color;
            _blocks = new Block[][,]{
                new Block[,] {{0, c, c },
                              {c, c, 0 }},
                new Block[,] { {c, 0 },
                               {c, c },
                               {0, c }},
                new Block[,] {{0, c, c},
                              {c, c, 0}},
                new Block[,] { {c, 0 },
                               {c, c },
                               {0, c}}
            };
        }
    }
}
