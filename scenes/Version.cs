using Godot;
using System;

public partial class Version : Label
{
    public override void _Ready()
    {
        Text = ProjectSettings.GetSetting("application/config/version").ToString();
    }
}
