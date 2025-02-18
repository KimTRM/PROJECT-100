using Godot;
using Godot.Collections;
using GodotUtilities;

[Scene, Icon("res://Assets/Icons/Player.png")]
public partial class Player : CharacterBody2D
{
	[Export] public float WalkSpeed { get; set; } = 150.0f;
	[Export] public float RunSpeed { get; set; } = 250.0f;
	[Export(PropertyHint.Range, "0,1")] public float Acceleration { get; set; } = 0.1f;
	[Export(PropertyHint.Range, "0,1")] public float Deceleration { get; set; } = 0.1f;

	[Export] public float JumpForce { get; set; } = -400.0f;
	[Export(PropertyHint.Range, "0,1")] public float DecelerateOnJumpRelease { get; set; } = 0.5f;

	[Export] public float DashSpeed { get; set; } = 1000.0f;
	[Export] public float DashMaxDistance { get; set; } = 300.0f;
	[Export] public Curve DashCurve { get; set; }
	[Export] public float DashCooldown { get; set; } = 1.0f;

	private double gravity;
	private bool isDashing = false;
	private float dashStartPosition = 0;
	private int dashDirection = 0;
	private float dashTimer = 0;

	[Node]
	public AnimationPlayer animationPlayer;

	[Node]
	private Sprite2D sprite2D;

	[Export]
	private Dictionary<string, State> states;

	private State currentState;

	[Export(PropertyHint.Range, "50,500")] public float MaxSpeed = 200.0f;
	[Export(PropertyHint.Range, "0,100")] public float GravityScale = 20.0f;
	[Export(PropertyHint.Range, "0,20")] public float JumpHeight = 2.0f;

	public float JumpMagnitude => Mathf.Sqrt(2 * JumpHeight * GravityScale * 9.8f);

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");

		states = new Dictionary<string, State>
			{
				// { "IdleState", new IdleState(this) },
				// { "WalkingState", new WalkingState(this) },
				// { "JumpingState", new JumpingState(this) },
				// { "FallingState", new FallingState(this) },
			};

		// currentState = states["IdleState"];
		// currentState.Enter();
	}

	// public override void _Input(InputEvent @event)
	// {
	// 	currentState.HandleInput(@event);
	// }
	//
	// public override void _Process(double delta)
	// {
	// 	sprite2D.FlipH = Mathf.Abs(Input.GetAxis("Left", "Right") + 1) < 0.01f;
	// 	currentState.Update(delta);
	// }

	// public override void _PhysicsProcess(double delta)
	// {
	// 	currentState.PhysicsUpdate(delta);
	// }

	public void Transition(string StateName)
	{
		if (states.ContainsKey(StateName) && currentState != states[StateName])
		{
			currentState.Exit();
			currentState = states[StateName];
			currentState.Enter();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!IsOnFloor())
		{
			Velocity = new Vector2(Velocity.X, (float)(Velocity.Y + gravity * delta));
		}

		float speed = Input.IsActionPressed("Run") ? RunSpeed : WalkSpeed;

		float direction = Input.GetAxis("Left", "Right");
		if (direction != 0)
		{
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, direction * speed, speed * Acceleration), Velocity.Y);
			sprite2D.FlipH = direction == -1;
			if (IsOnFloor())
			{
				if (Input.IsActionPressed("Down"))
				{
					// AnimatedSprite.Play("RunSword");
				}
				else
				{
					animationPlayer?.Play("walk_side");
				}
			}
		}
		else
		{
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, WalkSpeed * Deceleration), Velocity.Y);
			if (IsOnFloor())
			{
				animationPlayer?.Play("idle_side");
			}
		}

		// Handle jump.
		if (Input.IsActionJustPressed("Jump") && (IsOnFloor() || IsOnWall()))
		{
			Velocity = new Vector2(Velocity.Y, JumpForce);
			// AnimatedSprite.Play("Jump");
		}

		if (Input.IsActionJustReleased("Jump") && Velocity.Y < 0)
		{
			Velocity = new Vector2(Velocity.X, Velocity.Y * DecelerateOnJumpRelease);
		}

		MoveAndSlide();
	}
}
