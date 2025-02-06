
namespace Pieces
{
    public class L : Piece
    {
        public L(Block color = Block.Orange) : base(color) 
        {
            var c = color;
            _posRotationalModifier = new(1, 0);
            _blocks = new Block[][,]{
                new Block[,] {{c, c, c, c },
                              {0, 0, 0, c }},
                new Block[,] { {c, c },
                               {c, 0 },
                               {c, 0 },
                               {c, 0 }},
                new Block[,] {{c, 0, 0, 0 },
                              {c, c, c, c }},
                new Block[,] { {0 ,c },
                               {0 ,c },
                               {0 ,c },
                               {c ,c }, }
                
            };
        }
    }
}
