using System.Collections.Generic;
using System.Linq;
using Godot;
using GodotUtilities;

[Scene]
public partial class BlockCanvas : MarginContainer
{
    [Node("WindowContainer/Overlay/MarginContainer/ZoomButtons/ZoomButton")]
    private Button ZoomButton;

    [Node("WindowContainer/Window")]
    public Control Window;

    private float zoomFactor = 1.0f;
    private float zoomStep = 0.1f;
    private float minZoom = 0.5f;
    private float maxZoom = 2.0f;

    private bool isDragging = false;
    private Vector2 dragStartPos;
    private Vector2 controlStartPos;

    private List<CodeBlock> _codeBlocks = new List<CodeBlock>();

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

    public CodeBlock[] GetBlocks()
    {
        foreach (Control child in Window.GetChildren())
        {
            if (child is CodeBlock block)
            {
                _codeBlocks.Add(block);
                return _codeBlocks.ToArray();
            }
        }

        return null;
    }

    public void AddBlock(CodeBlock block)
    {
        Window.AddChild(block);
        _codeBlocks.Add(block);
    }

    public void RemoveBlock(CodeBlock block)
    {
        _codeBlocks.Remove(block);
        block.QueueFree();
    }

    public CodeBlock GetClosestBlock(Vector2 position, float snapThreshold = 50f)
    {
        return _codeBlocks
            .Where(block => block != null && block.Visible)
            .OrderBy(block => block.Position.DistanceTo(position))
            .FirstOrDefault(block => block.Position.DistanceTo(position) < snapThreshold);
    }

    public void AttachBlock(CodeBlock block, CodeBlock targetBlock)
    {
        if (targetBlock == null) return;

        block.Position = targetBlock.Position + new Vector2(0, targetBlock.Size.Y + 10); // Snap below target
        _codeBlocks.Remove(block);
        _codeBlocks.Insert(_codeBlocks.IndexOf(targetBlock) + 1, block);
    }
}
