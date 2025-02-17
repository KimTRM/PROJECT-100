using Godot;
using System;

public partial class IdleState : PlayerState
{
	public IdleState(Player player) : base(player) {}

    public override void Enter()
	{
		player.animationPlayer.Play("idle_side");
	}

	public override void HandleInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("Left") || Input.IsActionJustPressed("Right"))
        {
            player.Transition("WalkingState");
        }
        else if (Input.IsActionJustPressed("Jump"))
        {
            player.Transition("JumpingState");
        }
    }

	public override void Update(double delta)
	{
		if (!player.IsOnFloor())
        {
			player.Transition("FallingState");
        }
	}
}
