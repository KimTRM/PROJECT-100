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

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();	
		}
    }

    public override async Task Execute()
    {
		foreach (Control child in valueContainer.GetChildren())	
		{
			if (child is CodeBlock block)
			{
				await block.Execute();
				console.OnCommandEntered(block.BlockValue.ToString());
			}
			
			else if (valueContainer.GetChildren().Count == 1 && child is LineEdit lineEdit)
			{
				console.OnCommandEntered(value.Text);
			}
		}
		await Task.CompletedTask;
	}
}
