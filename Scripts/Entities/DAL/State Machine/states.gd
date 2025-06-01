class_name States extends Node

# Stores a Reference
static var dal: DAL

func _ready():
	
	pass # Replace with function body.


# Enters State
func Enter() -> void:
	pass
	

# Exits State
func Exit() -> void:
	pass
	

func Process(_delta: float) -> States:
	return null
	

func Phisics(_delta: float) -> States:
	return null
	

func HandleInput(_event: InputEvent) -> States:
	return null
