extends Control

var start_block  # The first block in the sequence

func _ready() -> void:
	execute_blocks()

func execute_blocks():
	if start_block:
		print("Starting execution from:", start_block.name)
		start_block.execute() # Move to the next block
