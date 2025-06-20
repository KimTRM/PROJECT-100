using Godot;
using GodotUtilities;
using Godot.Collections;
using System;

[Scene]
public partial class BlockCategoryContainer : PanelContainer
{
	[Export] public string CategoryName;
	[Export] public Array<PackedScene> BlockDefinitions;
	[Export] public DragManager dragManager;

	[Node("VBoxContainer/CategoryTitle")]
	public Label categoryTitle;
	[Node("VBoxContainer/CodeBlockContainer")]
	public VBoxContainer codeBlockContainer;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
	}

	public void OnDragManagerReady(DragManager dragManager)
	{
		this.dragManager = dragManager;
		foreach (Control child in codeBlockContainer.GetChildren())
		{
			if (child is CodeBlock block)
			{
				block.dragManager = dragManager;
			}
		}
	}
}
