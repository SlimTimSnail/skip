using Godot;
using System;

[GlobalClass]
public partial class FlowManager : Node
{
    public enum GameState
    {
        MainMenu,
        Game,
        LoseMenu,
        WinMenu,
    }

    public static FlowManager Instance { get; private set; }

    // Export variables
    [Export]
    private PackedScene _mainMenuScene;
    [Export]
    private PackedScene _gameScene;
    [Export]
    private PackedScene _loseMenuScene;
    [Export]
    private PackedScene _winMenuScene;
    //

    private Node _parent;
    private Node _currentScene;

    public override async void _Ready()
    {
        Instance = this;
        await ToSignal(GetParent(), Node.SignalName.Ready);
        _parent = GetParent();
        ChangeState(GameState.MainMenu);
    }

    private void LoadScene(PackedScene scene)
    {
        _currentScene = scene.Instantiate();
        _parent.AddChild(_currentScene);
    }

    private void UnloadScene()
    {
        if (GodotObject.IsInstanceValid(_currentScene))
        {
            _parent.RemoveChild(_currentScene);
            _currentScene.QueueFree();
        }
    }


    //Public functions
    public void ChangeState(GameState state)
    {
        UnloadScene();

        switch(state)
        {
            case GameState.MainMenu:
            LoadScene(_mainMenuScene);
            AudioManager.Instance.SwitchMusicTrack(AudioManager.MusicSelect.Menu);
            break;

            case GameState.Game:
            LoadScene(_gameScene);
            AudioManager.Instance.SwitchMusicTrack(AudioManager.MusicSelect.Game);
            break;

            case GameState.LoseMenu:
            LoadScene(_loseMenuScene);
            AudioManager.Instance.SwitchMusicTrack(AudioManager.MusicSelect.Menu);
            break;

            case GameState.WinMenu:
            LoadScene(_winMenuScene);
            AudioManager.Instance.SwitchMusicTrack(AudioManager.MusicSelect.Game);
            break;

            default:
            break;
        }
    }
}
