using Godot;
using System;
using System.Threading.Tasks;

public partial class SavingIndicator : HBoxContainer
{
	[Export]
	public double SavingDelay { get; set; } = 60;

	private double SavingTimer = 0;
    
	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        SavingTimer += delta;

		if (SavingTimer >= SavingDelay)
		{
			TriggerSave();
            SavingTimer = 0;
		}
    }

	public async Task TriggerSave()
	{
        Visible = true;

        await Task.Run(() => {
            SaveManager.Instance.SaveGame();
        });
        await Task.Delay(500);

        Visible = false;
    }
}
