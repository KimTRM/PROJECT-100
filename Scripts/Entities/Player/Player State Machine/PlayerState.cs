using Godot;
using System;

public partial class PlayerState : State
{
	protected Player player;

	public PlayerState(Player player)
	{
		this.player = player;
	}
}
