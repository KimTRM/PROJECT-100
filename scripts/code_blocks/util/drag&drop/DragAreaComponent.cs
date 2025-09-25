using Godot;

public partial class DragAreaComponent : Control
{
	[Signal] public delegate void DragStartedEventHandler();

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			bool nowInside = GetGlobalRect().HasPoint(mouseEvent.GlobalPosition);
			if (nowInside && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.IsPressed())
				EmitSignalDragStarted();
		}
	}
}
