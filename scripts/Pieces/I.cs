
namespace Pieces
{
    public class I : Piece
    {
        public I(Block color = Block.Cyan) : base(color) 
        {
            var c = color;
            _posRotationalModifier = new(1, 1);
            _blocks = new Block[][,]{
                new Block[,] { { c, c, c, c } },
                new Block[,] {{c}, {c}, {c}, {c} },
                new Block[,] { { c, c, c, c } },
                new Block[,] {{c}, {c}, {c}, {c} },
            };
        }
    }
}
