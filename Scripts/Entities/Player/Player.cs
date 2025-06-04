using DialogueManagerRuntime;
using GodotUtilities.Logic;
using GodotUtilities;
using Godot;

[Scene, Icon("res://Assets/Icons/Player.png")]
public partial class Player : CharacterBody2D
{
	[Node] private AnimationPlayer _animationPlayer;
	[Node] private Sprite2D _sprite2D;
	[Node] private InputController _inputController;
	[Node] private VelocityComponent _velocityComponent;

	[Export, Node("Sprite2D/Direction/InteractableFinder")]
	private Area2D _interactableFinder;

	private DelegateStateMachine _stateMachine;
	private string _animDirection = "down";

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // Auto-wires child nodes
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = _velocityComponent.GetVelocity(_inputController.GetMovementInput());
		_velocityComponent.AccelerateToVelocity(Velocity, (float)delta);
		_velocityComponent.Move(this);

		UpdateAnimation();
		InteractWithObject();
	}

	private void InteractWithObject()
	{
		if (_inputController.InteractPressed() && _inputController.IsInteracting == false)
		{
			var interactables = _interactableFinder.GetOverlappingAreas();
			if (interactables.Count > 0)
			{
				(interactables[0] as Interactable)?.Interact();
				_inputController.SetInteracting(true); // Disable movement
				return;
			}
		}

		if (GetParent().GetNodeOrNull<ExampleBalloon>("ExampleBalloon") != null)
		{
			_inputController.SetInteracting(true);
		}
		else
		{
			_inputController.SetInteracting(false);
		}
	}

	private void UpdateAnimation()
	{
		Vector2 movementInput = _inputController.GetMovementInput();

		if (movementInput == Vector2.Up)
		{
			_animDirection = "up";
		}
		else if (movementInput == Vector2.Down)
		{
			_animDirection = "down";
		}
		else if (movementInput == Vector2.Left || movementInput == Vector2.Right)
		{
			_animDirection = "side";
		}


		if (movementInput != Vector2.Zero)
		{
			_animationPlayer.Play($"walk_{_animDirection}");
			_sprite2D.FlipH = _inputController.GetMovementInput().X < 0;
		}
		else
		{
			_animationPlayer.Play($"idle_{_animDirection}");
		}
	}
}
