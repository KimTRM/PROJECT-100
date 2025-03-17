using Godot;

public partial class InputController : Node
{
    public bool IsInteracting { get; private set; } = false;

    public Vector2 GetMovementInput()
    {
        if (IsInteracting) return Vector2.Zero; // Disable movement when interacting
        return new Vector2(Input.GetAxis("Left", "Right"), 0);
    }

    public bool JumpPressed()
    {
        return !IsInteracting && Input.IsActionJustPressed("Jump");
    }

    public bool JumpReleased()
    {
        return !IsInteracting && Input.IsActionJustReleased("Jump");
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
