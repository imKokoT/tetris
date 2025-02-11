using Godot;
using System;

public partial class UpdateScore : Label
{
    public override void _Process(double delta)
    {
        string score = GameData.Instance.Score.ToString();
        Text = score.PadLeft(6, '0');
    }
}
