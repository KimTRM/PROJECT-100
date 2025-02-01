class_name CodeBlock extends Control

@export_category("Drag Settings")
@export var dragging = false
@export var snap_target = null
@export var offset = Vector2.ZERO
@export var parent_block = null

signal executed

var VarCategory: String
var VarType: String
var VarName: String
var VarValue

func _gui_input(event):
	if event is InputEventMouseButton:
		if event.pressed and event.button_index == MOUSE_BUTTON_LEFT:
			start_drag()
		elif !event.pressed:
			stop_drag()

func _process(_delta):
	if dragging:
		global_position = get_global_mouse_position() + offset

func start_drag():
	dragging = true
	offset = global_position - get_global_mouse_position()
	if parent_block:
		parent_block = null
	
	move_to_front()

func stop_drag():
	dragging = false
	if snap_target:
		snap_to_target()

func snap_to_target():
	global_position = snap_target.global_position + Vector2(0, 50) # Stack vertically
	parent_block = snap_target

func _on_Area2D_body_entered(body):
	if body.is_in_group("snap_zone"):
		snap_target = body

func _on_Area2D_body_exited(body):
	if body == snap_target:
		snap_target = null
