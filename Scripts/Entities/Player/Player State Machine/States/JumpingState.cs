using Godot;
using System;

public partial class JumpingState : PlayerState
{
	private Vector2 gravityVector = Vector2.Down;

	public JumpingState(Player player) : base(player) { }

	public override void Enter()
	{
		// player.animationPlayer.Play("jump");
		player.Velocity = new Vector2(player.Velocity.X, -player.JumpMagnitude);
	}

	public override void PhysicsUpdate(double delta)
	{
		player.Velocity += gravityVector * player.GravityScale * (float)delta;
		player.MoveAndSlide();

		if (player.Velocity.Y > 0)
		{
			player.Transition("FallingState");
		}
	}
}