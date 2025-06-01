using Godot;
using GodotUtilities;
using System;
using System.Threading.Tasks;

[Scene]
public partial class MathCalculationBlock : CodeBlock
{
	[Node("PanelContainer/MarginContainer/HBoxContainer/ValueContainer1")]
	private PanelContainer valueContainer1;
	[Node("PanelContainer/MarginContainer/HBoxContainer/ValueContainer2")]
	private PanelContainer valueContainer2;

	[Node("PanelContainer/MarginContainer/HBoxContainer/ValueContainer1/Value1")]
	private LineEdit value1;
	[Node("PanelContainer/MarginContainer/HBoxContainer/ValueContainer2/Value2")]
	private LineEdit value2;

	[Node("PanelContainer/MarginContainer/HBoxContainer/MathSymbol")]
	private OptionButton mathSymbol;

	public string Value1;
	public string Value2;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override async Task Execute()
	{
		foreach (Control child in valueContainer1.GetChildren())
		{
			if (child is CodeBlock block)
			{
				await block.Execute();
				BlockValue = block.BlockValue;
			}
			else if (!string.IsNullOrWhiteSpace(value1.Text) && !string.IsNullOrWhiteSpace(value2.Text))
			{
				Value1 = value1.Text;
				Value2 = value2.Text;

				int v1, v2;
				if (int.TryParse(Value1, out v1) && int.TryParse(Value2, out v2))
				{
					BlockValue = Calculate(v1, v2, mathSymbol.Text);
				}
				else
				{
					GD.PrintErr("Invalid input: Value1 or Value2 is not a valid integer.");
					value1.Text = string.Empty;
					value2.Text = string.Empty;
					ShowErrorMessage("Please enter valid integers for Value1 and Value2.");
					BlockValue = 0; // Reset BlockValue
				}
			}
		}
		await Task.CompletedTask;
	}

	private int Calculate(int value1, int value2, string mathSymbol)
	{
		switch (mathSymbol)
		{
			case "+":
				return value1 + value2;
			case "-":
				return value1 - value2;
			case "*":
				return value1 * value2;
			case "/":
				return value1 / value2;
			default:
				return 0;
		}
	}

	public override void _Ready()
	{
	}

	private void ShowErrorMessage(string message)
	{
		// Example: Display a popup or label with the error message
		var errorPopup = new Popup();
		AddChild(errorPopup);
		errorPopup.PopupCentered();
		var label = new Label { Text = message };
		errorPopup.AddChild(label);
	}


}
