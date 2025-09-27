using Godot;
using System.Linq;
using Godot.Collections;

public partial class DragManager : Control
{
    [Signal] public delegate void BlockDroppedEventHandler();
    [Signal] public delegate void BlockModifiedEventHandler();

    [Export] private BlockPicker blockPicker;
    [Export] private BlockCanvas blockCanvas;

    [Export] float blockSmoothing = 0.15f;

    private Array<Node> blocks = [];
    private Array<Node> dropAreas = [];

    private CodeBlock draggedObject;
    private Node originalParent;
    private Node dropTarget;
    private Vector2 offset;

    private bool dragging = false;

    public override void _Ready()
    {
        GetCodeBlocks();
        SetClosestDroppableTargets();

        blockCanvas.MouseEntered += () => SetDroppableTarget(blockCanvas.Window);
    }

    public override void _Process(double delta)
    {
        if (dragging && draggedObject != null && IsInstanceValid(draggedObject))
            draggedObject.GlobalPosition = draggedObject.GlobalPosition.Lerp(
            GetGlobalMousePosition() - offset,
            (float)delta * blockSmoothing
        );

        AdjustZoom();
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
        offset = GetGlobalMousePosition() - draggedObject.GlobalPosition + new Vector2(0, 2);

        draggedObject.Resize();
        draggedObject.Reparent(this);
    }

    private void EndDrag()
    {
        dragging = false;

        if (draggedObject == null || !IsInstanceValid(draggedObject)) return;

        Node newParent = dropTarget ?? originalParent;

        bool removeBecauseInvalidParent = draggedObject.IsAncestorOf(newParent);
        bool removeBecausePicker = blockPicker.GetRect().HasPoint(GetGlobalMousePosition());

        if (removeBecauseInvalidParent || removeBecausePicker || originalParent == null)
        {
            RemoveBlock(draggedObject);
        }
        else if (blockCanvas.GetRect().HasPoint(draggedObject.GetGlobalPosition()))
        {
            draggedObject.Reparent(blockCanvas.Window);
        }
        else
        {
            draggedObject.Reparent(newParent);
        }

        draggedObject = null;
        dropTarget = null;
    }

    private async void RemoveBlock(CodeBlock block)
    {
        if (block == null || !IsInstanceValid(block)) return;

        block.Reparent(this);
        block.ZIndex = 1000;
        block.PivotOffset = block.GetRect().Size / 2.0f;
        block.GlobalPosition = GetGlobalMousePosition() - offset;

        var tween = GetTree().CreateTween();
        tween.TweenProperty(block, "scale", new Vector2(0, 0), 0.15f);

        await ToSignal(tween, "finished");

        if (IsInstanceValid(block))
            block?.QueueFree();
    }

    private void GetCodeBlocks()
    {
        blocks = GetTree().GetNodesInGroup("CodeBlock");
        foreach (CodeBlock block in blocks.Cast<CodeBlock>()) block.DragStarted += StartDrag;
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

        foreach (DropAreaComponent dropArea in dropAreas.Cast<DropAreaComponent>())
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

    private void AdjustZoom()
    {
        if (blockCanvas == null) return;

        float scaleLerpSpeed = 0.116f;

        var minZoom = 0.1f;
        var maxZoom = 5.0f;
        var mousePos = GetGlobalMousePosition();
        var zoomFactor = Mathf.Clamp(blockCanvas.GetZoomFactor(), minZoom, maxZoom);

        bool overCanvas = blockCanvas.GetGlobalRect().HasPoint(mousePos);
        bool overPicker = blockPicker.GetGlobalRect().HasPoint(mousePos);

        Vector2 targetScale = overCanvas
            ? new Vector2(zoomFactor, zoomFactor)
            : Vector2.One;

        if (draggedObject != null)
            draggedObject.Modulate = draggedObject.Modulate.Lerp(
                overPicker
                    ? new Color(0.45f, 0.45f, 0.45f, 0.792f)
                    : new Color(1.0f, 1.0f, 1.0f),
                0.15f
            );

        Scale = Scale.Lerp(targetScale, scaleLerpSpeed);
        GlobalPosition = GlobalPosition.Lerp(mousePos - offset, scaleLerpSpeed);
    }
}
