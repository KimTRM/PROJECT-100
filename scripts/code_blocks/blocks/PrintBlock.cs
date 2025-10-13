using Godot;
using System.Threading.Tasks;

public partial class PrintBlock : BlockDefinition
{
    public override async Task Execute()
    {
        await Task.Delay(10);
        GD.Print("Print Block Executed");
    }
}
