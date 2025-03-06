using Godot;
using System;

public partial class DragDropArea : Control
{
	[Signal]
    public delegate void DragStartedEventHandler(Vector2 offset);

    // private static readonly Constants Constants = GD.Load<Constants>("res://addons/block_code/ui/constants.gd");

    [Export]
    public bool DragOutside = false;

    private Vector2 _dragStartPosition = Vector2.Inf;

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is not InputEventMouseButton buttonEvent)
            return;

        if (buttonEvent.ButtonIndex != MouseButton.Left)
            return;

		if (buttonEvent.Pressed)
        {
            _dragStartPosition = buttonEvent.GlobalPosition;
        }
        else
        {
            _dragStartPosition = Vector2.Inf;
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is not InputEventMouseMotion motionEvent)
            return;

        if (_dragStartPosition == Vector2.Inf)
            return;

        if (DragOutside && GetGlobalRect().HasPoint(motionEvent.GlobalPosition))
            return;

        // if (_dragStartPosition.DistanceTo(motionEvent.GlobalPosition) < Constants.MINIMUM_DRAG_THRESHOLD)
        //     return;

        GD.Print("drag started");

        GetViewport().SetInputAsHandled();
        EmitSignal(SignalName.DragStarted, _dragStartPosition - motionEvent.GlobalPosition);
        _dragStartPosition = Vector2.Inf;
    }
}
