
namespace Pieces
{
    public class J : Piece
    {
        public J(Block color = Block.Blue) : base(color) 
        {
            var c = color;
            _blocks = new Block[][,]{
                new Block[,] {{0, 0, 0, c },
                              {c, c, c, c }},
                new Block[,] { {c, c },
                               {0, c },
                               {0, c },
                               {0, c }},
                new Block[,] {{c, c, c, c },
                              {c, 0, 0, 0 }},
                new Block[,] { {c, 0 },
                               {c, 0 },
                               {c, 0 },
                               {c, c }, }
                
            };
        }
    }
}
