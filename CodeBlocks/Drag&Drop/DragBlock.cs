using Godot;
using System;

public partial class DragBlock : Control
{
	private CodeBlock _block;
    private Vector2 _offset;
    private BlockCanvas _blockCanvas;

    public DragBlock(CodeBlock block, Vector2 offset, BlockCanvas blockCanvas)
    {
        _block = block;
        _offset = offset;
        _blockCanvas = blockCanvas;
        AddChild(_block);
    }

    public override void _Process(double delta)
    {
        _block.GlobalPosition = GetGlobalMousePosition() - _offset;
    }

    public CodeBlock ApplyDrag()
    {
        CodeBlock closestBlock = GetClosestSnapTarget();
        if (closestBlock != null)
        {
            _blockCanvas.AttachBlock(_block, closestBlock);
            return _block;
        }
        else
        {
            _block.QueueFree();
            return null;
        }
    }

    private CodeBlock GetClosestSnapTarget()
    {
        float snapDistance = 50f;
		CodeBlock bestTarget = null;
        float bestDistance = float.MaxValue;

        foreach (CodeBlock candidate in _blockCanvas.GetBlocks())
        {
            float distance = (_block.GlobalPosition - candidate.GlobalPosition).Length();
            if (distance < snapDistance && distance < bestDistance)
            {
                bestDistance = distance;
                bestTarget = candidate;
            }
        }
        return bestTarget;
    }
}
