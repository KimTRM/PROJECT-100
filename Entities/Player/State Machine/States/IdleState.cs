using Godot;
using System;

public partial class IdleState : State
{
	public override void Enter()
	{
		player.UpdateAnimation("idle");
	}

	public override void Update(float delta)
	{
		if (player.Direction != Vector2.Zero)
		{
			sm.TransitionTo("Walk");
		} 
			
		player.Velocity = Vector2.Zero;
	}
}
