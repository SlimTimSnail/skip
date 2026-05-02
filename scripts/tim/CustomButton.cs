using Godot;
using System;

public partial class CustomButton : Button
{
    [Export]
    private AudioStreamPlayer _clickSound;
    [Export]
    private AudioStreamPlayer _hoverSound;

    public override void _Ready()
    {
        ButtonDown += OnMouseDown;
        MouseEntered += OnMouseEntered;
    }

    private void OnMouseEntered()
    {
        _hoverSound.Play();
    }


    private void OnMouseDown()
    {
        _clickSound.Play();
    }

}
