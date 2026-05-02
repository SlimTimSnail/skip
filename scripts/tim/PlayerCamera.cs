using Godot;
using System;

[GlobalClass]
public partial class PlayerCamera : Camera2D
{
    [Export]
    private Node2D _playerNode;

    [Export]
    private float _yOffset = 700f;

    public override void _Ready()
    {
        SetStartingPosition();
    }

    public override void _Process(double delta)
    {
        Vector2 currentPosition = new Vector2(_playerNode.GlobalPosition.X, _yOffset);
        SetPosition(currentPosition);
    }

    private void SetStartingPosition()
    {
        Vector2 startPosition = new Vector2(_playerNode.GlobalPosition.X, _yOffset);
        SetPosition(startPosition);
        ResetSmoothing();
    }
}
