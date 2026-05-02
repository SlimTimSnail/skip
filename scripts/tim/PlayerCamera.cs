using Godot;
using System;

[GlobalClass]
public partial class PlayerCamera : Camera2D
{
    [Export]
    private Node2D _playerNode;
}
