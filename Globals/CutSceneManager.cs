using Godot;
using System;

public partial class CutSceneManager : Node
{
	
	public override void _Ready()
	{
		CutSceneLoader cutSceneLoader = (CutSceneLoader)ResourceLoader.Load<PackedScene>("res://Scenes/UI/CutSceneLoader/CutSceneLoader.tscn").Instantiate();
		// cutSceneLoader.DisplayText("Hello, World!");
		cutSceneLoader.Hide();
		AddChild(cutSceneLoader);
	}

	
}
