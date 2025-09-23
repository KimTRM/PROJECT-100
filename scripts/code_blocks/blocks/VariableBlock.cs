using Godot;
using GodotUtilities;
using System.Threading.Tasks;

[Scene]
public partial class VariableBlock : CodeBlock
{
	[Node("MarginContainer/HBoxContainer/VariableLabel")]
	public Label varType;

	[Node] private LineEdit varName;
	[Node] private DragAreaComponent dragAreaComponent;

	[Export] public string varNameStr;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		dragAreaComponent.DragStarted += () => { EmitSignalDragStarted(this); };
	}

	public override async Task Execute()
	{
		foreach (Control child in dragAreaComponent.GetChildren())
		{
			if (child is ExpressionBlock block)
			{
				await block.Execute();
				varNameStr = block.Value1;
				SetBlockValue(block.Value2);
			}
			else
			{
				varNameStr = varName.Text;
			}
		}

		await Task.CompletedTask;
	}
}
