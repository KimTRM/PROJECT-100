using Godot;
using Godot.Collections;

public partial class DragManager : Control
{
    [Signal] public delegate void BlockDroppedEventHandler();
    [Signal] public delegate void BlockModifiedEventHandler();

    [Export] private BlockPicker blockPicker;
    [Export] private BlockCanvas blockCanvas;

    [Export]
    private Array<Node> blocks = new();
    private Array<Node> dropAreas = new();

    private CodeBlock draggedObject;
    private Node originalParent;
    private Node dropTarget;
    private Vector2 offset;

    public bool dragging = false;

    public override void _Ready()
    {
        GetCodeBlocks();

        // blockPicker.MouseEntered += () => SetDroppableTarget(blockPicker.CodeBlockContainer);
        blockCanvas.MouseEntered += () => SetDroppableTarget(blockCanvas.Window);
    }

    public override void _Process(double delta)
    {
        if (dragging && draggedObject != null)
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
        {
            GetCodeBlocks();
            SetClosestDroppableTargets();
            EndDrag();
        }
    }

    private void StartDrag(CodeBlock draggable)
    {
        if (dragging) return;

        dragging = true;

        originalParent = draggable.GetParent();
        draggedObject = draggable;
        offset = GetGlobalMousePosition() - draggedObject.GlobalPosition;

        draggedObject.MouseFilter = MouseFilterEnum.Ignore;
        draggedObject.Reparent(this);
    }

    private void EndDrag()
    {
        Node newParent = dropTarget ?? originalParent;

        if (blockPicker.GetRect().HasPoint(GetLocalMousePosition()))
            draggedObject.QueueFree();

        if (draggedObject != null && newParent != null)
        {
            if (!draggedObject.IsAncestorOf(newParent))
                draggedObject.Reparent(newParent);
            else if (blockCanvas.GetRect().HasPoint(GetLocalMousePosition()))
                draggedObject.Reparent(blockCanvas.Window);
            else
                draggedObject.Reparent(originalParent);
        }

        draggedObject = null;
        dropTarget = null;
        dragging = false;
    }

    private void GetCodeBlocks()
    {
        blocks = GetTree().GetNodesInGroup("CodeBlock");
        foreach (CodeBlock block in blocks) block.DragStarted += StartDrag;
    }

    private void SetDroppableTarget(Node target)
    {
        if (target.IsAncestorOf(target)) return;

        dropTarget = target;
    }

    private void SetClosestDroppableTargets()
    {
        if (draggedObject == null)
            return;

        dropAreas = GetTree().GetNodesInGroup("DropArea");

        DropAreaComponent closestDropArea = null;
        float bestDist = float.MaxValue;
        var mousePos = GetGlobalMousePosition();

        foreach (DropAreaComponent dropArea in dropAreas)
        {
            if (dropArea == null)
                continue;

            if (dropArea.HasBlock())
                continue;

            if (!dropArea.GetGlobalRect().HasPoint(mousePos))
                continue;

            if (draggedObject.BlockType != dropArea.allowedBlockTypes)
                continue;

            float distance = draggedObject.GlobalPosition.DistanceTo(dropArea.GlobalPosition);
            if (distance < bestDist)
            {
                bestDist = distance;
                closestDropArea = dropArea;
            }
        }

        if (closestDropArea != null)
        {
            SetDroppableTarget(closestDropArea);
            EmitSignal(SignalName.BlockDropped);
        }
    }
}
