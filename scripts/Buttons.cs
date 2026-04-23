using Godot;
using System;

public partial class Buttons : HBoxContainer
{
	private Button _exit;
    private Button _save;

    [Export]
    public SavingIndicator SavingIndicator { get; set; }

    public override void _Ready()
	{
		_exit = GetNode<Button>("Exit");
		_exit.Pressed += OnExitPressed;
        _save = GetNode<Button>("ManualSave");
        _save.Pressed += OnSavePressed;

    }

    private void OnExitPressed()
    {
        SaveManager.Instance.SaveGame();
        GetTree().ChangeSceneToFile("res://scenes/mainmenu.tscn");
    }

    private async void OnSavePressed()
    {
        await SavingIndicator.TriggerSave();
    }
}
