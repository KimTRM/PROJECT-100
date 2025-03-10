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
			else
			{
				Value1 = value1.Text;
				Value2 = value2.Text;
				
				BlockValue = Calculate(int.Parse(Value1), int.Parse(Value2), mathSymbol.Text);
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


}
