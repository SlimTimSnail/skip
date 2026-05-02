using Godot;
using System;

[GlobalClass]
public partial class FlowManager : Node
{
    [Export]
    private PackedScene _mainMenuScene;
    [Export]
    private PackedScene _gameScene;

    private Node _parent;
    private Node _currentScene;

    public static FlowManager Instance { get; private set; }

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
    public void ChangeStateMenu()
    {
        UnloadScene();
        LoadScene(_mainMenuScene);
    }

    public void ChangeStateGame()
    {
        UnloadScene();
        LoadScene(_gameScene);
    }
}
