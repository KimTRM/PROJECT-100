using Godot;
using System.Collections.Generic;

public partial class DragManager : Control
{
    [Signal] public delegate void BlockDroppedEventHandler();
    [Signal] public delegate void BlockModifiedEventHandler();

    private BlockCanvas _blockCanvas;

    // private BlockPicker _picker;
    private DragBlock dragBlock;

    [Export] private NodePath pickerPath;
    [Export] private NodePath blockCanvasPath;

    public override void _Ready()
    {
        // _picker = GetNode<Picker>(pickerPath);
        _blockCanvas = GetNode<BlockCanvas>(blockCanvasPath);
    }

    public override void _Process(double delta)
    {
        // dragBlock?.UpdateDragState();
    }

    public void StartDrag(CodeBlock block, Vector2 offset)
    {
        if (block.GetParent() != null)
        {
            block.GetParent().RemoveChild(block);
        }
        
        dragBlock = new DragBlock(block, offset, _blockCanvas);
        AddChild(dragBlock);
    }

    public void EndDrag()
    {
        if (dragBlock == null) return;

        CodeBlock placedBlock = dragBlock.ApplyDrag();
        if (placedBlock != null)
        {
            EmitSignal(SignalName.BlockDropped);
        }
        
        dragBlock.QueueFree();
        dragBlock = null;
    }
}
