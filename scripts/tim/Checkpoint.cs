using Godot;
using System;

[GlobalClass]
public partial class Checkpoint : Area2D
{
    [Export]
    private bool _isFinalCheckpoint = false;

    public override void _Ready()
    {
        BodyEntered += OnOverlap;
    }

    private void OnOverlap(Node2D body)
    {
        if (body is Player player)
        {
            player.CheckpointOverlapped(_isFinalCheckpoint);
        }
    }

}
