using Godot;
using System.Threading.Tasks;

public partial class CodeBlock : Control
{
    [Export] private Resource BlockDefinition = null;

    public string BlockType;
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
