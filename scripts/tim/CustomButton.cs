using Godot;
using System;

public partial class CustomButton : Button
{
    [Export]
    private bool _isInitialFocus = false;

    public override void _Ready()
    {
        MouseEntered += OnMouseEntered;

        if (_isInitialFocus == true)
        {
            GrabFocus();
        }
    }

    private void OnMouseEntered()
    {
        GrabFocus();
    }

}
