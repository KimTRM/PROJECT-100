using Godot;
using System;

[GlobalClass]
public partial class BaseBlock : Resource
{
    [Export]
    public string BlockName { get; set; } = "BaseBlock";
    [Export(PropertyHint.MultilineText)]
    public string Description { get; set; } = "A base code block.";
    [Export]
    public BlockType BlockType { get; set; } = BlockType.NONE;
    [Export]
    public Variant.Type ReturnType { get; set; } = Variant.Type.Nil;

}
