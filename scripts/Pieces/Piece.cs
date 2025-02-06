using Godot;


namespace Pieces
{
    public abstract class Piece
    {
        public readonly Block color;
        public Vector2I pos = new(4, 0);
        
        protected int _rotation = 0;
        /// <summary>
        /// Figure rotation, where:
        /// - 0 -> 0 
        /// - 1 -> 90 
        /// - 2 -> 180 
        /// - 3 -> 270
        /// </summary>
        public int Rotation 
        { 
            get => _rotation; 
            set => _rotation = value % 4; 
        }

        /// <summary>
        /// represents 4 figure's rotations as set of blocks, where 1 is block
        /// </summary>
        protected Block[][,] _blocks;
        /// <summary>
        /// returns figures blocks
        /// </summary>
        public Block[,] Blocks => _blocks[_rotation];

        public Block[,] GetBlocksByRot(byte rot) => _blocks[rot%4];
        
        /// <summary>
        /// In constructor should be created figure blocks!
        /// </summary>
        /// <param name="color">figure color</param>
        public Piece(Block color)
        {
            this.color = color;
        }
    }

}
