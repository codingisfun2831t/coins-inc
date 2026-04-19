using Godot;
using System;

public partial class Currencies : VBoxContainer
{
	private Label _coinsText;

	public override void _Ready()
	{
		_coinsText = GetNode<Label>("Coins/Text");
        Update();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) => Update();


    private void Update()
    {
        _coinsText.Text = SaveManager.Instance.Coins.ToString();
    }
}
