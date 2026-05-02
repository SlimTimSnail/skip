using Godot;

public partial class SquareChar : CharacterBody2D
{
	[Export]
	private float _speed = 500f;

	[Export]
	private float _jumpVelocity = -600f;

	[Export]
	private float _gravity = 1000f;

	[Export]
	private Node2D _ropeFront = null;

	[Export]
	private Node2D _ropeBack = null;

	private float _rotationDegrees = 0;

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

		_rotationDegrees += 1;

		_ropeFront.RotationDegrees = _rotationDegrees;
		_ropeBack.RotationDegrees = _rotationDegrees;
	}
}
