using Godot;
using System;

public partial class MainMenu : Node
{
    [Export]
    private Button _playButton;

    [Export]
    private Button _quitButton; 

    public override void _Ready()
    {
        _playButton.Pressed += OnPlayButtonPressed;
        _quitButton.Pressed += OnQuitButtonPressed;
    }

    private void OnPlayButtonPressed()
    {
        FlowManager.Instance.ChangeState(FlowManager.GameState.Game);
    }

    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
