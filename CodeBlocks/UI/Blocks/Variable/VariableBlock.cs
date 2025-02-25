using Godot;
using GodotUtilities;
using System.Threading.Tasks;

[Scene]
public partial class VariableBlock : CodeBlock
{	
	[Node ("MarginContainer/HBoxContainer/VariableLabel")]
	public Label varType;

	[Node ("MarginContainer/HBoxContainer/VariableContainer/VarName")]
	private LineEdit varName;

	[Node ("MarginContainer/HBoxContainer/VariableContainer")]
	private PanelContainer varContainer;

	public override async Task Execute()
    {
		GD.Print("Executing Variable Block");
		await Task.CompletedTask;
    }

	private void Execution()
	{
		
	}


}
