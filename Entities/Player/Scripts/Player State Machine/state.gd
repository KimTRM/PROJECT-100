class_name State extends Node

# Stores a Reference
static var player: Player

func _ready():
	pass # Replace with function body.

# Enters State
func Enter() -> void:
	pass
	

# Exits State
func Exit() -> void:
	pass
	
func Process(_delta: float) -> State:
	return null
	
func Phisics(_delta: float) -> State:
	return null
	
func HandleInput(_event: InputEvent) -> State:
	return null
	