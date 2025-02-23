using Godot;
using System;

public partial class StartingMenu : Control
{
	void _on_start_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Worlds/world.tscn");
	}

	void _on_settings_button_pressed()
	{
		
	}

	void _on_about_button_pressed()
	{
		
	}
}
