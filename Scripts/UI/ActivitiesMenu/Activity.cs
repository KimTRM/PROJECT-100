using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class Activity : MarginContainer
{
	[Node("PanelContainer/VBoxContainer/HBoxContainer/ActivityName")]
	public Label ActivityName;
	[Node("PanelContainer/VBoxContainer/HBoxContainer/HBoxContainer/NumItems")]
	public Label NumItems;
	[Node]
	public Button button;

	[Export]
	public PackedScene scene;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

}
