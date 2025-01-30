extends Control

@onready var quiz_editor = $MarginContainer/VBoxContainer/HBoxContainer/Content/QuizEditor

func _ready():
	pass

func _on_accounts_pressed():
	quiz_editor.hide()

func _on_quiz_editor_pressed():
	quiz_editor.show()

func _on_scores_pressed():
	quiz_editor.hide()
