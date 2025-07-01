using System.Collections.Generic;
using Godot;
using GodotUtilities;

[Scene]
public partial class BlockCanvas : MarginContainer
{
    [Node("WindowContainer/Overlay/MarginContainer/ZoomButtons/ZoomButton")]
    private Button ZoomButton;
    [Node("WindowContainer/Window")]
    public Control Window;

    [Export] private DragManager dragManager;

    [Export]
    private NodePath ConsolePath;
    public Console console;

    private float _zoomFactor = 1.0f;
    private float _zoomStep = 0.1f;
    private float _minZoom = 0.1f;
    private float _maxZoom = 5.0f;

    private bool _isDragging = false;
    private Vector2 _dragStartPos;
    private Vector2 _controlStartPos;

    private List<CodeBlock> _codeBlocks = [];

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        console = GetNode<Console>(ConsolePath);
        MouseEntered += OnMouseEntered;
    }

    public override void _GuiInput(InputEvent @event)
    {
        ZoomCanvas(@event);
        DragCanvas(@event);
    }

    private void ZoomCanvas(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            // Zoom in/out
            if (mouseEvent.Pressed)
            {
                if (mouseEvent.ButtonIndex == MouseButton.WheelUp)
                {
                    AdjustZoom(_zoomFactor + _zoomStep);
                }
                else if (mouseEvent.ButtonIndex == MouseButton.WheelDown)
                {
                    AdjustZoom(_zoomFactor - _zoomStep);
                }
                // Start dragging
                else if (mouseEvent.ButtonIndex == MouseButton.Middle)
                {
                    _isDragging = true;
                    _dragStartPos = GetViewport().GetMousePosition();
                    _controlStartPos = Window.Position;
                }
            }
            else if (!mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Middle)
            {
                _isDragging = false;
            }
        }
    }

    private void DragCanvas(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motionEvent && _isDragging)
        {
            Vector2 mouseDelta = GetViewport().GetMousePosition() - _dragStartPos;
            Window.Position = Window.Position.Lerp(_controlStartPos + mouseDelta, 25.0f * (float)GetProcessDeltaTime());
        }
    }

    private void AdjustZoom(float newZoom)
    {
        _zoomFactor = Mathf.Clamp(newZoom, _minZoom, _maxZoom);
        Window.Scale = new Vector2(_zoomFactor, _zoomFactor);

        // Adjust the size of the window towards the mouse position when resizing
        Vector2 mousePos = GetViewport().GetMousePosition();
        Vector2 newSize = Window.Size * _zoomFactor;
        Vector2 offset = mousePos - Window.GlobalPosition;
        Window.GlobalPosition = mousePos - offset * _zoomFactor / _zoomFactor;

        ZoomButton.Text = $"{_zoomFactor:F1}x";
    }

    private void OnMouseEntered()
    {
        dragManager.SetDroppableTarget(Window);
    }

    private void _on_zoom_in_button_pressed()
    {
        AdjustZoom(_zoomFactor + _zoomStep);
    }
    private void _on_zoom_button_pressed()
    {
        AdjustZoom(1.0f);
    }
    private void _on_zoom_out_button_pressed()
    {
        AdjustZoom(_zoomFactor - _zoomStep);
    }

    private async void _on_execute_pressed()
    {
        foreach (Control child in Window.GetChildren())
        {
            if (child is CodeBlock block)
            {
                await block.Execute();
            }
        }
    }
}
