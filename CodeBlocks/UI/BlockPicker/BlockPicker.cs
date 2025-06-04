using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class BlockPicker : MarginContainer
{
	[Export]
	private NodePath DragManagerPath;
	private DragManager dragManager;

	[Node("ScrollContainer/MarginContainer/CodeBlockContainer")]
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
		codeBlockContainer = GetNode<VBoxContainer>("ScrollContainer/MarginContainer/CodeBlockContainer");
		dragManager = GetNode<DragManager>(DragManagerPath);

		foreach (Control child in codeBlockContainer.GetChildren())
		{
			if (child is CodeBlock block)
			{
				block.dragManager = dragManager;
			}
		}
	}
}
