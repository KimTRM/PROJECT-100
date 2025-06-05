using Godot;
using System;
using System.Threading.Tasks;

public partial class CodeBlock : Control
{
    [Export] Resource BlockDefinition = null;

    public string BlockType;
    public Variant BlockValue;

    public virtual async Task Execute() { await Task.CompletedTask; }

    public DragManager dragManager;

    [Export] public bool Dragging = false;
    public CodeBlock NextBlock = null;

    private Vector2 _offset = Vector2.Zero;
    private CodeBlock _snapTarget = null;

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            if (!dragManager.dragging && mouseEvent.Pressed)
            {
                dragManager.StartDrag(this);
            }
        }
    }
}
