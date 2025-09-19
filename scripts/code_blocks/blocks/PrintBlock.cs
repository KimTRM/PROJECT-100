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

	// public override void _Ready()
	// {
	// 	// value.MouseEntered += OnMouseEntered;
	// }

	public override async Task Execute()
	{
		foreach (Control child in valueContainer.GetChildren())
		{
			if (child is CodeBlock block)
			{
				await block.Execute();
				GD.Print("Print Block Value: ", block.GetBlockValue());
				// console.OnCommandEntered(block.BlockValue.ToString());
			}

			else if (valueContainer.GetChildren().Count == 1 && child is LineEdit lineEdit)
			{
				GD.Print("Print Block Value: ", lineEdit.Text);
				// console.OnCommandEntered(value.Text);
			}
		}
		await Task.CompletedTask;
	}

	// private void OnMouseEntered()
	// {
	// 	foreach (Control block in valueContainer.GetChildren())
	// 	{
	// 		if (block is CodeBlock codeBlock)
	// 		{
	// 			codeBlock.dragManager = dragManager;
	// 		}
	// 	}
	// 	dragManager.SetDroppableTarget(valueContainer);
	// }
}
