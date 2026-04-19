using Godot;
using System;

public partial class AboutWindow : Window
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
