using Godot;


public partial class InputController : Node
{
    GridData _gridData;
    Tetris _tetris;

    public override void _Ready()
    {
        _gridData = GameData.Instance.GridData;
        _tetris = GetNode<Tetris>("/root/Tetris");
    }

    public override void _Process(double delta)
    {


        //  --- piece control ------------------------------------------------
        var piece = _gridData.Piece;
        if (piece == null) return;

        if (Input.IsActionJustPressed("rot-right") && piece.CanRotTo(piece.Rotation+1))
        {
            piece.Rotation++;
            _tetris.UpdateTiles();
        }
        if (Input.IsActionJustPressed("rot-left") && piece.CanRotTo(piece.Rotation-1))
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
            GameData.Instance.CurrentDelay = GameData.MIN_DELAY;
        else
            GameData.Instance.CurrentDelay = GameData.Instance.UpdateDelay;
    }
}
