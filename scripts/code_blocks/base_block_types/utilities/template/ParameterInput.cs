using Godot;
using System.Linq;
using GodotUtilities;

[Scene]
public partial class ParameterInput : MarginContainer
{
	[Signal] public delegate void DragStartedEventHandler();

	[Node] private MarginContainer InputSwitcher;
	[Node] private DropAreaComponent DropAreaComponent;

	[Node] private LineEdit TextInput;
	[Node] private OptionButton BoolInput;
	[Node] private OptionButton OptionInput;
	[Node] private ColorPickerButton ColorPickerInput;

	[Node] private MarginContainer Vector2Input;
	[Node] private LineEdit YInput;
	[Node] private LineEdit XInput;

	[Node] private MarginContainer Vector3Input;
	[Node] private LineEdit X3Input;
	[Node] private LineEdit Y3Input;
	[Node] private LineEdit Z3Input;

	[Export] public string PlaceholderText = "Parameter";
	[Export] public Variant.Type InputType = Variant.Type.String;
	[Export] public BlockType BlockType = BlockType.VARIABLE;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		DropAreaComponent.allowedBlockTypes = BlockType;
		UpdateVisibleInput();
	}

	public CodeBlock GetDroppedBlock()
	{
		return DropAreaComponent.DroppedBlock;
	}

	private void UpdateVisibleInput()
	{
		if (DropAreaComponent.HasBlock())
			SwitchInput(null);

		switch (InputType)
		{
			case Variant.Type.Int:
			case Variant.Type.Float:
			case Variant.Type.String:
				SwitchInput(TextInput);
				TextInput.PlaceholderText = PlaceholderText;
				break;
			case Variant.Type.Bool:
				SwitchInput(BoolInput);
				break;
			case Variant.Type.Color:
				SwitchInput(ColorPickerInput);
				break;
			case Variant.Type.Vector2:
				SwitchInput(Vector2Input);
				break;
			case Variant.Type.Vector3:
				SwitchInput(Vector3Input);
				break;
			default:
				SwitchInput(null);
				GD.PushWarning($"{Name}: Unsupported InputType {InputType}");
				break;
		}
	}

	private void SwitchInput(Node node)
	{
		foreach (Control child in InputSwitcher.GetChildren().Cast<Control>())
			child.Visible = child == node;
	}

	private void OnDragAreaComponentDragStarted()
	{
		EmitSignalDragStarted();
	}

	private void OnDropAreaDroppedBlockChanged(CodeBlock droppedBlock)
	{
		UpdateVisibleInput();
	}
}
