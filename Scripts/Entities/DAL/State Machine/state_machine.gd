class_name StateMachine extends Node

var states: Array[ States ] 
var pre_state: States
var current_state: States

# Called when the node enters the scene tree for the first time.
func _ready():
	process_mode = Node.PROCESS_MODE_DISABLED
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	ChangeState( current_state.Process(delta) )
	pass
	
func _physics_process(delta):
	ChangeState( current_state.Phisics(delta) )
	pass

func _unhandled_input(event):
	ChangeState( current_state.HandleInput(event))
	pass

func Initialize( _DAL: DAL) -> void:
	states = []
	
	for c in get_children():
		if c is States:
			states.append(c)
	
	if states.size() > 0:
		states[0].dal = _DAL
		ChangeState( states[0] )
		process_mode = Node.PROCESS_MODE_INHERIT
		

func ChangeState( new_state: States ) -> void:
	if new_state == null || new_state == current_state:
		return
	
	if current_state:
		current_state.Exit()
		
	pre_state = current_state
	current_state = new_state
	current_state.Enter()
