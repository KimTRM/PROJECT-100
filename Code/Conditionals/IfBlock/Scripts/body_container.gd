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
