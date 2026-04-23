using Godot;
using System;

public partial class CoinSpawner : ReferenceRect
{
    private double _time = 0;
    private PackedScene _coinScene;

    [Export]
    public double SpawnDelay { get; set; } = 0.1D;

    [Export]
    public int SpawnMax { get; set; } = 25;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _coinScene = GD.Load<PackedScene>("res://Scenes/Coin.tscn");
        if (_coinScene == null)
        {
            GD.PrintErr("Failed to load CoinScene! Check your file path.");
        }
    }

	public override void _Process(double delta)
	{
        if (GetChildCount() >= SpawnMax)
        {
            _time = 0; // reset timer to avoid instant spawn after a coin is collected
            return;
        }

        _time += delta;

        if (_time >= SpawnDelay)
        {
            int amt = (int)Math.Floor(_time / SpawnDelay);
            for (int i = 0; i < amt; i++)
            {
                SpawnInArea();
            }

            _time = 0;
        }
    }

    public void SpawnInArea()
    {
        float randomX = (float)GD.RandRange(0, Size.X);
        float randomY = (float)GD.RandRange(0, Size.Y);
        Vector2 spawnPos = new Vector2(randomX, randomY);

        var randomCoin = SaveManager.Instance.RandomBoughtCoin();
        Coin coin = _coinScene.Instantiate<Coin>();
        coin.Position = spawnPos;
        AddChild(coin);


        Tween tween;
        tween = CreateTween();
        tween.SetEase(Tween.EaseType.InOut);
        tween.SetTrans(Tween.TransitionType.Cubic);
        coin.CoinResource = randomCoin;
        coin.CoinScale = 0;
        tween.TweenProperty(coin, "CoinScale", 2, 0.05F);

    }
}
