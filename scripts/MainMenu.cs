using Godot;

public partial class MainMenu : Control
{
	// buttons
	private Button _playButton;
	private Button _aboutButton;
	private Button _exitButton;

	private Window _aboutWindow;

	public override void _Ready()
	{
		// find containers
		var buttonsContainer = GetNode<VBoxContainer>("MenuContainer/ButtonsContainer");
		var hbox = buttonsContainer.GetNode<HBoxContainer>("HBoxContainer");

		// and buttons
		_playButton = buttonsContainer.GetNode<Button>("PlayButton");
		_aboutButton = hbox.GetNode<Button>("AboutButton");
		_exitButton = hbox.GetNode<Button>("ExitButton");

		// Hooking up the events
		_playButton.Pressed += OnPlayPressed;
		_aboutButton.Pressed += OnAboutPressed;
		_exitButton.Pressed += OnExitPressed;

		// find window
		_aboutWindow = GetNode<Window>("AboutWindow");
    }

	// button listeners

	private void OnPlayPressed()
	{
		// load and play game
		SaveManager.Instance.LoadGame();
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
	}

	private void OnAboutPressed()
	{
		_aboutWindow.PopupCentered();
    }

	private void OnExitPressed() => GetTree().Quit();
}
