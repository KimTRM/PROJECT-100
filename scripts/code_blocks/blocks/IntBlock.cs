using Godot;
using System.Threading.Tasks;

public partial class IntBlock : BlockDefinition
{
    public override async Task Execute()
    {
        await Task.Delay(10);
        GD.Print("Int Block Executed");
    }
}
