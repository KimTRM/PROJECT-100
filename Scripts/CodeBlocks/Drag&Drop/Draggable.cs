using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class Draggable : Control
{
    public enum DragAction { None, Place, Remove }

    private CodeBlock _block;
    private string _blockScope;
    private BlockCanvas _blockCanvas;
    private Control _previewBlock;
    private Array<Node> _snapPoints = new();
    private Array<Rect2> _deleteAreas = new();
    private DragAction _action = DragAction.None;
    // private SnapPoint _targetSnapPoint;

    public Draggable(CodeBlock block, string blockScope, Vector2 offset, BlockCanvas blockCanvas)
    {
        // GD.Assert(block.GetParent() == null);
        AddChild(block);
        block.Position = -offset;

        _block = block;
        _blockScope = blockScope;
        _blockCanvas = blockCanvas;
    }

    public void SetSnapPoints(Array<Node> snapPoints)
    {
        // _snapPoints =
    }

    public void AddDeleteArea(Rect2 deleteArea)
    {
        _deleteAreas.Add(deleteArea);
    }

    public void UpdateDragState()
    {
        GlobalPosition = GetGlobalMousePosition();

        Scale = _blockCanvas.IsMouseOver() ? new Vector2(_blockCanvas.GetZoomFactor(), _blockCanvas.GetZoomFactor()) : Vector2.One;

        foreach (var rect in _deleteAreas)
        {
            if (rect.HasPoint(GetGlobalMousePosition()))
            {
                _action = DragAction.Remove;
                // _targetSnapPoint = null;
                return;
            }
        }

        _action = DragAction.Place;
        // _targetSnapPoint = FindClosestSnapPoint();
    }

    public CodeBlock ApplyDrag()
    {
        UpdateDragState();

        if (_action == DragAction.Place)
        {
            PlaceBlock();
            return _block;
        }
        else if (_action == DragAction.Remove)
        {
            RemoveBlock();
            return null;
        }
        else
        {
            return null;
        }
    }

    private void RemoveBlock()
    {
        // _targetSnapPoint = null;
        _block.QueueFree();
    }

    private void PlaceBlock()
    {
        Rect2 canvasRect = _blockCanvas.GetGlobalRect();
        Vector2 position = _block.GlobalPosition - canvasRect.Position;

        RemoveChild(_block);

        // if (_targetSnapPoint != null)
        // {
        //     var orphanedBlock = _targetSnapPoint.InsertSnappedBlock(_block);
        //     if (orphanedBlock != null)
        //     {
        //         _blockCanvas.ArrangeBlock(orphanedBlock, GetSnapBlock());
        //     }
        // }
        // else
        // {
        //     _blockCanvas.AddBlock(_block, position);
        // }

        // _targetSnapPoint = null;
    }

    private bool SnapsTo(Node node)
    {
        var snapPoint = node as DropAreaComponent;
        if (snapPoint == null)
        {
            GD.PushError($"Warning: node {node} is not of class SnapPoint.");
            return false;
        }

        if (!_blockCanvas.IsAncestorOf(snapPoint))
            return false;

        // Add type and scope checks here as needed

        if (snapPoint.IsAncestorOf(_block))
            return false;

        // Add top block and scope checks here as needed

        return true;
    }

    // private SnapPoint FindClosestSnapPoint()
    // {
    //     SnapPoint closestSnapPoint = null;
    //     float closestDistance = float.MaxValue;
    //     foreach (SnapPoint snapPoint in _snapPoints)
    //     {
    //         float distance = GetDistanceToSnapPoint(snapPoint);
    //         if (distance > Constants.MinimumSnapDistance * _blockCanvas.Zoom)
    //             continue;
    //         if (closestSnapPoint == null || distance < closestDistance)
    //         {
    //             closestSnapPoint = snapPoint;
    //             closestDistance = distance;
    //         }
    //     }
    //     return closestSnapPoint;
    // }

    // private float GetDistanceToSnapPoint(SnapPoint snapPoint)
    // {
    //     Vector2 fromGlobal = _block.GlobalPosition;
    //     return fromGlobal.DistanceTo(snapPoint.GlobalPosition);
    // }

    // private Block GetSnapBlock()
    // {
    //     return BlockTreeUtil.GetParentBlock(_targetSnapPoint);
    // }
}