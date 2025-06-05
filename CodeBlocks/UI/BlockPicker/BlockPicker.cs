using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class BlockPicker : MarginContainer
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
		codeBlockContainer = GetNode<VBoxContainer>("ScrollContainer/MarginContainer/CodeBlockContainer");

		foreach (Control child in codeBlockContainer.GetChildren())
		{
			if (child is CodeBlock block)
			{
				block.dragManager = dragManager;
			}
		}
	}
}
