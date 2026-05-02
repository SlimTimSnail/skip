using Godot;
using System;

public partial class EndMenu : Node
{
    [Export]
    private Button _replayButton;

    [Export]
    private Button _leaveButton; 

    public override void _Ready()
    {
        _replayButton.Pressed += OnReplayButtonPressed;
        _leaveButton.Pressed += OnLeaveButtonPressed;
    }

    private void OnReplayButtonPressed()
    {
        FlowManager.Instance.ChangeState(FlowManager.GameState.Game);
    }

    private void OnLeaveButtonPressed()
    {
        FlowManager.Instance.ChangeState(FlowManager.GameState.MainMenu);
    }
}
