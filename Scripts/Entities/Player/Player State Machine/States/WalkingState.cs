using Godot;
using System;

public partial class WalkingState : PlayerState
{
    public WalkingState(Player player) : base(player) {}

    public override void Enter()
	{
		player.animationPlayer.Play("walk_side");
	}

	public override void HandleInput(InputEvent @event)
    {
        if (Input.IsActionJustReleased("Left") || Input.IsActionJustReleased("Right"))
        {
            player.Transition("IdleState");
        }
        else if (Input.IsActionJustPressed("Jump"))
        {
			player.Transition("JumpingState");
        }
    }

	public override void PhysicsUpdate(double delta)
    {
        float direction = Input.GetAxis("Left", "Right");
        player.Velocity = new Vector2(direction * player.MaxSpeed, player.Velocity.Y);
		player.MoveAndSlide();
    }
}
