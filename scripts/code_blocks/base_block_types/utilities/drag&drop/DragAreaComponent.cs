using Godot;

public partial class DragAreaComponent : Control
{
	[Signal] public delegate void DragStartedEventHandler();

	[Export] private bool isDraggable;

	public override void _Ready()
	{
		MouseEntered += () => isDraggable = true;
		MouseExited += () => isDraggable = false;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			bool nowInside = GetGlobalRect().HasPoint(mouseEvent.GlobalPosition);
			if (isDraggable && nowInside && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.IsPressed())
				EmitSignalDragStarted();
		}
	}
}
