using Godot;

public partial class StartingMenu : Control
{
	void _on_start_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Worlds/world.tscn");
	}

	void _on_settings_button_pressed()
	{
		SettingsMenu SettingsMenu = (SettingsMenu)GetNode("/root/SettingsMenu");
		SettingsMenu.Show();
	}

	void _on_about_button_pressed()
	{
	
	}

	void _on_exit_button_pressed()
	{
		GetTree().Quit();
	}

	void _on_logout_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/UI/LoginMenu/LoginMenu.tscn");
	}
}
