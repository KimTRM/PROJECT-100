extends Control

var start_block  # The first block in the sequence

func _ready() -> void:
	execute_blocks()

func execute_blocks():
	var block = start_block
	while block:
		block.execute()
		block = block.parent_block  # Move to the next block
