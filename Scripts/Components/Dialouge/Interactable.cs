using Godot;
using System;

public partial class Interactable : Area2D
{
	[Export] public Resource dialogue_resource;
	[Export] public string dialogue_start = "start";

	[Export] public bool interact_on_enter = false;

	public void Interact()
	{

	}

	public void _on_body_entered(Node body)
	{
		if (interact_on_enter)
		{
			Interact();
			interact_on_enter = false;
		}
	}
}
