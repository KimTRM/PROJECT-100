using Godot;
using Godot.Collections;

public partial class DragManager : Control
{
    [Signal] public delegate void BlockDroppedEventHandler();
    [Signal] public delegate void BlockModifiedEventHandler();

    [Export] private BlockPicker blockPicker;
    [Export] private BlockCanvas blockCanvas;

    private Array<Node> blocks = new();
    // private Array<Node> dropArea = new();

    private Node originalParent;
    private Control draggedObject;
    private Vector2 offset;
    private Node dropTarget;

    public bool dragging = false;

    public override void _Ready()
    {
        blocks = GetTree().GetNodesInGroup("CodeBlock");
        // dropArea = GetTree().GetNodesInGroup("DropArea");

        foreach (CodeBlock block in blocks)
        {
            block.DragStarted += StartDrag;
        }
    }

    public override void _Process(double delta)
    {
        if (dragging)
        {
            draggedObject.GlobalPosition = draggedObject.GlobalPosition.Lerp(
                GetGlobalMousePosition() - offset,
                (float)delta * 18.0f
            );
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.IsReleased() && dragging)
            EndDrag();
    }

    public void SetDroppableTarget(Node target)
    {
        if (target.IsAncestorOf(target)) return;

        dropTarget = target;
    }

    public void StartDrag(CodeBlock draggable)
    {
        dragging = true;

        originalParent = draggable.GetParent();
        draggedObject = draggable;
        offset = GetGlobalMousePosition() - draggedObject.GlobalPosition;

        draggedObject.MouseFilter = MouseFilterEnum.Ignore;
        draggedObject.Reparent(this);
    }

    private void EndDrag()
    {
        dragging = false;

        Node newParent = dropTarget ?? originalParent;

        if (draggedObject != null && newParent != null && newParent.IsInsideTree())
        {
            if (!draggedObject.IsAncestorOf(newParent))
            {
                draggedObject.Reparent(newParent);
                draggedObject.GlobalPosition = GetGlobalMousePosition() - offset;
            }
            else
            {
                draggedObject.Reparent(originalParent);
                draggedObject.GlobalPosition = GetGlobalMousePosition() - offset;
            }
        }
        draggedObject = null;
    }
}
