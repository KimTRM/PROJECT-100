class_name QuizEditor extends MarginContainer

@onready var question_container = $VBoxContainer/ScrollContainer/QuestionContainer
@onready var no_ = $VBoxContainer/ColorRect/HBoxContainer/MarginContainer/HBoxContainer/No_

var datas: Array = []
var quizes: Array[QuestionEditor] = []
var index = 0

func _ready():
	HttpManager.connect("request_completed", _on_accounts_received)
	HttpManager.queue_request(HttpManager.COMMANDS["GET_QUIZ"])
	
func _on_accounts_received(response):
	datas = response
	
	load_questions()
	no_.text = str(index)

func load_questions():
	for i in datas:
		var Quiz = load("res://AdminControl/QuizEditor/QuestionEditor/QuestionEditor.tscn")
		var quiz = Quiz.instantiate()
		question_container.add_child(quiz)
	
	for child in question_container.get_children():
		if child is QuestionEditor:
			quizes.append(child)
	
	for i in datas:
		quizes[index].ID = i["ID"]
		quizes[index].question_number.text = str(index + 1)
		quizes[index].question_box.text = i["Question"]
		quizes[index].choice_a.text = i["ChoiceA"]
		quizes[index].choice_b.text = i["ChoiceB"]
		quizes[index].choice_c.text = i["ChoiceC"]
		quizes[index].choice_d.text = i["ChoiceD"]
		index += 1

func _on_add_question_pressed():
	var Quiz = load("res://AdminControl/QuizEditor/QuestionEditor/QuestionEditor.tscn")
	var quiz: QuestionEditor = Quiz.instantiate()
	question_container.add_child(quiz)
	
	quiz.question_number.text = str(index + 1)
	quiz.ID = HttpManager.generate_id()
	
	index += 1
	no_.text = str(index)

func _on_reload_questions_pressed():
	for child: QuestionEditor in question_container.get_children():
		child.queue_free()
	
	index = 0
	datas.clear()
	quizes.clear()
	
	HttpManager.disconnect("request_completed", _on_accounts_received)
	HttpManager.connect("request_completed", _on_accounts_received)
	HttpManager.queue_request(HttpManager.COMMANDS["GET_QUIZ"])
