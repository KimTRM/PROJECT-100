using Godot;
using Godot.Collections;

public partial class LoginMenu : Control
{
	private LineEdit UserName;
	private LineEdit Password;

	[Export]
	private Array<Dictionary> UserDatas;

	public override void _Ready()
	{
		UserName = GetNode<LineEdit>("PanelContainer/LoginPanel/MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/PanelContainer/MarginContainer/VBoxContainer/Username");
		Password = GetNode<LineEdit>("PanelContainer/LoginPanel/MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/PanelContainer2/MarginContainer/VBoxContainer/Password");
	
		HTTPManager.Instance.RequestCompleted += OnAccountReceived;
		var data = new Dictionary();
		HTTPManager.Instance.QueueRequest("get_user_account", data);
	}
	
	private void OnAccountReceived(Array<Dictionary> response)
	{
		UserDatas = response;
	}

}
