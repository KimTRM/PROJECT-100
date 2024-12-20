using Godot;
using System;

public partial class Walk : DalState
{
	[Export] public float Speed = 200.0f;

	public override void Enter()
	{
		DAL.UpdateAnimation("walk");
	}

	public override void Update(float delta)
	{
		if (DAL.Direction == Vector2.Zero)
		{
			stateMachine.TransitionTo("Idle");
		}

		DAL.Velocity = DAL.Direction * Speed;

		if (DAL.SetDirection())
		{
			DAL.UpdateAnimation("walk");
		}
	}
}
