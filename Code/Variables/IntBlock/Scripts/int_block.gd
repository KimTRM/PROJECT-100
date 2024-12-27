@icon("res://Code/Variables/IntBlock/Art/int.png")
class_name IntBlock extends Control

@export var code: VarCodeInfo

@onready var var_name = $PanelContainer/PanelContainer/MarginContainer/HBoxContainer/PanelContainer2/VarName
@onready var value = $PanelContainer/PanelContainer/MarginContainer/HBoxContainer/PanelContainer3/Value

func _ready():
	pass

func _input(event):
	if event.is_action_pressed("ui_cancel"):
		SetValue()
		var add = int(code.value)
		print(add)
	pass

func SetValue():
	code.var_name = var_name.text
	code.value = value.text 
	pass
