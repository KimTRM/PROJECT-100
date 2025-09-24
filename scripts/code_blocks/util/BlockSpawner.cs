using Godot;
using System;

public partial class BlockSpawner : VBoxContainer
{
	[Export] public CodeBlock BlockToSpawn;

	public override void _Ready()
	{
		StartupInitialize();
	}

	public override void _Process(double delta)
	{
		if (GetChildOrNull<CodeBlock>(1) != null && GetChildCount() > 1)
		{
			GetChildOrNull<CodeBlock>(1).QueueFree();
		}
	}

	private void StartupInitialize()
	{
		BlockToSpawn ??= GetChildOrNull<CodeBlock>(0);

		if (BlockToSpawn == null)
		{
			GD.PushWarning($"{Name}: No template CodeBlock found.");
			return;
		}

		BlockToSpawn.DragStarted += SpawnBlock;
	}

	public void SpawnBlock(CodeBlock dragged)
	{
		var newBlock = BlockToSpawn.Duplicate() as CodeBlock;

		var oldBlock = BlockToSpawn;
		oldBlock.DragStarted -= SpawnBlock;

		BlockToSpawn = newBlock;
		newBlock.DragStarted += SpawnBlock;

		AddChild(newBlock);
	}
}
