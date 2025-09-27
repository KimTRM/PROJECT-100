using Godot;
using GodotUtilities;
using System.Threading.Tasks;

[Scene]
public partial class VariableBlock : CodeBlock
{
	[Node] private LineEdit varName;

	[Export] public string varNameStr;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override async Task Execute()
	{
		var block = dropAreaComponent.DroppedBlock;
		if (dropAreaComponent.HasBlock())
		{
			await block.Execute();
			// varNameStr = block.Value1;
			// SetBlockValue(block.Value2);
		}
		else
		{
			varNameStr = varName.Text;
			SetBlockValue(varNameStr);
		}

		await Task.CompletedTask;
	}
}
