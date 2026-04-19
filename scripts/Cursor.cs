using Godot;
using System;

public partial class Cursor : Area2D
{
    private CircleShape2D _shape = null!;

    public override void _Ready()
    {
        var collision = GetNode<CollisionShape2D>("Shape");
        _shape = collision.Shape as CircleShape2D;
    }

    public override void _Process(double delta)
    {
        Position = GetGlobalMousePosition();
        QueueRedraw(); // IMPORTANT
    }

    public override void _Draw()
    {
        if (_shape != null)
        {
            DrawCircle(Vector2.Zero, _shape.Radius, new Color(1, 1, 1, 0.25f));
        }
    }
}