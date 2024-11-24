using Godot;
using System;

public partial class PlayerCamera : Camera2D
{
    public override void _Ready()
    {
        var levelManager = GetNode<LevelManager>("/root/LevelManager");
        levelManager.Connect(
            nameof(LevelManager.TileMapBoundsChanged),
            Callable.From((Vector2[] bounds) => UpdateLimits(bounds))
        );
        
        UpdateLimits(levelManager.CurrentTileMapBounds);
    }

    public void UpdateLimits(Vector2[] bounds)
    {
        if (bounds == null || bounds.Length == 0) return;
        
        LimitLeft = (int)bounds[0].X;
        LimitTop = (int)bounds[0].Y;
        LimitRight = (int)bounds[1].X;
        LightMask = (int)bounds[1].Y;
    }
}