class_name Walk extends States

@export var move_speed : float = 200.0
@onready var idle : States = $"../Idle"

func _ready():
	pass

# Enters State
func Enter() -> void:
	dal.UpdateAnimation("walk")
	
# Exits State
func Exit() -> void:
	pass
	
func Process(_delta: float) -> States:
	if dal.direction == Vector2.ZERO:
		return idle
	
	dal.velocity = dal.direction * move_speed
	
	if dal.SetDirection():
		dal.UpdateAnimation("walk")
	return null
	

func Phisics(_delta: float) -> States:
	return null

func HandleInput(_event: InputEvent) -> States:
	return null
	
