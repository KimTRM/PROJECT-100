using Godot;
using System;

public partial class PlayerSpwaner : Node2D
{
	public override void _Ready()
	{
		Visible = false;
		if(PlayerManager.Instance.playerSpawned == false)
		{
			PlayerManager.Instance.SetAsParent(GetParent() as Node2D);
			PlayerManager.Instance.SetPlayerPosition(GlobalPosition);
			PlayerManager.Instance.playerSpawned = true;
		}
	}
}
