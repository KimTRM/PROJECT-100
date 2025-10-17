using Godot;
using GodotUtilities;
using Godot.Collections;

[Scene]
public partial class BlockPicker : MarginContainer
{
	[Export] private Array<BlockDefinition> BlockDefinitions = new();

	[Node] public VBoxContainer CodeBlockContainer;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		PopulateBlocks();
	}

	private void PopulateBlocks()
	{
		foreach (var blockDef in BlockDefinitions)
		{
			if (blockDef == null)
			{
				GD.PushWarning($"{Name}: One of the BlockDefinitions is null.");
				continue;
			}

			var blockSpawner = new BlockSpawner
			{
				BlockResource = blockDef
			};
			CodeBlockContainer.AddChild(blockSpawner);
		}
	}
}
