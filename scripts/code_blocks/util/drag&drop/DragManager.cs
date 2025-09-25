using Godot;
using Godot.Collections;

public partial class DragManager : Control
{
    [Signal] public delegate void BlockDroppedEventHandler();
    [Signal] public delegate void BlockModifiedEventHandler();

    [Export] private BlockPicker blockPicker;
    [Export] private BlockCanvas blockCanvas;

    [Export] private Array<Node> blocks = new();
    [Export] private Array<Node> dropAreas = new();

    private CodeBlock draggedObject;
    private Node originalParent;
    private Node dropTarget;
    private Vector2 offset;

    public bool dragging = false;

    public override void _Ready()
    {
        GetCodeBlocks();
        SetClosestDroppableTargets();

        blockCanvas.ZoomAdjusted += AdjustZoom;
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
        offset = GetGlobalMousePosition() - draggedObject.GlobalPosition + new Vector2(0, 8);

        draggedObject.Reparent(this);
    }

    private void EndDrag()
    {
        if (draggedObject == null) return;

        Node newParent = dropTarget ?? originalParent;

        if (blockPicker.GetRect().HasPoint(GetGlobalMousePosition()) && draggedObject != null)
        {
            draggedObject.Reparent(this);
            RemoveBlock(draggedObject);
        }

        if (blockCanvas.GetRect().HasPoint(draggedObject.GetGlobalPosition()))
            draggedObject.Reparent(blockCanvas.Window);

        if (!draggedObject.IsAncestorOf(newParent))
            draggedObject.Reparent(newParent);
        else
            draggedObject.Reparent(originalParent);

        draggedObject = null;
        dropTarget = null;
        dragging = false;
    }

    private async void RemoveBlock(CodeBlock block)
    {
        if (block == null) return;

        draggedObject.ZIndex = 1000;
        block.Position = GetGlobalMousePosition();

        if (block is Control ctrl)
        {
            ctrl.PivotOffset = ctrl.GetRect().Size / 2.0f;
        }

        var tween = GetTree().CreateTween();
        tween.TweenProperty(block, "scale", new Vector2(0, 0), 0.15f);

        await ToSignal(tween, "finished");

        block.QueueFree();
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
            if (!blockCanvas.IsAncestorOf(dropArea))
                continue;

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
            closestDropArea.DroppedBlock = draggedObject;
            EmitSignal(SignalName.BlockDropped);
        }
    }

    private void AdjustZoom(float newZoom)
    {
        var _minZoom = 0.1f;
        var _maxZoom = 5.0f;
        var _zoomFactor = Mathf.Clamp(newZoom, _minZoom, _maxZoom);
        Scale = new Vector2(_zoomFactor, _zoomFactor);
    }
}
