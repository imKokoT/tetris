using Godot;
using System;

public partial class Play : Button
{
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(OnRelease)));
    }

    private void OnRelease()
    {
        GetTree().ChangeSceneToFile("res://scenes/game.tscn");
    }
}
