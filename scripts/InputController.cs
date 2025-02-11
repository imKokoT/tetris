using Godot;
using Pieces;


public partial class InputController : Node
{
    Grid _gridData;
    Tetris _tetris;

    public override void _Ready()
    {
        _gridData = GameData.Instance.GridData;
        _tetris = GetNode<Tetris>("/root/Tetris");
    }

    public override void _Process(double delta)
    {
        OnPlay();
    }

    private void OnPlay()
    {
        if (GameData.Instance.State != GameState.Play) return;

            var piece = _gridData.Piece;
        if (piece == null) return;

        if (Input.IsActionJustPressed("rot-right") && piece.CanRotTo(piece.Rotation + 1))
        {
            piece.Rotation++;
            _tetris.UpdateTiles();
        }
        if (Input.IsActionJustPressed("rot-left") && piece.CanRotTo(piece.Rotation - 1))
        {
            piece.Rotation--;
            _tetris.UpdateTiles();
        }

        if (Input.IsActionJustPressed("move-right") && piece.CanMoveAt(Vector2I.Right))
        {
            piece.pos.X++;
            _tetris.UpdateTiles();
        }
        if (Input.IsActionJustPressed("move-left") && piece.CanMoveAt(Vector2I.Left))
        {
            piece.pos.X--;
            _tetris.UpdateTiles();
        }

        if (Input.IsActionPressed("move-down"))
        {
            GameData.Instance.CurrentDelay = GameData.MIN_DELAY;
            _tetris.GameLoopTimer.WaitTime = GameData.Instance.CurrentDelay;

        }
        else GameData.Instance.CurrentDelay = GameData.Instance.UpdateDelay;
        if (Input.IsActionJustPressed("move-down"))
            _tetris.GameLoopTimer.Start();
    }
}
