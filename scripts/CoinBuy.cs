using Godot;
using System;

public partial class CoinBuy : Panel
{
	private CoinResource _coinResource;

	private TextureRect _iconRect;
	private Label _nameLabel;
	private Label _flavorTextLabel;
	private Label _priceLabel;
	private Button _buyButton;

	[Export]
	public CoinResource CoinResource
	{
		get => _coinResource;
		set
		{
			_coinResource = value;
			updateUI();
        }
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// get controls
		HBoxContainer stack = GetNode<HBoxContainer>("Stack");
        _iconRect = stack.GetNode<TextureRect>("Icon");

		VBoxContainer textContainer = stack.GetNode<VBoxContainer>("Text");
        _nameLabel = textContainer.GetNode<Label>("Name");
		_flavorTextLabel = textContainer.GetNode<Label>("Flavor");

		VBoxContainer right = stack.GetNode<VBoxContainer>("Right");
        _priceLabel = right.GetNode<Label>("Price");
		_buyButton = right.GetNode<Button>("Buy");

		_buyButton.Pressed += OnBuyPressed;

        if (CoinResource != null) updateUI();
    }

	private void updateBuyButton()
	{
		if (CoinResource == null) return;
		if (_buyButton == null) return;

        if (SaveManager.Instance.BoughtCoins.Contains(CoinResource.ID))
		{
			_buyButton.Disabled = true;
			_buyButton.Text = "Bought";
			return;
        }

        _buyButton.Text = "Buy";
		_buyButton.Disabled = SaveManager.Instance.Coins < CoinResource.Price;
    }

	private void updateUI()
	{
        if (_iconRect != null) _iconRect.Texture = _coinResource.Texture;
        if (_nameLabel != null) _nameLabel.Text = _coinResource.Name;
        if (_flavorTextLabel != null) _flavorTextLabel.Text = _coinResource.FlavorText;
        if (_priceLabel != null) _priceLabel.Text = _coinResource.Price.ToString() + " coins";

        updateBuyButton();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        // we could use a event in SaveManager but the time it takes to update
        // the button is negligible, so we can just check every frame
        updateBuyButton();
    }

	private void OnBuyPressed()
	{
		if (SaveManager.Instance.BoughtCoins.Contains(CoinResource.ID)) return;
		if (SaveManager.Instance.Coins < CoinResource.Price) return;

		SaveManager.Instance.Coins -= CoinResource.Price;
		SaveManager.Instance.BoughtCoins.Add(CoinResource.ID);
    }
}
