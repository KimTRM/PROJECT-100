class_name CodeBlock extends Control

@export_category("Drag Settings")
@export var dragging = false
@export var snap_target = null
@export var offset = Vector2.ZERO
@export var parent_block = null

var VarCategory: String
var VarType: String
var VarName: String
var VarValue

func _gui_input(event) -> void:
	if event is InputEventMouseButton:
		if event.pressed and event.button_index == MOUSE_BUTTON_LEFT:
			start_drag()
		elif !event.pressed:
			stop_drag()

func _process(delta: float) -> void:
	if dragging:
		global_position = get_global_mouse_position() + offset

func start_drag() -> void:
	dragging = true
	offset = global_position - get_global_mouse_position()
	if parent_block:
		parent_block = null
	
	move_to_front()

func stop_drag() -> void:
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

signal executed  # Signal when block finishes execution

var next_blocks = []  # Array of connected blocks
var variables = {}    # Shared variable storage
@onready var block_manager = get_tree().get_first_node_in_group("block_manager")

func execute():
	print("Executing block:", name)
	executed.emit()
	
	# Execute the next block(s)
	for block in next_blocks:
		await get_tree().create_timer(0.5).timeout  # Small delay
		block.execute()
	
func add_next_block(block):
	if block and block not in next_blocks:
		next_blocks.append(block)
