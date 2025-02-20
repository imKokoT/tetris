using Godot;

public partial class UpdateLvl : Label
{
    public override void _Process(double delta)
    {
        Text = $"level: {GameData.Instance.Level + 1}";
    }
}
