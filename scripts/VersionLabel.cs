using Godot;
using System;

public partial class VersionLabel : Label
{
	[Export] public bool ShowEngineVersion { get; set; } = false;

	public override void _Ready()
	{
		string ver = (string) ProjectSettings.GetSetting("application/config/version");
		Text = $"Version {ver}";

		if (ShowEngineVersion)
		{
			string eng = (string)Engine.GetVersionInfo()["string"];
			Text += $", using Godot Engine {eng}";
		}
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
