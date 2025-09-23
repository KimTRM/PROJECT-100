using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class BlockPicker : PanelContainer
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
