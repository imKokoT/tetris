using Godot;
using Pieces;
using System;
using System.Linq;


public class Grid
{
    public const int xMax = 10, yMax = 20;
    public Block[,] world = new Block[xMax, yMax];

    private Piece _piece;
    private Piece _nextPiece = Piece.GenerateRandPiece();
    public Piece Piece => _piece;
    public Piece NextPiece => _nextPiece;

    private bool _filledBefore = false;
    private int _fillMultiplier = 1; 

    # region methods
    public Block GetBlock(int x, int y)
    {
        if (x < 0 || x >= xMax || y < 0 || y >= yMax)
            return Block.Wall;
        if (_piece != null && _piece.HasBlockAtPos(x,y))
            return _piece.color;
        if (_piece != null && _piece.HasHintBlockAtPos(x,y))
            return _piece.hintColor;
        return world[x, y];
    }

    public Block GetWorldBlock(int x, int y)
    {
        if (x < 0 || x >= xMax || y < 0 || y >= yMax)
            return Block.Wall;

        // transform gaps into air
        return world[x, y] == Block.Gap ? Block.None : world[x, y];
    }

    /// <summary>
    /// attempt to spawn piece
    /// </summary>
    public void SpawnPiece()
    {
        if (!_nextPiece.CanMoveAt(Vector2I.Zero))
        {
            GameData.Instance.State = GameState.GameOver;
            GD.Print("GAME OVER!");
            return;
        }
        
        _piece = _nextPiece;
        _nextPiece = Piece.GenerateRandPiece();
    }

    public void PlacePiece()
    {
        // --- count trick bonus ----------------------------------------------
        var bonus = 10;
        var rotBonus = Math.Clamp(
            Math.Abs(GameData.Instance.rotationBonusMultiplier),
            -4, 4
        );
        if (_piece is O)
            rotBonus = 0;

        if (!_piece.CanMoveAt(Vector2I.Up))
        {
            bonus += 30 + (10 * (int)Mathf.Pow(2, rotBonus));
            GD.Print($"bonus for trick: {bonus}");
        }
        else if (!_piece.CanMoveAt(Vector2I.Up * 2))
        {
            bonus += 20;
            GD.Print($"little bonus for trick: {bonus}");
        }
        GameData.Instance.Score += bonus;

        // --- place piece -----------------------------------------------------
        for (int x = 0; x < _piece.Blocks.GetLength(0); x++)
        {
            for (int y = 0; y < _piece.Blocks.GetLength(1); y++)
                if (_piece.Blocks[x,y] != Block.None)
                    world[_piece.pos.X + x, _piece.pos.Y + y] = _piece.Blocks[x, y];
            
            // mark gaps
            if (_piece.pos.X + x < 0 || _piece.pos.X + x >= xMax) 
                continue;

            var col = world.GetCol(_piece.pos.X + x);
            bool barrier = false;
            for (int y = 0; y < col.Length; y++)
            {
                if (!barrier && col[y] != Block.None) 
                    barrier = true;
                else if (barrier && col[y] == Block.None)
                    world[_piece.pos.X + x, y] = Block.Gap;
            }
        }

        // --- count filed lines -----------------------------------------------
        if (_filledBefore)
            _fillMultiplier++;
        else
            _fillMultiplier = 1;

        int filled = 0;
        for (int y = 0; y < yMax; y++)
        {
            if (world.GetRow(y).Contains(Block.None) || world.GetRow(y).Contains(Block.Gap)) continue;

            filled++;
            for (int dy = y; dy > 0; dy--)
                world.SetRow(world.GetRow(dy - 1), dy);
        }
        GameData.Instance.Score += filled > 0 ? 100 * (int)Mathf.Pow(2, filled) * _fillMultiplier : 0;

        // --- count bonus for opened holes ----------------------------------------


        // --- apply game speed ----------------------------------------------------
        if (filled > 0 && !_filledBefore)
        {
            var gd = GameData.Instance;

            gd.UpdateDelay *= gd.Level < 100 ? 1f - 0.05468f * (1f - gd.Level / 100f) : 1;
            gd.CurrentDelay = gd.UpdateDelay;
            gd.Level++;
            GD.Print($"Level {gd.Level+1}; Game speed changed to {gd.CurrentDelay:F3} sec/upd!");
        }

        _filledBefore = filled > 0;
        _piece = null;
    }

    #endregion
}
