using Godot;
using System.Collections.Generic;

public partial class DragManager : Control
{
    [Signal] public delegate void BlockDroppedEventHandler();
    [Signal] public delegate void BlockModifiedEventHandler();

    [Export] private BlockCanvas _blockCanvas;
    [Export] private BlockPicker _blockPicker;

    private Node _originalParent;
    private Control _draggedObject;
    private Vector2 _offset;

    private Node _dropTarget;

    public bool dragging = false;

    public override void _Ready()
    {
        _blockCanvas.MouseEntered += OnBlockCanvasMouseEntered;
        _blockPicker.MouseEntered += OnBlockPickerMouseEntered;
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
            if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.IsReleased())
            {
                if (dragging)
                {
                    EndDrag();
                }
            }
        }
    }

    public void StartDrag(CodeBlock draggable)
    {
        dragging = true;

        _originalParent = draggable.GetParent();
        _draggedObject = draggable;
        _draggedObject.Reparent(this);
        _offset = GetGlobalMousePosition() - _draggedObject.GlobalPosition;
    }

    public void EndDrag()
    {
        dragging = false;

        Node newParent = GetDropTarget() ?? _originalParent;

        _draggedObject.Reparent(newParent);
        _draggedObject.GlobalPosition = GetGlobalMousePosition() - _offset;
        _draggedObject = null;
    }

    bool canvasEntered;
    bool pickerEntered;

    private Node GetDropTarget()
    {
        if (canvasEntered)
        {
            return _blockCanvas.Window;
        }

        if (pickerEntered)
        {
            return _blockPicker.codeBlockContainer;
        }

        return null;
    }

    private void OnBlockCanvasMouseEntered()
    {
        canvasEntered = true;
    }

    private void OnBlockPickerMouseEntered()
    {
        pickerEntered = true;
    }
}
