using Godot;
using System;

public partial class MainMenu : Node
{
    [Export]
    private Button _playButton;

    [Export]
    private Button _howToPlayButton; 

    [Export]
    private Button _quitButton; 

    [Export]
    private Button _returnButton; 

    [Export]
    private Control _howToPlayParent;

    [Export]
    private Control _menuParent;


    public override void _Ready()
    {
        _playButton.Pressed += OnPlayButtonPressed;
        _quitButton.Pressed += OnQuitButtonPressed;
        _howToPlayButton.Pressed += OnHTPButtonPressed;
        _returnButton.Pressed += OnReturnButtonPressed;
        _howToPlayParent.Visible = false;
    }

    private void OnReturnButtonPressed()
    {
        _howToPlayParent.Visible = false;
        _menuParent.Visible = true;
        _howToPlayButton.GrabFocus();
    }


    private void OnHTPButtonPressed()
    {
        _menuParent.Visible = false;
        _howToPlayParent.Visible = true;
        _returnButton.GrabFocus();
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
