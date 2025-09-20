using Godot;

public partial class DropAreaComponent : MarginContainer
{
	[Signal] public delegate void DragStartedEventHandler(CodeBlock block);
	[Signal] public delegate void DroppedBlockChangedEventHandler(CodeBlock block);
	[Signal] public delegate void BlockRemovedEventHandler(CodeBlock block);

	[Export] private Types.BlockType allowedBlockTypes = Types.BlockType.STATEMENT;
	[Export] private Variant variantType;

	[Export]
	private CodeBlock droppedBlock
	{
		get => droppedBlock;
		set
		{
			droppedBlock = value;
			EmitSignalDroppedBlockChanged(droppedBlock);

			if (value == null)
			{
				EmitSignalBlockRemoved(droppedBlock);
			}
		}
	}

	public CodeBlock GetDroppedBlock()
	{
		return droppedBlock;
	}

	public bool HasBlock()
	{
		return droppedBlock != null;
	}

	public void ReplaceBlock(CodeBlock newBlock)
	{
		var oldBlock = droppedBlock;

		if (oldBlock != null)
		{
			oldBlock.GetParent().RemoveChild(oldBlock);
			EmitSignalBlockRemoved(oldBlock);
		}

		droppedBlock = newBlock;
		AddChild(droppedBlock);
	}
}
