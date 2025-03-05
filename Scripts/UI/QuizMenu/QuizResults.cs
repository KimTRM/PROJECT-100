using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class QuizResults : MarginContainer
{
	[Node("PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/VBoxContainer/PlayerNameLabel")]
	private Label playerNameLabel;
	[Node("PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/VBoxContainer/HBoxContainer/UIDLabel")]
	private Label uidLabel;

	[Node("PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/ProgressBar")]
	private ProgressBar progressBar;

	[Node("PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/PerformanceStatusContainer/HBoxContainer/PanelContainer2/VBoxContainer/NumberOfCorrect")]
	private Label numberOfCorrect;
	[Node("PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/PerformanceStatusContainer/HBoxContainer/PanelContainer3/VBoxContainer/NumberOfIncorrect")]
	private Label numberOfIncorrect;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
	}

	public void SetQuizResults(string name, string uid, int correct, int incorrect)
	{
		playerNameLabel.Text = name;
		uidLabel.Text = uid;
		numberOfCorrect.Text = correct.ToString();
		numberOfIncorrect.Text = incorrect.ToString();

		progressBar.MaxValue = correct + incorrect;
		progressBar.Value = correct;
	}

	void _on_exit_button_pressed()
	{
		var ActivitesMenu = (ActivitiesMenu)ResourceLoader.Load<PackedScene>("res://Scenes/UI/ActivitesMenu/ActivitiesMenu.tscn").Instantiate();
		GetParent().AddChild(ActivitesMenu);
		QueueFree();
	}
}
