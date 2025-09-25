using Godot;
using GodotUtilities;
using System.Threading.Tasks;

[Scene]
public partial class CodeBlock : MarginContainer
{
    [Signal] public delegate void DragStartedEventHandler(CodeBlock codeBlock);

    [Node, Export] public DragAreaComponent dragAreaComponent;
    [Node, Export] public DropAreaComponent dropAreaComponent;

    [Export] public Resource BlockDefinition = null;

    [Export] public Types.BlockType BlockType = Types.BlockType.NONE;

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
        if (dragAreaComponent == null && dropAreaComponent == null) return;

        dragAreaComponent.DragStarted += StartDrag;
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
