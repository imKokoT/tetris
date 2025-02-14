using Godot;
using System;

public partial class Play : Button
{
    AudioStream _clickSound = GD.Load<AudioStream>("res://sounds/tick.ogg");
    AudioStreamPlayer _player;

    public override void _Ready()
    {
        _player = new AudioStreamPlayer();
        AddChild(_player);

        Connect("button_up", new Callable(this, nameof(OnRelease)));
        Connect("button_down", new Callable(this, nameof(OnPress)));
    }

    private void OnRelease()
    {
        GD.Print("loading game scene");
        GetTree().ChangeSceneToFile("res://scenes/game.tscn");
    }

    private void OnPress()
    {
        _player.Stream = _clickSound;
        _player.Play();
    }
}
