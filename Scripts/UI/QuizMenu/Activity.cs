using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class Activity : MarginContainer
{
	[Node("MarginContainer/VBoxContainer/ActivityName")]
	public Label ActivityName;
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
