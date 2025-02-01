@icon("res://Code/Variables/IntBlock/Art/int.png")
class_name IntBlock extends CodeBlock

@export var code: VarCodeInfo

# -- NODES --
#@onready var var_name: LineEdit = $PanelContainer/MarginContainer/HBoxContainer/PanelContainer2/VarName
#@onready var value: LineEdit = $PanelContainer/MarginContainer/HBoxContainer/PanelContainer3/Value

func _ready():
	pass
	#_var_name.text = code.var_name
	#_value.text = str(code.value)

func execute():
	print("Executing block: ", name)
	executed.emit()  

#func SetValue():
	#VarCategory = code.category
	#VarType = code.type
	#VarName = _var_name.text
	#CheckIfInt()
#
#func CheckIfInt():
	#var isInt = _value.text.is_valid_int()
#
	#if isInt:
		#VarValue = int(_value.text)
#
	#_value.modulate = Color.WHITE if isInt  else Color.RED
