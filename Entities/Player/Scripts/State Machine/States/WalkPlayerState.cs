using Godot;
using System;

public partial class WalkPlayerState : PlayerState
{
	[Export (PropertyHint.Enum, "Slow: 150, Normal: 200, Fast: 500")] 
	public int Speed = 200;
	
	public override void Enter()
	{
		GD.Print($"Walk: {Speed}");
		player.UpdateAnimation("walk");
	}

	public override void HandleInput(InputEvent @event)
	{
		Speed = Input.IsActionPressed("Run") ? 300 : 200;
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
