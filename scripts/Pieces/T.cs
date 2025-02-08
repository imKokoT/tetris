
namespace Pieces
{
    public class T : Piece
    {
        public T(Block color = Block.Purple) : base(color) 
        {
            var c = color;
            _blocks = new Block[][,]{
                new Block[,] {{c, c, c},
                              {0, c, 0 }},
                new Block[,] { {c, 0 },
                               {c, c },
                               {c, 0 }},
                new Block[,] {{ 0, c, 0 },
                              { c, c, c }},
                new Block[,] { {0 ,c },
                               {c ,c },
                               {0 ,c }}
            };
        }
    }
}
