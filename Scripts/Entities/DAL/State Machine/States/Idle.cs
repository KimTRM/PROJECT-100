using Godot;
using System;

public partial class Idle : States
{
    public override void Enter()
    {
		DAL.UpdateAnimation("idle");
	}

	public override void Update(float delta)
	{
		if (DAL.Direction != Vector2.Zero)
		{
			stateMachine.TransitionTo("Walk");
		} 
			
		DAL.Velocity = Vector2.Zero;
	}
}
