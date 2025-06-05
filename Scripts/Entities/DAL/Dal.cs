using Godot;
using System;
using GodotUtilities;

[Scene, Icon("res://Assets/Icons/DAL.png")]
public partial class Dal : CharacterBody2D
{
	[Node] private Sprite2D _sprite2D;
	[Node] private AnimationPlayer _animationPlayer;
	[Node] private PathFindComponent _pathFindComponent;
	[Node] private VelocityComponent _velocityComponent;
	[Node] private InputController _inputController;

	private string animDirection = "down";
	private Vector2 target;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		// Velocity = _velocityComponent.GetVelocity(_inputController.GetMovementInput());
		// _velocityComponent.AccelerateToVelocity(Velocity, (float)delta);

		target = PlayerManager.Instance.player.GlobalPosition;
		_pathFindComponent.SetTargetPosition(target);
		_pathFindComponent.FollowPath(delta);
		_velocityComponent.Move(this);

		UpdateAnimation();
	}

	private void UpdateAnimation()
	{
		Vector2 movementInput = target.Normalized();

		if (movementInput == Vector2.Up)
		{
			animDirection = "up";
		}
		else if (movementInput == Vector2.Down)
		{
			animDirection = "down";
		}
		// else if (movementInput == Vector2.Left || movementInput == Vector2.Right)
		// {
		// 	animDirection = "side";
		// }


		if (movementInput != Vector2.Zero)
		{
			_animationPlayer.Play($"walk_{animDirection}");
			_sprite2D.FlipH = _inputController.GetMovementInput().X < 0;
		}
		else
		{
			_animationPlayer.Play($"idle_{animDirection}");
		}
	}
}
