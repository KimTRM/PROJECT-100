using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
	[Export] bool visibleOnPause = true;

	public override void _Ready()
	{
		GameManager.Instance.GamePauseToggle += OnGamePauseToggle;

		if (!visibleOnPause) return;
		Hide();
	}

	private void OnGamePauseToggle(bool isPaused)
	{
		if (visibleOnPause == isPaused)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	void _on_resume_button_pressed()
	{
		Hide();
		GameManager.Instance.isPaused = false;
		GetTree().Paused = false;
	}

	void _on_activities_button_pressed()
	{
		GameManager.Instance.LoadScene("res://Scenes/UI/ActivitesMenu/ActivitiesMenu.tscn");
	}

	void _on_settings_button_pressed()
	{
		UiManager.Instance.ChangeCurrentUI(UiManager.Instance.uiElements["SettingsMenu"]);
		// SettingsMenu SettingsMenu = (SettingsMenu)GetNode("/root/SettingsMenu");
		// SettingsMenu.Show();
	}

	void _on_save_quit_button_pressed()
	{
		if (GameManager.Instance.PlayerID != null)
		{
			GameManager.Instance.SavePlayerData(GameManager.Instance.PlayerID, PlayerManager.Instance.player.GlobalPosition, "1", "5");
			PlayerManager.Instance.UnparentPlayer(PlayerManager.Instance.player.GetParent() as Node2D);
			PlayerManager.Instance.playerSpawned = false;
		}

		GameManager.Instance.isGamePuasable = false;
		
		Hide();
		GameManager.Instance.isPaused = false;
		GetTree().Paused = false;
		GameManager.Instance.LoadScene("res://Scenes/UI/StartingScreen/StartingMenu.tscn");
	}
}
