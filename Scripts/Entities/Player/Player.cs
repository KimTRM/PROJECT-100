using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 105.0f;
    [Export] public float JumpVelocity = -500.0f;
    [Export] public float GravityScale = 1.0f; // Scale for adjusting gravity for smoother jumps
    [Export] public float JumpSmoothness = 0.5f; // Smoothness for jumps using lerp
    [Export] public int MaxJumps = 2;
    [Export] public float Friction = 5.0f; // Increase for smoother friction effect

    [Node]
    private AnimationPlayer animationPlayer;

	[Node]
	private Sprite2D sprite2D;

	private float gravity;
    private int jumpCount = 0;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity") * GravityScale;
    }

    public override void _PhysicsProcess(double delta)
    {
        // Add the gravity smoothly
        if (!IsOnFloor())
        {
            Velocity = new Vector2(Velocity.X, Velocity.Y + (float)(gravity * delta));
        }

        // Reset jump count when on the floor
        if (IsOnFloor())
        {
            jumpCount = 0;
        }

        // Handle jumps and double air jump
        if (Input.IsActionJustPressed("Up") && jumpCount < MaxJumps)
        {
            Velocity = new Vector2(Velocity.X, Mathf.Lerp(Velocity.Y, JumpVelocity, JumpSmoothness));
            jumpCount++;
        }

        // Get the input direction: -1, 0, 1
        float direction = Input.GetAxis("Left", "Right");

        // Flip the Sprite
        if (direction > 0)
        {
            sprite2D.FlipH = false;
        }
        else if (direction < 0)
        {
			sprite2D.FlipH = true;
        }

        // Play animations
        if (IsOnFloor())
        {
            if (Mathf.IsZeroApprox(direction))
            {
                // animatedSprite.Play("idle");
            }
            else
            {
                // animatedSprite.Play("run");
            }
        }
        else
        {
            // animatedSprite.Play("jump");
        }

        // Apply movement
        if (!Mathf.IsZeroApprox(direction))
        {
            Velocity = new Vector2(direction * Speed, Velocity.Y);
        }
        else
        {
            // Apply friction to simulate sliding when stopping
            Velocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, Friction * Speed), Velocity.Y);
        }

        MoveAndSlide();
    }
}
