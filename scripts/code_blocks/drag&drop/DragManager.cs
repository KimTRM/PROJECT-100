using Godot;
using Godot.Collections;

public partial class DragManager : Control
{
    [Signal] public delegate void BlockDroppedEventHandler();
    [Signal] public delegate void BlockModifiedEventHandler();

    [Export] private BlockPicker _blockPicker;
    [Export] private BlockCanvas _blockCanvas;

    [Export] private Array _dropAreas = new();

    private Node _originalParent;
    private Control _draggedObject;
    private Vector2 _offset;

    private Node _dropTarget;

    [Export] private Array<Node> blocks;

    public bool dragging = false;

    public override void _Ready()
    {
        blocks = GetTree().GetNodesInGroup("CodeBlock");

        foreach (CodeBlock block in blocks)
        {
            block.DragStarted += (CodeBlock block) =>
            {
                GD.Print(block.Name);
                StartDrag(block);
            };
        }
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

            if (mouseEvent.IsPressed() && mouseEvent.ButtonIndex == MouseButton.Right)
            {
                _dropAreas?.Clear();

                for (int i = 0; i < GetTree().GetNodesInGroup("DropArea").Count; i++)
                {
                    var dropArea = GetTree().GetNodesInGroup("DropArea")[i];
                    _dropAreas.Add(dropArea);
                }
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
        _offset = GetGlobalMousePosition() - _draggedObject.GlobalPosition;

    }

    private void EndDrag()
    {
        dragging = false;

        Node newParent = _dropTarget ?? _originalParent;

        if (_draggedObject != null && newParent != null && newParent.IsInsideTree())
        {
            if (!_draggedObject.IsAncestorOf(newParent))
            {
                _draggedObject.Reparent(newParent);
                _draggedObject.GlobalPosition = GetGlobalMousePosition() - _offset;
            }
            else
            {
                _draggedObject.Reparent(_originalParent);
                _draggedObject.GlobalPosition = GetGlobalMousePosition() - _offset;
            }
        }
        _draggedObject = null;
    }
}
