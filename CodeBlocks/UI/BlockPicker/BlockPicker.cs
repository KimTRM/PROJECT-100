using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class BlockPicker : PanelContainer
{
	[Node("ScrollContainer/MarginContainer/CodeBlockContainer")]
	public VBoxContainer codeBlockContainer;

	[Export] private DragManager dragManager;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		foreach (Control child in codeBlockContainer.GetChildren())
		{
			if (child is BlockCategoryContainer blockCategoryContainer)
			{
				dragManager.DragManagerReady += blockCategoryContainer.OnDragManagerReady;
			}
		}
	}
}
