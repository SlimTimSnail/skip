using Godot;

public partial class GameScreen : Node
{
	public void GoToEnd()
	{
		FlowManager.Instance.ChangeState(FlowManager.GameState.LoseMenu);
	}
}
