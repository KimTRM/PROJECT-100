using Godot;
using System;
using GodotUtilities;

[Scene, Icon("res://Assets/Icons/DAL.png")]
public partial class Dal : CharacterBody2D
{
	[Node]
	private Sprite2D sprite2D;
	[Node]
	private AnimationPlayer animationPlayer;
	[Node]
	private VelocityComponent velocityComponent;
	[Node]
	private InputController inputController;

	private string animDirection = "down";

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = velocityComponent.GetVelocity(inputController.GetMovementInput());
		velocityComponent.AccelerateToVelocity(Velocity, (float)delta);
		velocityComponent.Move(this);

		UpdateAnimation();
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
		// else if (movementInput == Vector2.Left || movementInput == Vector2.Right)
		// {
		// 	animDirection = "side";
		// }


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
