using Godot;
using System.Threading.Tasks;

[GlobalClass]
public abstract partial class BlockDefinition : Resource
{
    [Export]
    public string BlockName { get; set; } = "BaseBlock";

    [Export(PropertyHint.MultilineText)]
    public string Description { get; set; } = "A base code block.";

    [Export]
    public BlockType BlockType { get; set; } = BlockType.NONE;

    [Export]
    public Variant.Type ReturnType { get; set; } = Variant.Type.Nil;

    public virtual async Task Execute() { await Task.CompletedTask; }
}
