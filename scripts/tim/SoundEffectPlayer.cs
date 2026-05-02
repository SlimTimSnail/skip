using Godot;
using System;

[GlobalClass]
public partial class SoundEffectPlayer : AudioStreamPlayer
{
    public void PlaySoundEffect()
    {
        Play();
    }
}
