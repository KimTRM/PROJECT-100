extends Control

@onready var login_panel: PanelContainer = $PanelContainer/LoginPanel
@onready var signup_panel: PanelContainer = $PanelContainer/SignupPanel

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func _on_to_signup_button_pressed() -> void:
	login_panel.hide()
	signup_panel.show()

func _on_signup_button_pressed() -> void:
	login_panel.show()
	signup_panel.hide()
