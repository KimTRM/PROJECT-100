using Godot;
using System;

public partial class FallingState : PlayerState
{
	private Vector2 gravityVector;

	public FallingState(Player player) : base(player) 
	{
		gravityVector = new Vector2(0, player.GravityScale);
	}

	public override void Enter()
	{
		// player.animationPlayer.Play("fall");
	}

	public override void PhysicsUpdate(double delta)
    {
		gravityVector.Y += player.GravityScale * (float)delta;
		player.Velocity += gravityVector;
		player.MoveAndSlide();

        if (player.IsOnFloor())
        {
			player.Transition("IdleState");
        }
    }
}