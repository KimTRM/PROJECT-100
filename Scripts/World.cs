using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class World : Node2D
{
	[Node("UserInterface/MainPanel")]
	private MarginContainer MainPanel;

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

    public override void _Input(InputEvent @event)
    {
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			MainPanel.Visible = !MainPanel.Visible;
		}
    }
}
