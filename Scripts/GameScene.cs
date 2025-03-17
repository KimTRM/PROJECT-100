using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class GameScene : Node2D
{
	[Node]
	public Player Player;

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

    public override void _Ready()
	{
	}

	
}
