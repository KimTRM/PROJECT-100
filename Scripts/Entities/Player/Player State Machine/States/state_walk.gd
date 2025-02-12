class_name StateWalk extends State

@export var move_speed : float = 200.0
@onready var idle : State = $"../Idle"

func _ready():
	pass

# Enters State
func Enter() -> void:
	player.UpdateAnimation("walk")
	
	
# Exits State
func Exit() -> void:
	pass
	
func Process(_delta: float) -> State:
	if player.direction == Vector2.ZERO:
		return idle
	
	player.velocity = player.direction * move_speed
	
	if player.SetDirection():
		player.UpdateAnimation("walk")
	return null
	

func Phisics(_delta: float) -> State:
	return null

func HandleInput(_event: InputEvent) -> State:
	return null
	
