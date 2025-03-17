using Godot;
using System;

[GlobalClass]
public partial class MapContainerComponent : Node2D
{
	[Export]
	private TileMapLayer layer;

	public override void _Ready()
	{ 
		GameManager.Instance.isGamePuasable = true;
		GameManager.Instance.ChangeTilemapBounds(GetTilemapBounds());
	}
	
	public Vector2[] GetTilemapBounds()
	{
		Vector2[] bounds =
        [
            layer.GetUsedRect().Position * layer.RenderingQuadrantSize,
            layer.GetUsedRect().End * layer.RenderingQuadrantSize,
        ];
        return bounds;
	}
}
