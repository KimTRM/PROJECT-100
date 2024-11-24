using Godot;

public partial class LevelManager : Node
{
    public Vector2[] CurrentTileMapBounds { get; private set; }

    [Signal]
    public delegate void TileMapBoundsChangedEventHandler(Vector2[] bounds);

    public void ChangeTileMapBounds(Vector2[] bounds)
    {
        CurrentTileMapBounds = bounds;
        EmitSignal(nameof(TileMapBoundsChangedEventHandler), bounds);
    }
}