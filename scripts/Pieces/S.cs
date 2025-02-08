
namespace Pieces
{
    public class S : Piece
    {
        public S(Block color = Block.Green) : base(color) 
        {
            var c = color;
            _blocks = new Block[][,]{
                new Block[,] {{c, c, 0 },
                              {0, c, c }},
                new Block[,] { {0, c },
                               {c, c },
                               {c, 0 }},
                new Block[,] {{c, c, 0},
                              {0, c, c}},
                new Block[,] { {0, c },
                               {c, c },
                               {c, 0 }}
            };
        }
    }
}
