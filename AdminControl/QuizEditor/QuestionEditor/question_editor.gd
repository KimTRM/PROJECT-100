class_name QuestionEditor extends Control

@onready var question_number = $MarginContainer2/MarginContainer/VBoxContainer/HBoxContainer/MarginContainer/QuestionNumber

@onready var question_box: LineEdit = $MarginContainer2/MarginContainer/VBoxContainer/MarginContainer2/ColorRect/MarginContainer/QuestionBox
@onready var choice_a: LineEdit = $MarginContainer2/MarginContainer/VBoxContainer/MarginContainer3/ColorRect/MarginContainer/HBoxContainer/ChoiceA
@onready var choice_b: LineEdit = $MarginContainer2/MarginContainer/VBoxContainer/MarginContainer4/ColorRect/MarginContainer/HBoxContainer/ChoiceB
@onready var choice_c: LineEdit = $MarginContainer2/MarginContainer/VBoxContainer/MarginContainer5/ColorRect/MarginContainer/HBoxContainer/ChoiceC
@onready var choice_d: LineEdit = $MarginContainer2/MarginContainer/VBoxContainer/MarginContainer6/ColorRect/MarginContainer/HBoxContainer/ChoiceD

@onready var save_button = $MarginContainer2/MarginContainer/VBoxContainer/HBoxContainer/SaveButton

var ID: String
var Question: String
var ChoiceA: String
var ChoiceB: String
var ChoiceC: String
var ChoiceD: String
var CorrectAnswer: String

func can_save_data() -> bool:
	if question_box.text != "":
		question_box.modulate = Color.WHITE
	else:
		question_box.modulate = Color.RED
	
	if choice_a.text != "":
		choice_a.modulate = Color.WHITE
	else:
		choice_a.modulate = Color.RED
	
	if choice_b.text != "":
		choice_b.modulate = Color.WHITE
	else:
		choice_b.modulate = Color.RED
	
	if choice_c.text != "":
		choice_c.modulate = Color.WHITE
	else:
		choice_c.modulate = Color.RED
	
	if choice_d.text != "":
		choice_d.modulate = Color.WHITE
	else:
		choice_d.modulate = Color.RED
		
	if question_box.text != "" and choice_a.text != "" and choice_b.text != "" and choice_c.text != "" and choice_d.text != "":
		return true
		
	return false

func save_data():
	if can_save_data():
		Question = question_box.text
		ChoiceA = choice_a.text
		ChoiceB = choice_b.text
		ChoiceC = choice_c.text
		ChoiceD = choice_d.text
		
		var data:Dictionary = {
			"ID":ID,
			"Question" : Question,
			"ChoiceA" : ChoiceA,
			"ChoiceB" : ChoiceB,
			"ChoiceC" : ChoiceC,
			"ChoiceD" : ChoiceD,
			"CorrectAnswer" : CorrectAnswer
		}
		HttpManager.queue_request(HttpManager.COMMANDS["ADD_QUIZ"], data)

func _on_save_button_pressed():
	save_data()

func _on_delete_button_pressed():
	var data:Dictionary = {
		"ID":ID,
		"Question" : Question,
		"ChoiceA" : ChoiceA,
		"ChoiceB" : ChoiceB,
		"ChoiceC" : ChoiceC,
		"ChoiceD" : ChoiceD,
		"CorrectAnswer" : CorrectAnswer
	}
	
	HttpManager.queue_request(HttpManager.COMMANDS["DELETE_QUIZ"], data)
	queue_free()
