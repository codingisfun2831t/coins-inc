using Godot;
using System;

public partial class CoinsWindow : ClosableWindow
{
	private VBoxContainer _container;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		_container = GetNode<VBoxContainer>("ScrollContainer/Container");

		foreach (var coin in CoinManager.Instance.Coins.Values)
		{
			CoinBuy coinBuy = GD.Load<PackedScene>("res://scenes/coin_buy.tscn").Instantiate<CoinBuy>();
			coinBuy.CoinResource = coin;
			_container.AddChild(coinBuy);
		}

        MinSize = new Vector2I(Size.X, 100);
        MaxSize = new Vector2I(Size.X, 99999);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
