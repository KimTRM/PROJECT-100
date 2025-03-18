using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class Activity : MarginContainer
{
	[Node("PanelContainer/VBoxContainer/HBoxContainer/ActivityName")]
	public Label ActivityName;
	[Node("PanelContainer/VBoxContainer/HBoxContainer/HBoxContainer/Label")]
	Label Label;
	[Node("PanelContainer/VBoxContainer/HBoxContainer/HBoxContainer/NumItems")]
	public Label NumItems;

	[Node]
	MarginContainer ModifyButtonContainer;

	[Node]
	public Button button;

	[Export]
	public PackedScene scene;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

    public override void _Process(double delta)
    {
        // if(GameManager.Instance.PlayerUsername == null)
		// {
		// 	ModifyButtonContainer.Visible = true;
		// }
		// else
		// {
		// 	ModifyButtonContainer.Visible = false;
		// }
	}

    void _on_button_mouse_entered()
	{
		ActivityName.Modulate = new Color("000000");
		Label.Modulate = new Color("000000");
		NumItems.Modulate = new Color("000000");
	}
	void _on_button_mouse_exited()
	{
		ActivityName.Modulate = new Color("545454");
		Label.Modulate = new Color("545454");
		NumItems.Modulate = new Color("545454");
	}
}
