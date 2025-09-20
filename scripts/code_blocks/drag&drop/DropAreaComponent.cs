using Godot;
using Godot.Collections;

public partial class DropAreaComponent : MarginContainer
{
	[Signal] public delegate void DragStartedEventHandler(CodeBlock block);
	[Signal] public delegate void DroppedBlockChangedEventHandler(CodeBlock block);
	[Signal] public delegate void BlockRemovedEventHandler(CodeBlock block);

	[Export] private Types.BlockType allowedBlockTypes = Types.BlockType.STATEMENT;

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

	[Export] private Variant variantType;

	public override void _Ready()
	{
		// if (dropZone == null)
		// {
		// 	GD.PrintErr("Droppable area is not set for DropAreaComponent." + GetParent().Name);
		// 	return;
		// }

		// dropZone.MouseEntered += OnMouseEntered;
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

	private void OnMouseEntered()
	{
		// if (dropContainer == null)
		// 	dropContainer = this;

		// DragManager.SetDroppableTarget(dropContainer);
	}
}
