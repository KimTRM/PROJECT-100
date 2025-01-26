class_name BodyContainer extends VBoxContainer

var code_blocks: Array [CodeBlock]

# Called when the node enters the scene tree for the first time.
func _ready():
	pass

func Initialize() -> void:
	code_blocks = []
	
	for c in get_children():
		if c is CodeBlock:
			code_blocks.append(c)

func _can_drop_data(at_position, data):
	return true

func _drop_data(at_position, data):
	pass