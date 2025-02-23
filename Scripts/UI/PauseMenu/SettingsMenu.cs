using Godot;
using System;

public partial class SettingsMenu : CanvasLayer
{
	public override void _Ready()
	{
		Hide();
	}

	private void _on_resolutions_item_selected(int index)
	{
		switch (index)
		{
			case 0:
				DisplayServer.WindowSetSize(new Vector2I(1920, 1080));
				break;
			case 1:
				DisplayServer.WindowSetSize(new Vector2I(1600, 900));
				break;
			case 2:
				DisplayServer.WindowSetSize(new Vector2I(1280, 720));
				break;
		}
	}

	private void _on_full_screen_toggled(bool button_pressed)
	{
		DisplayServer.WindowSetMode(button_pressed ? DisplayServer.WindowMode.ExclusiveFullscreen : DisplayServer.WindowMode.Windowed);
	}

	private void _on_save_settings_button_pressed()
	{
		Hide();
	}
}
