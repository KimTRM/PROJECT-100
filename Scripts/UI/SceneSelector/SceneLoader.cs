using Godot;
using System;

public partial class SceneLoader : CanvasLayer
{
	void _on_back_button_pressed()
	{
		GameManager.Instance.LoadScene("res://Scenes/UI/StartingScreen/StartingMenu.tscn");
	}
}
