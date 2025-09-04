using Godot;

[GlobalClass]
public partial class DragAreaComponent : Node
{
	[Signal] public delegate void DragStartedEventHandler();

	[Export]
	private Control draggableArea;
	private DragManager dragManager;

	public override void _Ready()
	{
		CodeBlockManager.Instance.DragManagerReady += InitalizeDraggable;
	}

	private void InitalizeDraggable(DragManager dragManager)
	{
		this.dragManager = dragManager;

		if (draggableArea == null)
		{
			GD.PrintErr("Draggable area is not set for DragAreaComponent." + GetParent().Name);
			return;
		}
		draggableArea.GuiInput += GuiInput;
	}

	private void GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			if (!dragManager.dragging && mouseEvent.Pressed)
			{
				dragManager.StartDrag(GetParent() as CodeBlock);
			}
		}
	}
}
