@icon("res://Code/Conditionals/IfBlock/Art/If.png")
class_name IfBlock extends Control

@onready var body_container = $VBoxContainer/Body/MarginContainer/BodyContainer

# Called when the node enters the scene tree for the first time.
func _ready():
	body_container.Initialize()
	pass

func _input(event):
	if event.is_action_pressed("ui_cancel"):
		ReadCode()

func ReadCode():
	var index = 0
	for i in body_container.code_blocks:
		if i.VarName != "":
			print(str(index) + ": " + i.VarName + " = " + str(i.VarValue))
		else:
			print(str(index) + ": " + str(i.VarType) + " is not defined")
		index += 1