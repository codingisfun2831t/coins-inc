using Godot;
using System;

public partial class Buttons : HBoxContainer
{
	private Button _exit;

	public override void _Ready()
	{
		_exit = GetNode<Button>("Exit");

		_exit.Pressed += OnExitPressed;

    }

    private void OnExitPressed()
    {
        SaveManager.Instance.SaveGame();
        GetTree().ChangeSceneToFile("res://scenes/mainmenu.tscn");
    }
}
