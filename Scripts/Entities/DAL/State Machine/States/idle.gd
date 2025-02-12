class_name Idle extends States

@onready var walk : States = $"../Walk"

# Enters State
func Enter() -> void:
	dal.UpdateAnimation("idle")

# Exits State
func Exit() -> void:
	pass
	
func Process(_delta: float) -> States:
	if dal.direction != Vector2.ZERO:
		return walk
	dal.velocity = Vector2.ZERO
	return null
	
func Phisics(_delta: float) -> States:
	return null
	
func HandleInput(_event: InputEvent) -> States:
	return null
	
