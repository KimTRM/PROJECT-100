using Godot;
using System;

public partial class WalkState : State
{
	[Export] public float Speed = 200.0f;

	public override void Enter()
	{
		player.UpdateAnimation("walk");
	}

	public override void Update(float delta)
	{
		if (player.Direction == Vector2.Zero)
		{
			sm.TransitionTo("Idle");
		}

		player.Velocity = player.Direction * Speed;

		if (player.SetDirection())
		{
			player.UpdateAnimation("walk");
		}
	}
}
