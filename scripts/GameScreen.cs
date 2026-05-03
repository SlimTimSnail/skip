using Godot;

public partial class GameScreen : Node
{
	[Export]
	private SquareChar player = null;

	public override void _Ready()
	{
		player.PlayerLose += GoToEnd;
	}

	public void GoToEnd()
	{
		FlowManager.Instance.ChangeState(FlowManager.GameState.LoseMenu);
	}
}
