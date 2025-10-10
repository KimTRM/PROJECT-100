using Godot;
using GodotUtilities;
using System.Threading.Tasks;

[Scene]
public abstract partial class CodeBlock : MarginContainer
{
    [Signal] public delegate void DragStartedEventHandler(CodeBlock codeBlock);

    [Export] public BlockDefinition BlockDefinition = null;

    [Export] public DragAreaComponent dragAreaComponent;
    [Export] public DropAreaComponent dropAreaComponent;
    [Export] public TemplateEditor templateEditor;

    [Export] public BlockType BlockType = BlockType.NONE;

    private Variant BlockValue;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        AddToGroup("CodeBlock");

        if (dragAreaComponent != null)
            dragAreaComponent.DragStarted += StartDrag;

        if (dropAreaComponent != null)
            dropAreaComponent.BlockRemoved += (CodeBlock) => { Resize(); };
    }

    public virtual async Task Execute() { await Task.CompletedTask; }

    public void StartDrag()
    {
        EmitSignal(SignalName.DragStarted, this);
    }

    public Variant GetBlockValue()
    {
        return BlockValue;
    }

    public void SetBlockValue(Variant value)
    {
        BlockValue = value;
    }

    public void Resize()
    {
        Size = new Vector2(0, 0);
    }
}
