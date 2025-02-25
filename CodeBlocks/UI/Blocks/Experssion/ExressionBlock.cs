using Godot;
using GodotUtilities;
using System.Threading.Tasks;


[Scene]
public partial class ExressionBlock : CodeBlock
{
	[Node ("MarginContainer/HBoxContainer/ValueContainer1/Value1")]
	public LineEdit value1;
	[Node ("MarginContainer/HBoxContainer/ValueContainer2/Value2")]
	public LineEdit value2;

    public override async Task Execute()
    {
		await Task.CompletedTask;
    }
}
