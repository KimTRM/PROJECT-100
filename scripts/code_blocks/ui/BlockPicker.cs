using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class BlockPicker : PanelContainer
{
	[Node("ScrollContainer/MarginContainer/CodeBlockContainer")]
	public VBoxContainer codeBlockContainer;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
}
