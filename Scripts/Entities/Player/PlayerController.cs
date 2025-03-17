using Godot;

public partial class PlayerController : Node
{
    [Export] public float WalkSpeed { get; set; } = 150.0f;
    [Export] public float RunSpeed { get; set; } = 250.0f;
    [Export(PropertyHint.Range, "0,1")] public float Acceleration { get; set; } = 0.1f;
    [Export(PropertyHint.Range, "0,1")] public float Deceleration { get; set; } = 0.1f;

    [Export] public float JumpForce { get; set; } = -400.0f;
    [Export(PropertyHint.Range, "0,1")] public float JumpCutMultiplier { get; set; } = 0.5f; // Reduces jump height if released early

    [Export] public float FallMultiplier { get; set; } = 1.5f; // Increases fall speed for snappier jumps
    [Export] public float MaxFallSpeed { get; set; } = 500.0f; // Caps fall speed

    private CharacterBody2D player;
    private float gravity;

    public void Initialize(CharacterBody2D owner)
    {
        player = owner;
        gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
    }

    public void HandleMovement(double delta, Vector2 input, bool jumpPressed, bool jumpReleased)
    {
        float speed = Input.IsActionPressed("Run") ? RunSpeed : WalkSpeed;

        // Apply gravity & fall smoothing
        if (!player.IsOnFloor())
        {
            float gravityMultiplier = player.Velocity.Y > 0 ? FallMultiplier : 1.0f; // Faster fall, normal jump rise
            player.Velocity = new Vector2(
                player.Velocity.X, 
                Mathf.Min(player.Velocity.Y + gravity * (float)delta * gravityMultiplier, MaxFallSpeed)
            );
        }

        // Horizontal movement
        if (input.X != 0)
        {
            player.Velocity = new Vector2(
                Mathf.MoveToward(player.Velocity.X, input.X * speed, speed * Acceleration), 
                player.Velocity.Y
            );
        }
        else
        {
            player.Velocity = new Vector2(
                Mathf.MoveToward(player.Velocity.X, 0, WalkSpeed * Deceleration), 
                player.Velocity.Y
            );
        }

        // Handle Jump
        if (jumpPressed && (player.IsOnFloor() || player.IsOnWall()))
        {
            player.Velocity = new Vector2(player.Velocity.X, JumpForce);
        }

        // Cut jump if released early
        if (jumpReleased && player.Velocity.Y < 0)
        {
            player.Velocity = new Vector2(player.Velocity.X, player.Velocity.Y * JumpCutMultiplier);
        }

        player.MoveAndSlide();
    }

    public void ForceMove(Vector2 direction)
    {
        player.Velocity = new Vector2(Mathf.MoveToward(player.Velocity.X, direction.X * WalkSpeed, WalkSpeed * Acceleration), player.Velocity.Y);
    }

    public void ForceJump()
    {
        player.Velocity = new Vector2(player.Velocity.X, JumpForce);
    }
}
