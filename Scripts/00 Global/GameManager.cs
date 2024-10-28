using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager gameManager{get; private set;}
	public override void _Ready()
	{
		gameManager = this;
	}

	public int health = 100;
	
}
