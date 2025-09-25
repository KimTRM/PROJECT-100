using Godot;
using System;

public partial class BlockSpawner : MarginContainer
{
	[Export] public CodeBlock BlockToSpawn;

	public override void _Ready()
	{
		BlockToSpawn ??= GetChildOrNull<CodeBlock>(0);

		if (BlockToSpawn == null)
		{
			GD.PushWarning($"{Name}: No template CodeBlock found.");
			return;
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			bool nowInside = GetGlobalRect().HasPoint(mouseEvent.GlobalPosition);
			if (nowInside && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.IsPressed())
			{
				if (GetChildren().Count == 0)
					SpawnBlock();
			}

			GD.Print(GetTree().GetNodesInGroup("CodeBlock").Count);
		}
	}

	public void SpawnBlock()
	{
		BlockToSpawn.StartDrag();

		var newBlock = BlockToSpawn.Duplicate() as CodeBlock;

		BlockToSpawn = newBlock;

		AddChild(newBlock);
	}
}
