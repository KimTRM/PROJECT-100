using Godot;
using GodotUtilities;
using System.Threading.Tasks;

[Scene]
public partial class VariableBlock : CodeBlock
{
	public override async Task Execute()
	{
		var block = dropAreaComponent.DroppedBlock;
		if (dropAreaComponent.HasBlock())
		{
			await block.Execute();
			// SetBlockValue(block.Value2);
		}
		else
		{
			// SetBlockValue(???);
		}

		await Task.CompletedTask;
	}
}
