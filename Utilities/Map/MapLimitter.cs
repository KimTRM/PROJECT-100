using Godot;
using System;

public partial class MapLimitter : TileMapLayer
{
	public override void _Ready()
	{ 
		LevelManager.levelManager.ChangeTilemapBounds(GetTilemapBounds());
	}
	
	public Vector2[] GetTilemapBounds()
	{
		Vector2[] bounds = new Vector2[2];

		bounds[0] = GetUsedRect().Position * RenderingQuadrantSize;
		bounds[1] = GetUsedRect().End * RenderingQuadrantSize;

		return bounds;
	}
}
