using Godot;
using System;

public partial class Coin : Area2D
{
	private Sprite2D _sprite;
	private CircleShape2D _shape;

	private CoinResource _coin;
	public CoinResource CoinResource {
		get => _coin;
		set
		{
			if (_coin != value)
			{
				_coin = value;
                _sprite.Texture = _coin.Texture;
            }
		}
	}

	[Export]
	public float CoinScale
	{
		get => _sprite.Scale.X;
		set
		{
			_sprite.Scale = new Vector2(value, value);
			_shape.Radius = 16F * value;
		}
	}

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite");
		_shape = (CircleShape2D)GetNode<CollisionShape2D>("Shape").Shape;

        AreaEntered += Coin_AreaEntered;
    }

    private void Coin_AreaEntered(Area2D area)
    {
		if (area is Cursor cur)
		{
			SaveManager.Instance.Coins += _coin.BaseValue;
			QueueFree();
		}
    }
}
