class_name StateIdle extends State

@onready var walk: State = $"../Walk"
# @onready var interactable_finder: Area2D = $"../../Direction/InteractableFinder"

# Enters State
func Enter() -> void:
	player.UpdateAnimation("idle")

# Exits State
func Exit() -> void:
	pass
	
func Process(_delta: float) -> State:
	if player.direction != Vector2.ZERO:
		return walk
	player.velocity = Vector2.ZERO
	return null
	
func Phisics(_delta: float) -> State:
	return null
	
func HandleInput(_event: InputEvent) -> State:
	#if Input.is_action_just_pressed("interact"):
		#var interactable = interactable_finder.get_overlapping_areas()
		#if interactable.size() > 0:
			#interactable[0].action()
	return null
