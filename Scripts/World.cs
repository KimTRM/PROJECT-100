using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class World : Node2D
{
	[Node("UserInterface/MainPanel")]
	private MarginContainer MainPanel;

	public Vector2 PlayerPosition;

	public override void _Ready()
	{
		GD.Print(PlayerPosition);
		// GameManager.Instance.player.Position = PlayerPosition;

	}

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

    public override void _Input(InputEvent @event)
    {
		if (Input.IsActionJustPressed("ui_focus_next"))
		{
			MainPanel.Visible = !MainPanel.Visible;
		}
    }
}
