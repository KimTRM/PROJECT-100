using Godot;
using System.Threading.Tasks;

public partial class IfBlock : BlockDefinition
{
    public override async Task Execute()
    {
        await Task.Delay(10);
        GD.Print("If Block Executed");
    }
}
