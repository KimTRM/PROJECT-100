using Godot;
using System;

namespace CodeBlocks.Blocks;

public partial class CodeBlock : Control
{
    [Export] public bool Dragging = false; 
    public CodeBlock NextBlock = null;
    
    private Vector2 _offset = Vector2.Zero;
    private CodeBlock _snapTarget = null;
    
    public string BlockType;
    public Variant BlockValue;

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
            {
                StartDrag();
            }
            else if (!mouseEvent.Pressed)
            {
                StopDrag();
            }
        }
    }

    public override void _Process(double delta)
    {
        if (Dragging)
        {
            GlobalPosition = GetGlobalMousePosition() + _offset;
        }
    }

    private void StartDrag()
    {
        Dragging = true;
        _offset = GlobalPosition - GetGlobalMousePosition();
        RaiseZIndex();
    }

    private void StopDrag()
    {
        Dragging = false;
        ZIndex = 0; // Reset Z index when released
        
        if (_snapTarget != null)
        {
            SnapToTarget();
        }
    }

    private void RaiseZIndex()
    {
        ZIndex = 100; // Ensure the dragged block is on top
    }

    private void SnapToTarget()
    {
        GlobalPosition = _snapTarget.GlobalPosition + new Vector2(0, 50);
        NextBlock = _snapTarget;
    }

    // Execution logic
    public void Execute()
    {
        GD.Print("Executing block: " + BlockType);

        if (BlockType == "print")
        {
            GD.Print("PRINT: " + BlockValue);
        }
        else if (BlockType == "loop")
        {
            for (int i = 0; i < Convert.ToInt32(BlockValue); i++)
            {
                GD.Print("Loop Iteration: " + i);
            }
        }

        // Execute the next block in sequence
        if (NextBlock != null)
        {
            GetTree().CreateTimer(0.5f).Timeout += () => NextBlock.Execute();
        }
    }
}
