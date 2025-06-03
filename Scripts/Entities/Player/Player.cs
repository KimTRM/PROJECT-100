using DialogueManagerRuntime;
using GodotUtilities.Logic;
using GodotUtilities;
using Godot;

[Scene, Icon("res://Assets/Icons/Player.png")]
public partial class Player : CharacterBody2D
{
	[Node]
	private AnimationPlayer animationPlayer;
	[Node]
	private Sprite2D sprite2D;
	[Node]
	private InputController inputController;
	[Node]
	private VelocityComponent velocityComponent;
	[Export]
	[Node("Sprite2D/Direction/InteractableFinder")]
	private Area2D interactableFinder;

	private DelegateStateMachine stateMachine;

	private bool cutsceneActive = false;
	private string animDirection = "down";

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // Auto-wires child nodes
		}
	}

	public override void _Ready()
	{
		PlayerManager.Instance.player = this;
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = velocityComponent.GetVelocity(inputController.GetMovementInput());
		velocityComponent.AccelerateToVelocity(Velocity, (float)delta);
		velocityComponent.Move(this);

		UpdateAnimation();
		InteractWithObject();
	}

	private void InteractWithObject()
	{
		if (inputController.InteractPressed() && inputController.IsInteracting == false)
		{
			var interactables = interactableFinder.GetOverlappingAreas();
			if (interactables.Count > 0)
			{
				(interactables[0] as Interactable)?.Interact();
				inputController.SetInteracting(true); // Disable movement
				return;
			}
		}

		if (GetParent().GetNodeOrNull<ExampleBalloon>("ExampleBalloon") != null)
		{
			inputController.SetInteracting(true);
		}
		else
		{
			inputController.SetInteracting(false);
		}
	}

	private void UpdateAnimation()
	{
		Vector2 movementInput = inputController.GetMovementInput();

		if (movementInput == Vector2.Up)
		{
			animDirection = "up";
		}
		else if (movementInput == Vector2.Down)
		{
			animDirection = "down";
		}
		else if (movementInput == Vector2.Left || movementInput == Vector2.Right)
		{
			animDirection = "side";
		}


		if (movementInput != Vector2.Zero)
		{
			animationPlayer.Play($"walk_{animDirection}");
			sprite2D.FlipH = inputController.GetMovementInput().X < 0;
		}
		else
		{
			animationPlayer.Play($"idle_{animDirection}");
		}
	}
}
