using Godot;
using System;

public partial class Exit : Button
{
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(OnRelease)));
    }

    private void OnRelease()
    {
        GetTree().Quit();
    }
}
