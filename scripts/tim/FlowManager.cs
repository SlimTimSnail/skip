using Godot;
using System;

[GlobalClass]
public partial class FlowManager : Node
{
    public enum GameState
    {
        MainMenu,
        Game,
        End,
    }

    public static FlowManager Instance { get; private set; }

    // Export variables
    [Export]
    private PackedScene _mainMenuScene;
    [Export]
    private PackedScene _gameScene;
    [Export]
    private PackedScene _endScene;
    //

    private Node _parent;
    private Node _currentScene;

    public override async void _Ready()
    {
        Instance = this;
        await ToSignal(GetParent(), Node.SignalName.Ready);
        _parent = GetParent();
        LoadScene(_mainMenuScene);
    }

    private void LoadScene(PackedScene scene)
    {
        _currentScene = scene.Instantiate();
        _parent.AddChild(_currentScene);
    }

    private void UnloadScene()
    {
        _parent.RemoveChild(_currentScene);
        _currentScene.QueueFree();
    }


    //Public functions
    public void ChangeState(GameState state)
    {
        UnloadScene();

        switch(state)
        {
            case GameState.MainMenu:
            LoadScene(_mainMenuScene);
            break;

            case GameState.Game:
            LoadScene(_gameScene);
            break;

            case GameState.End:
            LoadScene(_endScene);
            break;

            default:
            break;
        }
    }
}
