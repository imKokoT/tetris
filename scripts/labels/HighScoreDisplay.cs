using Godot;
using System;

public partial class HighScoreDisplay : Label
{
    public override void _Ready()
    {
        Text = $"hi-score: {GameData.Instance.HighScore}\n" +
               $"hi-level: {GameData.Instance.HighLevel}\n" +
               $"hi-tricks: {GameData.Instance.HighTricks}\n" +
               $"hi-opened-gaps: {GameData.Instance.HighOpenedGaps}\n" +
               $"hi-lines-filled: {GameData.Instance.HighLinesFilled}";
    }
}
