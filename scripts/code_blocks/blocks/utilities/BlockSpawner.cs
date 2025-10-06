using Godot;

public partial class BlockSpawner : MarginContainer
{
	[Export] private BaseBlock BlockResource;
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
			if (GetGlobalRect().HasPoint(mouseEvent.GlobalPosition)
			&& mouseEvent.ButtonIndex == MouseButton.Left
			&& mouseEvent.IsPressed() && GetChildren().Count <= 1)
				SpawnBlock();
	}

	public void SpawnBlock()
	{
		BlockToSpawn.StartDrag();

		var newBlock = BlockToSpawn.Duplicate() as CodeBlock;

		BlockToSpawn = newBlock;

		AddChild(newBlock);
	}
}
