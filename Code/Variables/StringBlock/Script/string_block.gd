@icon("res://Code/Variables/StringBlock/Art/string.png")
class_name StringBlock extends CodeBlock

@export var code: VarCodeInfo

# -- NODES --
@onready var _var_name: LineEdit = $MarginContainer/HBoxContainer/PanelContainer2/VarName
@onready var _value: LineEdit = $MarginContainer/HBoxContainer/PanelContainer3/Value

func _ready():
	_var_name.text = code.var_name
	_value.text = str(code.value)

func SetValue():
	VarCategory = code.category
	VarType = code.type
	VarName = _var_name.text
	VarValue = _value.text
	
