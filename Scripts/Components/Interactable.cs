using Godot;
using DialogueManagerRuntime;
using System;

public partial class Interactable : Area2D
{
	[Export] public Resource dialogue_resource;
	[Export] public string dialogue_start = "start";

	public void Interact()
	{
		DialogueManager.ShowDialogueBalloon(dialogue_resource, dialogue_start);
	}
}
