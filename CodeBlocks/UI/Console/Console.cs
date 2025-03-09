using Godot;
using Godot.Collections;
using GodotUtilities;
using System;

[Scene]
public partial class Console : MarginContainer
{
	[Node("MarginContainer/TextEdit")]
	private TextEdit textEdit;

	[Signal] public delegate void CommandEnteredEventHandler(string command);

	private Array<string> commandHistory = new Array<string>();

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		CommandEntered += OnCommandEntered;
	}

	private void OnCommandEntered(string command)
	{
		commandHistory.Add(command);
		UpdateConsole();
	}

	private void UpdateConsole()
	{
		textEdit.Text = "";
		foreach (string command in commandHistory)
		{
			textEdit.Text += command + "\n";
		}
	}
}
