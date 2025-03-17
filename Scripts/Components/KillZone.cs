using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class KillZone : Area2D
{
	[Node]
	Timer timer;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

	private void _on_body_entered(Node body)
	{
		GD.Print("You died!");
        Engine.TimeScale = 0.5f;
        body.GetNode<CollisionShape2D>("CollisionShape2D").QueueFree();
        timer.Start();
	}
    
	private void _on_timer_timeout()
	{
		Engine.TimeScale = 1.0f;
        GetTree().ReloadCurrentScene();
		PlayerManager.Instance.UnparentPlayer(PlayerManager.Instance.player.GetParent() as Node2D);
		PlayerManager.Instance.playerSpawned = false;
	}
}
