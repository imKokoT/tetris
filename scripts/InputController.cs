using Godot;
using Pieces;
using System.Threading.Tasks;


public partial class InputController : Node
{
    Grid _gridData;
    Tetris _tetris;

    Control _pauseGUI;

    public override void _Ready()
    {
        _gridData = GameData.Instance.Grid;
        _tetris = GetNode<Tetris>("/root/Tetris");
        _pauseGUI = GetNode<Control>("%GUI/Pause");
    }

    public override void _Process(double delta)
    {

        if (Input.IsActionJustPressed("exit") &&
            (GameData.Instance.State == GameState.Pause || GameData.Instance.State == GameState.GameOver))
        {
            GetTree().Paused = false;
            GameData.Recreate();
            GetTree().ChangeSceneToFile("res://scenes/menu.tscn");
            Input.MouseMode = Input.MouseModeEnum.Visible;
            GD.Print("exiting to menu scene");
        }
        if (Input.IsActionJustPressed("restart") && GameData.Instance.State == GameState.GameOver)
        {
            GameData.Recreate();
            GetTree().ChangeSceneToFile("res://scenes/game.tscn");
            GD.Print("reloading game scene");
        }

        Play();
        Pause();
    }

    private void Pause()
    {
        if (Input.IsActionJustPressed("pause") && GameData.Instance.State == GameState.Pause)
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
            GameData.Instance.State = GameState.Play;
            _pauseGUI.Visible = false;
            GetTree().Paused = false;
        }
        else if (Input.IsActionJustPressed("pause") && GameData.Instance.State == GameState.Play)
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
            GameData.Instance.State = GameState.Pause;
            _pauseGUI.Visible = true;
            GetTree().Paused = true;
        }
    }

    private void Play()
    {
        if (GameData.Instance.State != GameState.Play) return;
            
        var piece = _gridData.Piece;
        if (piece == null) return;

        if (Input.IsActionJustPressed("rot-right") && piece.CanRotTo(piece.Rotation + 1))
        {
            piece.Rotate(1);
            _tetris.UpdateTiles();
        }
        if (Input.IsActionJustPressed("rot-left") && piece.CanRotTo(piece.Rotation - 1))
        {
            piece.Rotate(-1);
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
