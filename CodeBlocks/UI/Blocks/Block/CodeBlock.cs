using Godot;
using System.Threading.Tasks;

public partial class CodeBlock : Control
{
    [Export] Resource BlockDefinition = null;

    public virtual async Task Execute() { await Task.CompletedTask; }


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
}
