using Godot;
using System.Threading.Tasks;

public partial class CodeBlock : MarginContainer
{
    [Signal] public delegate void DragStartedEventHandler(CodeBlock codeBlock);
    [Export] private Resource BlockDefinition = null;

    [Export] public Types.BlockType BlockType = Types.BlockType.NONE;

    private Variant BlockValue;

    public virtual async Task Execute() { await Task.CompletedTask; }

    public Variant GetBlockValue()
    {
        return BlockValue;
    }

    public void SetBlockValue(Variant value)
    {
        BlockValue = value;
    }
}
