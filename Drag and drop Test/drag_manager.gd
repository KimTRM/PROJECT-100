extends Node2D

func _input(event):
	if event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_LEFT:
		if event.pressed:
			print("Click")
			#raycast_check_for_block()
		else:
			print("Release")

func raycast_check_for_block():
	var space_state = get_world_2d().direct_space_state
	var paramiters = PhysicsPointQueryParameters2D.new()
	
	paramiters.from = get_global_mouse_position()
	paramiters.collide_with_areas = true
	paramiters.collision_mask = 1
	
	var result = space_state.intersect_ray(paramiters)
	print(result)

func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
