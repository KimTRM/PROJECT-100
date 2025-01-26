@icon("res://Code/Variables/FloatBlock/Art/float.png")
class_name FloatBlock extends CodeBlock

@export var code: VarCodeInfo

# -- NODES --
@onready var _var_name: LineEdit = $PanelContainer/PanelContainer/MarginContainer/HBoxContainer/PanelContainer2/VarName
@onready var _value: LineEdit = $PanelContainer/PanelContainer/MarginContainer/HBoxContainer/PanelContainer3/Value

func _ready():
	_var_name.text = code.var_name
	_value.text = str(code.value)

func _physics_process(_delta):
	drag_animation()

func _input(event):
	drag(event)

func SetValue():
	VarCategory = code.category
	VarType = code.type
	VarName = _var_name.text
	CheckIfFloat()

func CheckIfFloat():
	var isInt = _value.text.is_valid_float()

	if isInt:
		VarValue = float(_value.text)

	_value.modulate = Color.WHITE if isInt  else Color.RED
