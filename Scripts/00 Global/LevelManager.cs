using Godot;
using System;

public partial class LevelManager : Node
{
	public static LevelManager levelManager {get; private set;}

	public override void _Ready()
	{
		levelManager = this;
	}

	public Vector2[] currentTilemapBounds;
	
	[Signal]
	public delegate void TileMapBoundsChangedEventHandler(Vector2[] bounds);

	public void ChangeTilemapBounds(Vector2[] bounds)
	{
		currentTilemapBounds = bounds;
		EmitSignal("TileMapBoundsChanged", bounds);
	}
}
