using Godot;
using System;

[GlobalClass, Icon("res://textures/coins/coin.png")]
public partial class CoinResource : Resource
{
    [ExportCategory("Text")]
    [Export] public string ID { get; set; }
    [Export] public string Name { get; set; }
    [Export] public string FlavorText { get; set; }

    [ExportCategory("Misc")]
    [Export] public int Price { get; set; }
    [Export] public int BaseValue { get; set; }
    [Export] public Texture2D Texture { get; set; }
}
