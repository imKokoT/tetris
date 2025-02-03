using Godot;
using System;

public partial class UpdateScore : MeshInstance2D
{
    TextMesh _text;

    public override void _Ready()
    {
        _text = Mesh as TextMesh;
    }

    public override void _Process(double delta)
    {
        string score = GameData.Instance.Score.ToString();
        _text.Text = score.PadLeft(6, '0');
    }
}
