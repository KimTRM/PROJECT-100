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
	public bool HasBlock() => GetChildren().Count > 0;

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
}
