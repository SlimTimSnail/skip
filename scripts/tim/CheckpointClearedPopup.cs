using Godot;
using System;

public partial class CheckpointClearedPopup : Control
{
    [Export]
	private Player _player = null;

    [Export]
    private AudioStreamPlayer _audio = null;

    [Export]
    private PanelContainer _panel = null;

    [Export]
    private float _fadeInDuration = 0.1f;

    [Export]
    private float _fadeOutDuration = 3f;

    public override void _Ready()
    {
        _player.HitCheckpoint += FadeIn;
        _panel.Modulate = new Color(1, 1, 1, 0);
    }

    public async void FadeIn()
    {
        _audio.Play();
        Tween tween = CreateTween();
        tween.TweenProperty(_panel, "modulate:a", 1f, _fadeInDuration);
        tween.Play();
        await ToSignal(tween, Tween.SignalName.Finished);
        GD.Print("hello");
        FadeOut();
    }

    private void FadeOut()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(_panel, "modulate:a", 0f, _fadeOutDuration);
        tween.Play();
    }
}
