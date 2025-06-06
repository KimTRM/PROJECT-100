using Game.Globals;
using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class GameScene : Node2D
{

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		AudioManager.Instance.PlayMusic((AudioStream)GD.Load("res://Assets/AudioFiles/Ludum Dare 28 01.ogg"));
	}


}
