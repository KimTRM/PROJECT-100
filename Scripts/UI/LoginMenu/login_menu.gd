extends Control

@onready var login_panel: PanelContainer = $PanelContainer/LoginPanel
@onready var signup_panel: PanelContainer = $PanelContainer/SignupPanel

var user_datas: Array = []

func _ready() -> void:
	HttpManager.connect("request_completed", _on_accounts_received)
	HttpManager.queue_request(HttpManager.COMMANDS["GET_USER_ACCOUNT"])

func _on_accounts_received(response) -> void:
	user_datas = response

func _on_to_signup_button_pressed() -> void:
	signup_panel.show()

func _on_login_button_pressed() -> void:
	var inputed_username = login_panel.username.text
	var inputed_password = login_panel.password.text
	
	var admin_username: String = user_datas[0]["UserName"]
	var admin_password: String = user_datas[0]["Password"]
	
	if inputed_username == admin_username and inputed_password == admin_password:
		get_tree().change_scene_to_file("res://Scenes/UI/AdminControl/AdminPage.tscn")
	else:
		get_tree().change_scene_to_file("res://Scenes/UI/StartingScreen/StartingMenu.tscn")
