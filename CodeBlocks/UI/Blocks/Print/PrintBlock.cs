using Godot;
using GodotUtilities;
using System;
using System.Threading.Tasks;

[Scene]
public partial class PrintBlock : CodeBlock
{
	[Node("PanelContainer/MarginContainer/HBoxContainer/ValueContainer")]
	private PanelContainer valueContainer;
	[Node("PanelContainer/MarginContainer/HBoxContainer/ValueContainer/Value")]
	private LineEdit value;

	private Console console;

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();	
		}
    }

    public override void _Ready()
    {
		console = GetParent().GetParent().GetParent().GetParent().GetNode<Console>("Console");
    }

    public override async Task Execute()
    {
		foreach (Control child in valueContainer.GetChildren())	
		{
			if (child is CodeBlock block)
			{
				await block.Execute();
				EmitSignal(Console.SignalName.CommandEntered, block.BlockValue.ToString());
			}
			else
			{
				EmitSignal(Console.SignalName.CommandEntered, value.Text);
			}
		}
		await Task.CompletedTask;
	}
}
