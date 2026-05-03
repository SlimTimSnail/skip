using Godot;
using System;

public partial class SquareChar : CharacterBody2D
{
	public event Action PlayerLose = null;
	public event Action PlayerWin = null;

	[Export]
	private float _speed = 500f;

	[Export]
	private float _jumpVelocity = -600f;

	[Export]
	private float _gravity = 1000f;

	[Export]
	private float _ropeSpeed = 3f;

	[Export]
	private float _fastRopeSpeed = 6f;

	[Export]
	private Node2D _ropeFront = null;

	[Export]
	private Node2D _ropeBack = null;

	private float _rotationDegrees = 0;

	private bool _hasLost = false;

	public override void _Ready()
	{
		_rotationDegrees = _ropeFront.RotationDegrees;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_hasLost)
		{
			return;
		}

		Vector2 vel = Velocity;

		if (!IsOnFloor())
		{
			vel.Y += _gravity * (float)delta;
		}

		if (Input.IsActionJustPressed("action") && IsOnFloor())
		{
			vel.Y = _jumpVelocity;
		}
		else if (Input.IsActionJustReleased("action") && vel.Y < 0)
		{
			vel.Y /= 2;
		}

		vel.X = 0;

		if (IsOnFloor())
		{
			if (Input.IsActionPressed("move_left"))
			{
				vel.X = -_speed;
			}
			
			if (Input.IsActionPressed("move_right"))
			{
				vel.X = _speed;
			}
		}

		Velocity = vel;

		MoveAndSlide();

		float clampedDegrees = _rotationDegrees % 360f;
		bool inLoseZone = clampedDegrees >= 120f && clampedDegrees <= 240f;

		if (inLoseZone && IsOnFloor())
		{
			PlayerLose?.Invoke();
			_hasLost = true;
			return;
		}

		bool inFastZone = clampedDegrees >= 100f && clampedDegrees <= 240f;

		_rotationDegrees += inFastZone ? _fastRopeSpeed : _ropeSpeed;

		_ropeFront.RotationDegrees = _rotationDegrees;
		_ropeBack.RotationDegrees = _rotationDegrees;
	}

	public void CheckpointOverlapped(bool isFinal)
	{
		if (isFinal == true)
		{
			PlayerWin?.Invoke();
		}
		else
		{
			//sami do the rope speed reset
		}
	}
}
