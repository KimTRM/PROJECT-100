using Godot;
using Godot.Collections;

[GlobalClass]
public partial class DropAreaComponent : PanelContainer
{
	[Export]
	private bool isDroppable = true;

	[Export]
	private Array<string> allowedBlockTypes = new Array<string>();

	[Export]
	private Control droppableArea;
	public DragManager DragManager;

	public override void _Ready()
	{
		if (droppableArea == null)
		{
			GD.PrintErr("Droppable area is not set for DropAreaComponent." + GetParent().Name);
			return;
		}

		CodeBlockManager.Instance.DragManagerReady += DragManagerReady;
		droppableArea.MouseEntered += OnMouseEntered;
	}

	private void DragManagerReady(DragManager dragManager)
	{
		DragManager = dragManager;
	}

	private void OnMouseEntered()
	{
		DragManager.SetDroppableTarget(this);
	}
}
