using Godot;
using System.Threading.Tasks;

public partial class CodeBlock : Control
{
    [Export] Resource BlockDefinition = null;

    public string BlockType;
    public Variant BlockValue;

    public DragManager dragManager;

    public virtual async Task Execute() { await Task.CompletedTask; }

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
