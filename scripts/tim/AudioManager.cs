using Godot;
using System;

public partial class AudioManager : Node

{
    public enum MusicSelect
    {
        Menu,
        Game,
    }


    public static AudioManager Instance { get; private set; }


    [Export]
    private AudioStreamPlayer _musicMenu;

    [Export]
    private AudioStreamPlayer _musicGame;

    [Export]
    private SoundEffectPlayer _sfx;

    private AudioStreamPlayer _currentMusic;


    public override void _Ready()
    {
        Instance = this;
    }


    private void PlayMusic(AudioStreamPlayer music)
    {
        if (!GodotObject.IsInstanceValid(_currentMusic))
        {
            music.Play();
            _currentMusic = music;
            return;
        }
        
        if (GodotObject.IsInstanceValid(_currentMusic) && _currentMusic != music)
        {
            _currentMusic.Stop();
            music.Play();
            _currentMusic = music;
            return;
        }
    }

    public void SwitchMusicTrack(MusicSelect state)
    {       
        switch(state)
        {
            case MusicSelect.Menu:
            PlayMusic(_musicMenu);
            break;

            case MusicSelect.Game:
            PlayMusic(_musicGame);
            break;

            default:
            break;
        }
    }

    public void PlaySfx(AudioStream sound)
    {
        if (_sfx.HasStreamPlayback())
        {
            _sfx.Stop();
        }
        _sfx.Stream = sound;
        _sfx.PlaySoundEffect();
    }
}
