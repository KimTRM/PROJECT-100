using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class AddElement : MarginContainer
{
	[Node("PanelContainer/VBoxContainer/TitleLabel")]
	private Label TitleLabel;
	[Node("PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/InfoLabel")]
	private Label InfoLabel;
	[Node("PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/LineEdit")]
	private LineEdit lineEdit;
	[Node("PanelContainer/VBoxContainer/AddButton")]
	public Button AddButton;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public void UpdateElementDetails(string titleLabel, string infoLabel)
	{
		TitleLabel.Text = titleLabel;
		InfoLabel.Text = infoLabel;
	}
	
	public string GetLineData()
	{
		return lineEdit.Text;
	}

	void _on_close_button_pressed()
	{
		QueueFree();
	}
}
