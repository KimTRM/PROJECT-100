@icon("res://Code/Variables/BooleanBlock/Art/boolean.png")
class_name BooleanBlock extends CodeBlock

@export var code: VarCodeInfo

# -- NODES --
@onready var _var_name: LineEdit = $MarginContainer/HBoxContainer/PanelContainer2/VarName
@onready var _value: OptionButton = $MarginContainer/HBoxContainer/PanelContainer3/Value

func _ready():
	_var_name.text = code.var_name
	_value.text = str(code.value)

func SetValue():
	VarCategory = code.category
	VarType = code.type
	VarName = _var_name.text
	VarValue = _value.text
