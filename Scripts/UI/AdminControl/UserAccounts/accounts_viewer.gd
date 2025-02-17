extends MarginContainer

@onready var question_number: Label = $VBoxContainer/ColorRect/HBoxContainer/MarginContainer/HBoxContainer/QuestionNumber

@onready var uid_column: VBoxContainer = $VBoxContainer/ScrollContainer/Rows/UIDColumn
@onready var firstname_column: VBoxContainer = $VBoxContainer/ScrollContainer/Rows/FirstnameColumn
@onready var lastname_column: VBoxContainer = $VBoxContainer/ScrollContainer/Rows/LastnameColumn
@onready var username_column: VBoxContainer = $VBoxContainer/ScrollContainer/Rows/UsernameColumn
@onready var password_column: VBoxContainer = $VBoxContainer/ScrollContainer/Rows/PasswordColumn
@onready var role_column: VBoxContainer = $VBoxContainer/ScrollContainer/Rows/RoleColumn

var user_datas: Array = []

func _ready() -> void:
	HttpManager.connect("request_completed", _on_accounts_received)
	HttpManager.queue_request(HttpManager.COMMANDS["GET_USER_ACCOUNT"])

func _on_accounts_received(response) -> void:
	user_datas = response
	test()
	load_user_ids()

func load_user_ids() -> void:
	for UID in user_datas:
		var UIDLebel: Label = Label.new()
		uid_column.add_child(UIDLebel)
		UIDLebel.text = UID["UserID"]
		
		var FirstnameLebel: Label = Label.new()
		firstname_column.add_child(FirstnameLebel)
		FirstnameLebel.text = UID["FirstName"]

		var LastnameLebel: Label = Label.new()
		lastname_column.add_child(LastnameLebel)
		LastnameLebel.text = UID["LastName"]

		var UsernameLebel: Label = Label.new()
		username_column.add_child(UsernameLebel)
		UsernameLebel.text = UID["UserName"]

		var PasswordLebel: Label = Label.new()
		password_column.add_child(PasswordLebel)
		PasswordLebel.text = UID["Password"]

		var RoleLebel: Label = Label.new()
		role_column.add_child(RoleLebel)
		RoleLebel.text = UID["Role"]
		
func test():
	question_number.text = str(user_datas.size())
