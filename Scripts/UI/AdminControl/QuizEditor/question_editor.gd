extends PanelContainer

@onready var question_number: Label = $MarginContainer/VBoxContainer/HBoxContainer/MarginContainer/QuestionNumber

@onready var question_box: LineEdit = $MarginContainer/VBoxContainer/QuestionPanel/MarginContainer/QuestionBox

@onready var choice_a: LineEdit = $MarginContainer/VBoxContainer/ChoicePanel/MarginContainer/HBoxContainer/ChoiceA
@onready var correct_answer_a: CheckBox = $MarginContainer/VBoxContainer/ChoicePanel/MarginContainer/HBoxContainer/CorrectAnswerA

@onready var choice_b: LineEdit = $MarginContainer/VBoxContainer/ChoicePanel2/MarginContainer/HBoxContainer/ChoiceB
@onready var correct_answer_b: CheckBox = $MarginContainer/VBoxContainer/ChoicePanel2/MarginContainer/HBoxContainer/CorrectAnswerB

@onready var choice_c: LineEdit = $MarginContainer/VBoxContainer/ChoicePanel3/MarginContainer/HBoxContainer/ChoiceC
@onready var correct_answer_c: CheckBox = $MarginContainer/VBoxContainer/ChoicePanel3/MarginContainer/HBoxContainer/CorrectAnswerC

@onready var choice_d: LineEdit = $MarginContainer/VBoxContainer/ChoicePanel4/MarginContainer/HBoxContainer/ChoiceD
@onready var correct_answer_d: CheckBox = $MarginContainer/VBoxContainer/ChoicePanel4/MarginContainer/HBoxContainer/CorrectAnswerD

var ID: String
var Question: String
var ChoiceA: String
var ChoiceB: String
var ChoiceC: String
var ChoiceD: String
var CorrectAnswer: String = ""

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
	
	if CorrectAnswer == "":
		$MarginContainer/VBoxContainer/MarginContainer.show()
	else:
		$MarginContainer/VBoxContainer/MarginContainer.hide()
	
	if question_box.text != "" and choice_a.text != "" and choice_b.text != "" and choice_c.text != "" and choice_d.text != "" and CorrectAnswer != "":
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

func toggle_answer(answer: String) -> void:
	match answer:
		"A":
			correct_answer_a.button_pressed = true
		"B":
			correct_answer_b.button_pressed = true
		"C":
			correct_answer_c.button_pressed = true
		"D":
			correct_answer_d.button_pressed = true

func _on_correct_answer_a_toggled(toggled_on: bool) -> void:
	if toggled_on:
		correct_answer_b.button_pressed = false
		correct_answer_c.button_pressed = false
		correct_answer_d.button_pressed = false
		CorrectAnswer = "A"
func _on_correct_answer_b_toggled(toggled_on: bool) -> void:
	if toggled_on:
		correct_answer_a.button_pressed = false
		correct_answer_c.button_pressed = false
		correct_answer_d.button_pressed = false
		CorrectAnswer = "B"
func _on_correct_answer_c_toggled(toggled_on: bool) -> void:
	if toggled_on:
		correct_answer_a.button_pressed = false
		correct_answer_b.button_pressed = false
		correct_answer_d.button_pressed = false
		CorrectAnswer = "C"
func _on_correct_answer_d_toggled(toggled_on: bool) -> void:
	if toggled_on:
		correct_answer_a.button_pressed = false
		correct_answer_b.button_pressed = false
		correct_answer_c.button_pressed = false
		CorrectAnswer = "D"
