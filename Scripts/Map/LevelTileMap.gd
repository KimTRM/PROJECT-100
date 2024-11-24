class_name LevelTileMap extends TileMapLayer

func _ready() -> void:
	LevelManager.ChangeTilemapBounds(GetTilemapBounds())
	pass

func GetTilemapBounds() -> Array[Vector2]:
	var bounds: Array[Vector2] = []
	var used_rect = get_used_rect()
	
	var start_position = map_to_local(used_rect.position)
	var end_position = map_to_local(used_rect.end)
	
	bounds.append(start_position)
	bounds.append(end_position)
	
	return bounds
