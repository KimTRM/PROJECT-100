using DialogueManagerRuntime;
using Godot;
using GodotUtilities;

[Scene, Icon("res://Assets/Icons/Player.png")]
public partial class Player : CharacterBody2D
{
	[Node] private AnimationPlayer animationPlayer;
	[Node] private Sprite2D sprite2D;
	[Node] private PlayerController playerController;
	[Node] private InputController inputController;

	[Export] private Area2D InteractableFinder;

	private bool cutsceneActive = false;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // Auto-wires child nodes
		}
	}

	public override void _Ready()
	{
		playerController.Initialize(this);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (cutsceneActive)
		{
			MoveAndSlide();
			return;
		}

		if (inputController.InteractPressed())
		{
			var interactables = InteractableFinder.GetOverlappingAreas();
			if (interactables.Count > 0)
			{
				(interactables[0] as Interactable)?.Interact();
				inputController.SetInteracting(true); // Disable movement
				return;
			}
		}

		if(GetParent().GetNodeOrNull<ExampleBalloon>("ExampleBalloon") != null)
		{
			inputController.SetInteracting(true);
		}
		else
		{
			inputController.SetInteracting(false);
		}

		// Resume movement if not interacting
		if (!inputController.IsInteracting)
		{
			playerController.HandleMovement(delta, inputController.GetMovementInput(), inputController.JumpPressed(), inputController.JumpReleased());
		}

		UpdateAnimation();
	}

	public void StartCutscene()
	{
		cutsceneActive = true;
		Velocity = Vector2.Zero;
	}

	public void EndCutscene()
	{
		cutsceneActive = false;
	}

	public void ForceMove(Vector2 direction)
	{
		playerController.ForceMove(direction);
		MoveAndSlide();
	}

	public void ForceJump()
	{
		playerController.ForceJump();
		MoveAndSlide();
	}

	public void UpdateAnimation()
	{
		if (Velocity.X != 0)
		{
			sprite2D.FlipH = Velocity.X < 0;
		}

		if (IsOnFloor())
		{
			animationPlayer?.Play(Velocity.X != 0 ? "walk_side" : "idle_side");
		}
		else if (Velocity.Y < 0)
		{
			// animationPlayer?.Play("jump");
		}
		else
		{
			// animationPlayer?.Play("fall");
		}
	}
}
