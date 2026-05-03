using Godot;
using System;
using System.Net;

public partial class TreeSpawner : Node2D
{
	private const int AMOUNT = 30;
	private const int SPACING = 1000;
	private const int OFFSET_RANGE = 100;

	private const int SEED = 17834865;

	[Export]
	private Sprite2D _baseTree = null;

	private int _index = 0;

	private Random rand = null;

	public override void _Ready()
	{
		rand = new Random(SEED);
	}

	public override void _Process(double delta)
	{
		if (_index == AMOUNT)
		{
			SetProcess(false);
		}

		Sprite2D tree = null;

		if (_index == 0)
		{
			tree = _baseTree;
		}
		else
		{
			tree = (Sprite2D)_baseTree.Duplicate((int)DuplicateFlags.Default);
			tree.Translate(new Vector2(SPACING * _index, 0));
			AddChild(tree);
		}

		int offset = rand.Next(-OFFSET_RANGE, OFFSET_RANGE);
		tree.Translate(new Vector2(offset, 0));

		_index++;
	}
}
