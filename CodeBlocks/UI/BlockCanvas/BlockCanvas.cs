using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class BlockCanvas : MarginContainer
{
    [Node ("WindowContainer/Overlay/MarginContainer/ZoomButtons/ZoomButton")]
    private Button ZoomButton;

	[Node ("WindowContainer/Window")]
	public Control Window;

    private float zoomFactor = 1.0f;
    private float zoomStep = 0.1f;
    private float minZoom = 0.5f;
    private float maxZoom = 2.0f;

    private bool isDragging = false;
    private Vector2 dragStartPos;
    private Vector2 controlStartPos;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            // Zoom in/out
            if (mouseEvent.Pressed)
            {
                if (mouseEvent.ButtonIndex == MouseButton.WheelUp)
                {
                    Zoom(zoomFactor + zoomStep);
                }
                else if (mouseEvent.ButtonIndex == MouseButton.WheelDown)
                {
                    Zoom(zoomFactor - zoomStep);
                }
                // Start dragging
                else if (mouseEvent.ButtonIndex == MouseButton.Middle)
                {
                    isDragging = true;
                    dragStartPos = GetViewport().GetMousePosition();
                    controlStartPos = Window.Position;
                }
            }
            else if (!mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Middle)
            {
                isDragging = false;
            }
        }

        // Handle dragging
        if (@event is InputEventMouseMotion motionEvent && isDragging)
        {
            Vector2 mouseDelta = GetViewport().GetMousePosition() - dragStartPos;
            Window.Position = controlStartPos + mouseDelta;
        }
    }

    public void _on_zoom_in_button_pressed()
    {
        Zoom(zoomFactor + zoomStep);
    }

    public void _on_zoom_button_pressed()
    {
        Zoom(1.0f);
    }

    public void _on_zoom_out_button_pressed()
    {
        Zoom(zoomFactor - zoomStep);
    }

    private void Zoom(float newZoom)
    {
        zoomFactor = Mathf.Clamp(newZoom, minZoom, maxZoom);
        Window.Scale = new Vector2(zoomFactor, zoomFactor);

        ZoomButton.Text = $"{zoomFactor:F1}x";
    }
}
