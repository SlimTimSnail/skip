using Godot;

public partial class GameScreen : Node
{
	[Export]
	private Player player = null;

	public override void _Ready()
	{
		player.PlayerLose += GoToEnd;
		player.PlayerWin += GoToWin;
	}

	public void GoToEnd()
	{
		FlowManager.Instance.ChangeState(FlowManager.GameState.LoseMenu);
	}

	public void GoToWin()
	{
		FlowManager.Instance.ChangeState(FlowManager.GameState.WinMenu);
	}
}
