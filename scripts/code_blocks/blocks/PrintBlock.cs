using Godot;
using GodotUtilities;
using System.Threading.Tasks;

[Scene]
public partial class PrintBlock : CodeBlock
{
	[Node] private LineEdit Value;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override async Task Execute()
	{
		if (dropAreaComponent.HasBlock())
		{
			await dropAreaComponent.DroppedBlock.Execute();
			GD.Print("Print Block Value: ", dropAreaComponent.DroppedBlock.GetBlockValue());
			// console.OnCommandEntered(block.BlockValue.ToString());
		}
		else
		{
			GD.Print("Print Block Value: ", Value.Text);
			// console.OnCommandEntered(value.Text);
		}

		await Task.CompletedTask;
	}
}
