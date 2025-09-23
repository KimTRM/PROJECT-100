using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class BlockPicker : MarginContainer
{
	[Node] public VBoxContainer CodeBlockContainer;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}
