using Godot;
using GodotUtilities;

[Scene]
public partial class TemplateEditor : MarginContainer
{
	[Signal] public delegate void DragStartedEventHandler();

	[Node] private HBoxContainer container;

	[Export] public string TemplateName { get; set; } = "NewTemplate";
	[Export] public string TemplateCategory { get; set; } = "General";
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
		UpdateTemplate();
	}

	private void UpdateTemplate()
	{
		foreach (var child in container.GetChildren())
		{
			container.RemoveChild(child);
			child.QueueFree();
		}

		AppendLabel(container, TemplateName);
		AppendInputParameter(container);
	}

	private void AppendLabel(HBoxContainer container, string text)
	{
		var label = new Label
		{
			Text = text,
			CustomMinimumSize = new Vector2(18, 16)
		};

		container.AddChild(label);
	}

	private void AppendInputParameter(HBoxContainer container)
	{
		var parameterInput = GD.Load<PackedScene>("uid://chdl1sp7r2avd").Instantiate<ParameterInput>();
		parameterInput.DragStarted += EmitSignalDragStarted;
		// parameterInput.PlaceholderText = PlaceholderText;
		parameterInput.BlockType = BlockType;
		parameterInput.InputType = InputType;

		container.AddChild(parameterInput);
	}
}
