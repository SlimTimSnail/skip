using Godot;
using System;

public partial class SquareChar : CharacterBody2D
{
	[Export]
	private float _speed = 500f;

	[Export]
	private float _jumpVelocity = -600f;

	[Export]
	private float _gravity = 1000f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 vel = Velocity;

		if (!IsOnFloor())
		{
			vel.Y += _gravity * (float)delta;
		}

		if (Input.IsActionJustPressed("action") && IsOnFloor())
		{
			vel.Y = _jumpVelocity;
		}

		if (Input.IsActionPressed("move_left"))
		{
			vel.X = -_speed;
		}
		else if (Input.IsActionPressed("move_right"))
		{
			vel.X = _speed;
		}
		else
		{
			vel.X = 0;
		}

		Velocity = vel;

		MoveAndSlide();
	}
}
