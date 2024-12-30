@icon("res://Code/Variables/IntBlock/Art/int.png")
class_name IntBlock extends CodeBlock

@export var code: VarCodeInfo

# -- NODES --
@onready var _var_name: LineEdit = $PanelContainer/PanelContainer/MarginContainer/HBoxContainer/PanelContainer2/VarName
@onready var _value: LineEdit = $PanelContainer/PanelContainer/MarginContainer/HBoxContainer/PanelContainer3/Value

func _ready():
	_var_name.text = code.var_name
	_value.text = str(code.value)

func SetValue():
	VarCategory = code.category
	VarType = code.type
	VarName = _var_name.text
	CheckIfInt()

func CheckIfInt():
	var isInt = _value.text.is_valid_int()

	if isInt:
		VarValue = int(_value.text)

	_value.modulate = Color.WHITE if isInt  else Color.RED
