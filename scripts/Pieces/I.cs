
namespace Pieces
{
    public class I : Piece
    {
        public I(Block color = Block.Cyan) : base(color) 
        {
            var c = color;
            pos = new(3,-1);
            _blocks = new Block[,]{
                {0,c,0,0},
                {0,c,0,0},
                {0,c,0,0},
                {0,c,0,0},
            };
        }
    }
}
