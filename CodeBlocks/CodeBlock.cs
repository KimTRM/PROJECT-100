using Godot;
using System.Collections.Generic;

public partial class CodeBlock : Control
{
    [Export] public bool Dragging = false;
    [Export] public CodeBlock SnapTarget = null;
    [Export] public Vector2 Offset = Vector2.Zero;
    [Export] public CodeBlock ParentBlock = null;

    public string VarCategory;
    public string VarType;
    public string VarName;
    public Variant VarValue;

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButtonEvent)
        {
            if (mouseButtonEvent.Pressed && mouseButtonEvent.ButtonIndex == MouseButton.Left)
            {
                StartDrag();
            }
            else if (!mouseButtonEvent.Pressed)
            {
                StopDrag();
            }
        }

        if (@event is InputEventMouseMotion mouseMotionEvent && Dragging)
        {
            GlobalPosition = GetGlobalMousePosition() + Offset;
        }
    }

    private void StartDrag()
    {
        Dragging = true;
        Offset = GlobalPosition - GetGlobalMousePosition();
        
        if (ParentBlock != null)
        {
            ParentBlock = null;
        }
        
        MoveToFront();
    }

    private void StopDrag()
    {
        Dragging = false;

        if (SnapTarget != null)
        {
            SnapToTarget();
        }
    }

    private void SnapToTarget()
    {
        GlobalPosition = SnapTarget.GlobalPosition + new Vector2(0, 50); // Stack vertically
        ParentBlock = SnapTarget;
    }

    public void Execute()
    {
        GD.Print($"Executing block: {VarName}, Value: {VarValue}");
    }

    private void _on_Area2D_body_entered(Node body)
    {
        if (body is CodeBlock otherBlock)
        {
            SnapTarget = otherBlock;
        }
    }

    private void _on_Area2D_body_exited(Node body)
    {
        if (body == SnapTarget)
        {
            SnapTarget = null;
        }
    }
}
