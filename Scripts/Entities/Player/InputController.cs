using Godot;

public partial class InputController : Node
{
    public bool IsInteracting { get; private set; } = false;

    public Vector2 GetMovementInput()
    {
        if (IsInteracting) return Vector2.Zero; // Disable movement when interacting
        return new Vector2(Input.GetAxis("Left", "Right"),
                            Input.GetAxis("Up", "Down")).Normalized();
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
