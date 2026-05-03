using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private const float LOSE_RANGE_MIN = 120f;
	private const float LOSE_RANGE_MAX = 230f;

	private const float FAST_RANGE_MIN = 90f;
	private const float FAST_RANGE_MAX = 230f;

	public event Action PlayerLose = null;
	public event Action PlayerWin = null;
	public event Action HitCheckpoint = null;

	[Export]
	private float _speed = 500f;

	[Export]
	private float _jumpVelocity = -600f;

	[Export]
	private float _gravity = 1000f;

	[Export]
	private float _startingRopeSpeed = 1f;

	[Export]
	private float _ropeSpeedIncrease = 3f;

	[Export]
	private float _fastStartingRopeSpeed = 6f;

	[Export]
	private float _fastRopeSpeedIncrease = 3f;

	[Export]
	private Node2D _ropeFront = null;

	[Export]
	private Node2D _ropeBack = null;

	[Export]
	private AudioStreamPlayer2D _jumpSound = null;

	[Export]
	private AudioStreamPlayer2D _landSound = null;

	[Export]
	private AudioStreamPlayer2D _ropeSound = null;

	[ExportGroup("Invincibility")]
	[Export]
	private Sprite2D _body = null;

	[Export]
	private Texture2D _invincibleTexture = null;

	private float _ropeSpeed = 1f;

	private float _fastRopeSpeed = 1f;

	private float _rotationDegrees = 0;

	private bool _hasLost = false;

	private bool _onFloorLastFrame = false;

	private bool _invincible = false;

	private Texture2D _originalTexture = null;

	public override void _Ready()
	{
		_ropeSpeed = _startingRopeSpeed;
		_fastRopeSpeed = _fastStartingRopeSpeed;
		_rotationDegrees = _ropeFront.RotationDegrees;

		_originalTexture = _body.Texture;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey key && key.Pressed)
		{
			if (key.Keycode == Key.I)
			{
				if (OS.HasFeature("editor"))
				{
					_invincible = !_invincible;

					_body.Texture = _invincible ? _invincibleTexture : _originalTexture;
				}
			}
		}
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
			_jumpSound.Play();
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

		bool inLoseRange = InLoseRange(_rotationDegrees);

		if (inLoseRange && IsOnFloor() && !_invincible)
		{
			PlayerLose?.Invoke();
			_hasLost = true;
			return;
		}

		bool inFastRange = InFastRange(_rotationDegrees);

		float oldDegrees = _rotationDegrees;
		_rotationDegrees += inFastRange ? _fastRopeSpeed : _ropeSpeed;

		_ropeFront.RotationDegrees = _rotationDegrees;
		_ropeBack.RotationDegrees = _rotationDegrees;

		if (InLoseRange(oldDegrees) && !InLoseRange(_rotationDegrees))
		{
			SucceedSkip();
		}

		if (!InFastRange(oldDegrees) && InFastRange(_rotationDegrees))
		{
			HitFastRange();
		}

		if (!_onFloorLastFrame && IsOnFloor())
		{
			Land();
		}

		_onFloorLastFrame = IsOnFloor();
	}

	private bool InFastRange(float rotation)
	{
		float clampedDegrees = rotation % 360;

		return clampedDegrees >= FAST_RANGE_MIN && clampedDegrees <= FAST_RANGE_MAX && !IsOnFloor();
	}

	private bool InLoseRange(float rotation)
	{
		float clampedDegrees = rotation % 360;

		return clampedDegrees >= LOSE_RANGE_MIN && clampedDegrees <= LOSE_RANGE_MAX;
	}

	private void SucceedSkip()
	{
		_ropeSpeed += _ropeSpeedIncrease;
		_fastRopeSpeed += _fastRopeSpeedIncrease;
	}

	private void HitFastRange()
	{
		if (!IsOnFloor())
		{
			_ropeSound.Play();
		}
	}

	private void Land()
	{
		_landSound.Play();
	}

	public void CheckpointOverlapped(bool isFinal)
	{
		if (isFinal == true)
		{
			PlayerWin?.Invoke();
		}
		else
		{
			HitCheckpoint?.Invoke();
			_ropeSpeed = _startingRopeSpeed;
			_fastRopeSpeed = _fastStartingRopeSpeed;
		}
	}
}
