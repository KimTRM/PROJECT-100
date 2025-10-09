using Godot;
using System;
using System.Threading.Tasks;

public partial class IntBlock : BlockDefinition
{
    [Export] public int Value { get; set; } = 0;

    public override async Task Execute()
    {
        await Task.Delay(10);
        GD.Print($"Integer Block Executed with Value: {Value}");
    }
}
