using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	[Export] public Vector2[] currentTilemapBounds;
	[Signal] public delegate void TileMapBoundsChangedEventHandler(Vector2[] bounds);

	public override void _Ready()
	{
		Instance = this;
	}
	
	public void ChangeTilemapBounds(Vector2[] bounds)
	{
		currentTilemapBounds = bounds;
		EmitSignal("TileMapBoundsChanged", bounds);
	}
}
