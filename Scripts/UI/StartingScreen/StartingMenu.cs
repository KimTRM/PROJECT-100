using Godot;
using Godot.Collections;

public partial class StartingMenu : CanvasLayer
{
	[Export]
	private Array<Dictionary> Data = new();

    public override void _Ready()
    {
        HTTPManager.Instance.RequestCompleted += OnLogin;
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_PLAYER_DATA"]);
    }

	public void OnLogin(Array<Dictionary> response)
	{
		Data = response;
		HTTPManager.Instance.RequestCompleted -= OnLogin;
	}

    void _on_start_button_pressed()
	{
		// GameManager.Instance.LoadScene("res://Scenes/Worlds/world.tscn");
	}

	void _on_continue_button_pressed()
	{
		GameManager.Instance.LoadScene("res://Scenes/Worlds/GameScene.tscn");
		
	}

	void _on_select_scene_button_pressed()
	{
		GameManager.Instance.LoadScene("res://Scenes/UI/SceneSelector/SceneLoader.tscn");
	}

	void _on_settings_button_pressed()
	{
		SettingsMenu SettingsMenu = UiManager.Instance.uiElements["SettingsMenu"] as SettingsMenu;
		UiManager.Instance.ChangeCurrentUI(SettingsMenu);
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
		GameManager.Instance.PlayerUsername = null;
		GameManager.Instance.PlayerID = null;
		GameManager.Instance.LoadScene("res://Scenes/UI/LoginMenu/LoginMenu.tscn");
	}
}
