using Godot;
using System;

public partial class HighScoreDisplay : Label
{
    public override void _Ready()
    {
        Text = $"hi-score: {GameData.Instance.HighScore}";
    }
}
