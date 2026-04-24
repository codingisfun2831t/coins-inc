using Godot;
using System;

public partial class ClosableWindow : Window
{
	public override void _Ready()
	{
        CloseRequested += Close;
	}

    private void Close()
    {
        Visible = false;
    }
}
