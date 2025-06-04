using Godot;
using System.Collections.Generic;

public partial class DragManager : Control
{
    [Signal] public delegate void BlockDroppedEventHandler();
    [Signal] public delegate void BlockModifiedEventHandler();

    private BlockCanvas _blockCanvas;

    private BlockPicker _picker;
    private DragBlock dragBlock;

    [Export] private NodePath pickerPath;
    [Export] private NodePath blockCanvasPath;

    public override void _Ready()
    {
        _picker = GetNode<BlockPicker>(pickerPath);
        _blockCanvas = GetNode<BlockCanvas>(blockCanvasPath);
    }

    private Node _originalParent;
    private Control _draggedObject;
    private Vector2 _offset;

    private Node _dropTarget;

    public bool dragging = false;

    public override void _Process(double delta)
    {
        if (dragging)
        {
            _draggedObject.GlobalPosition = GetGlobalMousePosition() - _offset;
        }
    }

    public void StartDrag(Control draggable, InputEvent @event)
    {
        if (draggable == null) return;

        // dragging = true;

        if (@event is InputEventMouseButton mouseEvent)
        {
            if (dragging && !mouseEvent.Pressed && mouseEvent.ButtonIndex != MouseButton.Left)
            {
                EndDrag();
            }
        }

        _originalParent = draggable.GetParent();
        _draggedObject = draggable;
        _offset = GetGlobalMousePosition() - _draggedObject.GlobalPosition;
        _originalParent.RemoveChild(_draggedObject);
        AddChild(_draggedObject); // Temporarily parent to DragManager
    }

    public void EndDrag()
    {
        if (_draggedObject == null) return;

        // dragging = false;

        // if (_dropTarget == _blockCanvas)
        // {
        //     _draggedObject.QueueFree();
        //     _draggedObject = null;
        //     return;
        // }

        Node newParent = GetDropTarget() ?? _originalParent; // Fallback if no valid target

        RemoveChild(_draggedObject);
        newParent.AddChild(_draggedObject);
        _draggedObject.GlobalPosition = GetGlobalMousePosition() - _offset;
        _draggedObject = null;
    }

    private Node GetDropTarget()
    {
        Vector2 mousePos = GetGlobalMousePosition();

        if (_on_block_canvas_mouse_entered())
        {
            GD.Print("Mouse entered BlockCanvas");
            return _blockCanvas.Window;
        }

        if (_on_block_picker_mouse_entered())
        {
            GD.Print("Mouse entered BlockPicker");
            return _picker.codeBlockContainer;
        }

        return null; // No valid target found
    }

    private bool _on_block_canvas_mouse_entered()
    {
        GD.Print("Mouse entered BlockCanvas");
        return true;
    }

    private bool _on_block_picker_mouse_entered()
    {
        GD.Print("Mouse entered BlockPicker");
        return true;
    }
}
