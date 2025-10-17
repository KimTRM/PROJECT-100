using Godot;

public partial class BlockSpawner : MarginContainer
{
	[Export] public BlockDefinition BlockResource;
	[Export] public CodeBlock SpawnedBlock;

	public override void _Ready()
	{
		SpawnBlock();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent
		&& mouseEvent.IsPressed()
		&& mouseEvent.ButtonIndex == MouseButton.Left
		&& GetChildren().Count <= 1
		&& GetGlobalRect().HasPoint(mouseEvent.GlobalPosition))
			SpawnBlock();
	}

	public void SpawnBlock()
	{
		CodeBlock newBlock = InitializeBlock(BlockResource.BlockType);
		AddChild(newBlock);

		SpawnedBlock = newBlock;

		SpawnedBlock.BlockDefinition = BlockResource;
		SpawnedBlock.templateEditor.TemplateName = BlockResource.BlockName;
	}

	private static CodeBlock InitializeBlock(BlockType block)
	{
		return ResourceLoader.Load<PackedScene>(GameConstants.BlockScenePath[block]).Instantiate<CodeBlock>();
	}
}
