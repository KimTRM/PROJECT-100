class_name CodeBlock extends Control

@export_category("Drag Settings")
@export var is_draggable = true
var is_dragging = false
@export var mouse_offset = 0
@export var delay = 0

var VarCategory: String
var VarType: String
var VarName: String
var VarValue

func SetValue():
	pass

#  -- DRAGGING --
func drag_animation():
	if is_dragging:
		var tween = get_tree().create_tween()
		tween.tween_property(self, "position", get_global_mouse_position() - mouse_offset, delay)

func drag(event):
	if event is InputEventMouseButton and event.button_index == MOUSE_BUTTON_LEFT and is_draggable:
		if event.pressed:
			if get_rect().has_point(event.position):
				print("dragging")
				is_dragging = true
				mouse_offset = get_global_mouse_position() - global_position
		else:
			is_dragging = false