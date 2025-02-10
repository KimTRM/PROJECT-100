using Godot;
using System;

public partial class MapLimiter : Node
{
	[Export]
	private TileMapLayer layer;

	public override void _Ready()
	{ 
		GameManager.Instance.ChangeTilemapBounds(GetTilemapBounds());
	}
	
	public Vector2[] GetTilemapBounds()
	{
		Vector2[] bounds = new Vector2[2];

		bounds[0] = layer.GetUsedRect().Position * layer.RenderingQuadrantSize;
		bounds[1] = layer.GetUsedRect().End * layer.RenderingQuadrantSize;

		return bounds;
	}
}
