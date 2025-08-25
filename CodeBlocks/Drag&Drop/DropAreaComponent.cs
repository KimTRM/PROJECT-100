using Godot;
using Godot.Collections;

[GlobalClass]
public partial class DropAreaComponent : PanelContainer
{
	[Export]
	private bool isDroppable = true;

	[Export]
	private Control dropContainer;

	[Export]
	private Control dropZone;

	[Export]
	private Array<string> allowedBlockTypes = new Array<string>();

	public DragManager DragManager;

	public override void _Ready()
	{
		if (dropZone == null)
		{
			GD.PrintErr("Droppable area is not set for DropAreaComponent." + GetParent().Name);
			return;
		}

		CodeBlockManager.Instance.DragManagerReady += DragManagerReady;
		dropZone.MouseEntered += OnMouseEntered;
	}

	private void DragManagerReady(DragManager dragManager)
	{
		DragManager = dragManager;
	}

	private void OnMouseEntered()
	{
		if (dropContainer == null)
			dropContainer = this;

		DragManager.SetDroppableTarget(dropContainer);
	}
}
