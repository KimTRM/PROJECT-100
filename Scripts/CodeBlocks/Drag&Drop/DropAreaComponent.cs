using Godot;
using Godot.Collections;

[GlobalClass]
public partial class DropAreaComponent : PanelContainer
{
	[Signal] public delegate void DragStartedEventHandler();

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

		AddToGroup("drop_areas");

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
