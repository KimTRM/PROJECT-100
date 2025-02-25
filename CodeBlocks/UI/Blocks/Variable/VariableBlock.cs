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

	[Export] public string varNameStr;
	[Export] public Variant varValue;

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

    public override async Task Execute()
    {
		foreach (Control child in varContainer.GetChildren())	
		{
			if (child is ExpressionBlock block)
			{
				await block.Execute();
				varNameStr = block.Value1;
				varValue = block.Value2;
				GD.Print($"{varNameStr} = {varValue}");
			}
			else
			{
				varNameStr = varName.Text;
			}
		}

		await Task.CompletedTask;
    }
}
