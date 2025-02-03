extends Control

@onready var content: ColorRect = $MarginContainer/VBoxContainer/HBoxContainer/Content

var QUIZ_EDITOR = load("res://AdminControl/QuizEditor/QuizEditor.tscn")
var quiz

const ACCOUNTS_VIEWER = preload("res://AdminControl/UserAccountsViewer/AccountsViewer.tscn")
var acc

func _ready():
	quiz = QUIZ_EDITOR.instantiate()
	acc = ACCOUNTS_VIEWER.instantiate()
	
	acc = ACCOUNTS_VIEWER.instantiate()
	content.add_child(acc)

func _on_accounts_pressed():
	if quiz != null:
		quiz.queue_free()
		acc = ACCOUNTS_VIEWER.instantiate()
		content.add_child(acc)

func _on_quiz_editor_pressed():
	if acc != null:
		acc.queue_free()
		quiz = QUIZ_EDITOR.instantiate()
		content.add_child(quiz)

func _on_scores_pressed():
	pass
