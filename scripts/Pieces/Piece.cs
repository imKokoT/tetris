using Godot;
using System;


namespace Pieces
{
    public abstract class Piece
    {
        public readonly Block color;
        public Vector2I pos = new(4, 0);
        
        protected Vector2I _posRotationalModifier= new(1, 0);
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
            set {
                _rotation = Mathf.PosMod(value, 4);
                pos += _rotation switch
                {
                    0 => new(_posRotationalModifier.X, -_posRotationalModifier.Y),
                    1 => new(-_posRotationalModifier.X, _posRotationalModifier.Y),
                    2 => new(_posRotationalModifier.X, -_posRotationalModifier.Y),
                    3 => new(-_posRotationalModifier.X, _posRotationalModifier.Y),
                    _ => throw new NotImplementedException()
                };
            }
        }

        /// <summary>
        /// represents 4 figure's rotations as set of blocks, where 1 is block
        /// </summary>
        protected Block[][,] _blocks;
        /// <summary>
        /// returns figures blocks
        /// </summary>
        public Block[,] Blocks => _blocks[_rotation];


        public Block[,] GetBlocksByRot(byte rot) => _blocks[Mathf.PosMod(rot, 4)];
        
        public Block GetBlockFromPos(Vector2I position)
        {
            var dpos = position - pos;
            if (dpos.X < 0 || dpos.X >= Blocks.GetLength(0) || dpos.Y < 0 || dpos.Y >= Blocks.GetLength(1))
                return Block.None;
            return Blocks[dpos.X, dpos.Y];
        }
        public Block GetBlockFromPos(int x, int y) => 
            GetBlockFromPos(new Vector2I(x, y));

        #region Conditions
        public bool HasBlockAtPos(Vector2I pos) => 
            GetBlockFromPos(pos) != Block.None;
        public bool HasBlockAtPos(int x, int y) =>
            GetBlockFromPos(x, y) != Block.None;

        public bool CanMoveAt(Vector2I pos)
        {
            GridData grid = GameData.Instance.GridData;

            for (int x = 0; x < Blocks.GetLength(0); x++)
                for (int y = 0; y < Blocks.GetLength(1); y++)
                    if (Blocks[x,y] != Block.None && grid.GetWorldBlock(this.pos.X + pos.X + x, this.pos.Y + pos.Y + y) != Block.None)
                        return false;
            return true;
        }

        public bool CanRotTo(int rotation)
        {
            int tmp = Rotation - rotation;
            Rotation += tmp;
            bool can = CanMoveAt(Vector2I.Zero);
            Rotation -= tmp;
            return can;
        }

        #endregion

        /// <summary>
        /// In constructor should be created figure blocks!
        /// </summary>
        /// <param name="color">figure color</param>
        public Piece(Block color)
        {
            this.color = color;
        }

        public static Piece GenerateRandPiece()
        {
            // TODO: implement other pieces
            int p = GD.RandRange(1, 7);
            return p switch
            {
                1 => new I(),
                2 => new L(),
                3 => new J(),
                4 => new T(),
                5 => new S(),
                6 => new Z(),
                7 => new O(),
                _ => throw new NotImplementedException($"Piece not exists"),
            };
        } 
    }

}
