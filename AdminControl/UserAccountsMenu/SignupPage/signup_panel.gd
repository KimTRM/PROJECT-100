extends PanelContainer

@onready var first_name: LineEdit = $MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/HBoxContainer/UsernamePanel2/MarginContainer/VBoxContainer/FirstName
@onready var last_name: LineEdit = $MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/HBoxContainer/UsernamePanel3/MarginContainer/VBoxContainer/LastName
@onready var user_name: LineEdit = $MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/UsernamePanel/MarginContainer/VBoxContainer/UserName
@onready var password: LineEdit = $MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/PasswordPanel/MarginContainer/VBoxContainer/Password
@onready var confirm_password: LineEdit = $MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/PasswordPanel2/MarginContainer/VBoxContainer/ConfirmPassword

@onready var error_message: Label = $MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/ErrorMessage

func error_trapping() -> void:
	first_name.modulate = Color.WHITE if first_name.text != "" else Color.RED
	last_name.modulate = Color.WHITE if last_name.text != "" else Color.RED
	user_name.modulate = Color.WHITE if user_name.text != "" else Color.RED
	password.modulate = Color.WHITE if password.text != "" else Color.RED
	confirm_password.modulate = Color.WHITE if confirm_password.text != "" else Color.RED

func can_save_data() -> bool:
	error_trapping()
	if first_name.text != "" and last_name.text != "" and user_name.text != "" and password.text != "":
		error_message.hide()
		return true
	else:
		error_message.show()
		error_message.text = "Please fill out missing fields"
	return false

func save_data() -> void:
	if can_save_data():
		var data:Dictionary = {
			"UserID" : HttpManager.generate_id(), 
			"FirstName" : first_name.text, 
			"LastName" : last_name.text, 
			"UserName" : user_name.text, 
			"Password" : password.text, 
			"Role" : "Student"
		}
		HttpManager.queue_request(HttpManager.COMMANDS["ADD_USER_ACCOUNT"], data)

func _on_signup_button_pressed() -> void:
	save_data()
