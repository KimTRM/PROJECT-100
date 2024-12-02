using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager gameManager {get; private set;}
	
	public override void _Ready()
	{
		gameManager = this;
	}

	public int health = 100;
		
	public void TakeDamage(int damage)
	{
		if (health > 0)
		{
			health -= damage;
		}

		if (health <= 0)
		{
			health = 0;
			GD.Print("You can't take damage!");
		}
	}
}
