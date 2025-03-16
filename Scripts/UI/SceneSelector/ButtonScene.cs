using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class ButtonScene : MarginContainer
{
	[Node("NewGame")]
	PanelContainer panel;
	[Node("NewGame/HBoxContainer/MarginContainer/PreviewImage")]
	TextureRect PreviewImage;
	[Node("NewGame/HBoxContainer/Label")]
	Label label;

	[Export] Texture2D textureRect;
	[Export] string SceneTitle;
	[Export] PackedScene SceneToLoad;

    public override void _Notification(int what)
    {
        if(what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

    public override void _Ready()
	{
		PreviewImage.Texture = textureRect;
		label.Text = SceneTitle;
	}

	void _on_new_game_button_pressed()
	{
		if (SceneToLoad == null) return;

		GameManager.Instance.LoadScene(SceneToLoad.ResourcePath);
	}

	void _on_new_game_button_mouse_entered()
	{
		label.Modulate = new Color("000000");
		panel.Size = new Vector2(720,0);
	}

	void _on_new_game_button_mouse_exited()
	{
		label.Modulate = new Color("00536d");
		panel.Size = new Vector2(GetParent<VBoxContainer>().Size.X, 0);
	}
}
