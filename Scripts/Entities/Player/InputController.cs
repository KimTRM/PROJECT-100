using System;
using Godot;
using Godot.Collections;

public partial class InputController : Node
{
    public bool IsInteracting { get; private set; } = false;

    [Export]
    private Dictionary<string, string> inputActions = new()
    {
        {"Left" , "Left"},
        {"Right" , "Right"},
        {"Up" , "Up"},
        {"Down" , "Down"},
    };

    public Vector2 GetMovementInput()
    {
        if (IsInteracting) return Vector2.Zero; // Disable movement when interacting
        return new Vector2(Input.GetAxis(inputActions["Left"], inputActions["Right"]),
                            Input.GetAxis(inputActions["Up"], inputActions["Down"])).Normalized();
    }

    public bool InteractPressed()
    {
        return Input.IsActionJustPressed("interact");
    }

    public void SetInteracting(bool state)
    {
        IsInteracting = state;
    }
}
