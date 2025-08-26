using Godot;

public partial class DragManager : Control
{
    [Signal] public delegate void BlockDroppedEventHandler();
    [Signal] public delegate void BlockModifiedEventHandler();

    private Node _originalParent;
    private Control _draggedObject;
    private Vector2 _offset;

    private Node _dropTarget;

    public bool dragging = false;

    public override void _Ready()
    {
        CodeBlockManager.Instance.EmitDragManagerReady(this);
    }

    public override void _Process(double delta)
    {
        if (dragging)
        {
            _draggedObject.GlobalPosition = _draggedObject.GlobalPosition.Lerp(
                GetGlobalMousePosition() - _offset,
                (float)delta * 18.0f
            );
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.IsReleased() && dragging)
            {
                EndDrag();
            }
        }
    }

    public void SetDroppableTarget(Node target)
    {
        if (target.IsAncestorOf(target)) return;

        _dropTarget = target;
    }

    public void StartDrag(CodeBlock draggable)
    {
        dragging = true;

        _originalParent = draggable.GetParent();
        _draggedObject = draggable;
        _draggedObject.MouseFilter = MouseFilterEnum.Ignore;
        _draggedObject.Reparent(this);
        // Offset bellow the object to align with mouse
        _offset = GetGlobalMousePosition() - _draggedObject.GlobalPosition + new Vector2(0, 18);

    }

    private void EndDrag()
    {
        dragging = false;

        Node newParent = _dropTarget ?? _originalParent;

        if (_draggedObject != null && newParent != null && newParent.IsInsideTree())
        {
            if (!_draggedObject.IsAncestorOf(newParent))
            {
                _draggedObject.MouseFilter = MouseFilterEnum.Pass;
                _draggedObject.Reparent(newParent);
                _draggedObject.GlobalPosition = GetGlobalMousePosition() - _offset;
            }
            else
            {
                // GD.PrintErr("Cannot reparent: would cause cyclic dependency.");
                _draggedObject.MouseFilter = MouseFilterEnum.Pass;
                _draggedObject.Reparent(_originalParent);
                _draggedObject.GlobalPosition = GetGlobalMousePosition() - _offset;
            }
        }
        _draggedObject = null;
    }
}
