using Godot;


namespace Figures
{
    public abstract class Figure
    {
        public readonly Block color;
        public readonly Vector2I size;

        public Vector2I pos = new(4, 0);
        
        private int _rotation = 0;
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
        private byte[][,] _blocks;
        /// <summary>
        /// returns figures blocks
        /// </summary>
        public byte[,] Blocks => _blocks[_rotation];

        public byte[,] GetBlocksByRot(byte rot) => _blocks[rot%4];
        
        public Figure(Block color)
        {
            this.color = color;
        }
    }

}
