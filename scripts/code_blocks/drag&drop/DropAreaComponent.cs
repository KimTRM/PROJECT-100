using Godot;

public partial class DropAreaComponent : MarginContainer
{
	[Signal] public delegate void DroppedBlockChangedEventHandler(CodeBlock block);
	[Signal] public delegate void BlockRemovedEventHandler(CodeBlock block);

	[Export]
	public Types.BlockType allowedBlockTypes = Types.BlockType.NONE;

	[Export]
	public CodeBlock DroppedBlock
	{
		get => _droppedBlock;
		set => SetDroppedBlock(value);
	}

	public CodeBlock GetDroppedBlock() => _droppedBlock;
	public bool HasBlock() => _droppedBlock != null;

	private CodeBlock _droppedBlock;
	private bool _setting;

	private void SetDroppedBlock(CodeBlock value)
	{
		if (_setting || _droppedBlock == value)
			return;

		_setting = true;

		var old = _droppedBlock;
		_droppedBlock = value;

		EmitSignal(SignalName.DroppedBlockChanged, _droppedBlock);

		if (old != null && _droppedBlock == null)
			EmitSignal(SignalName.BlockRemoved, old);

		_setting = false;
	}

	public void ReplaceBlock(CodeBlock newBlock)
	{
		if (_droppedBlock != null)
		{
			_droppedBlock.GetParent()?.RemoveChild(_droppedBlock);
			EmitSignal(SignalName.BlockRemoved, _droppedBlock);
		}

		_droppedBlock = newBlock;

		EmitSignal(SignalName.DroppedBlockChanged, _droppedBlock);
	}
}
