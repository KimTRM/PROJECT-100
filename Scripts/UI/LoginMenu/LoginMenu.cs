using Godot;
using Godot.Collections;
using GodotUtilities;

[Scene]
public partial class LoginMenu : Control
{
	[Node ("PanelContainer/LoginPanel/MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/PanelContainer/MarginContainer/VBoxContainer/Username")]
	private LineEdit Username;
	
	[Node ("PanelContainer/LoginPanel/MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/PanelContainer2/MarginContainer/VBoxContainer/Password")]
	private LineEdit Password;

	[Node ("PanelContainer/LoginPanel/MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/ErrorMessage")]
	private Label ErrorMessage;

	[Node ("PanelContainer/SignupPanel")]
	private PanelContainer SignupPanel;

	[Node ("PanelContainer/LoadingPanel")]
	private MarginContainer LoadingPanel;

	[Node ("PanelContainer/NoConnectionPanel")]
	private MarginContainer NoConnectionPanel;

	[Export]
	private Array<Dictionary> UserDatas;

    public override void _Notification(int what)
    {
        if(what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

    public override void _Ready()
	{
		LoadingPanel.Show();
		HTTPManager.Instance.RequestCompleted += OnAccountReceived;
		HTTPManager.Instance.RequestError += CheckError;
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_USER_ACCOUNT"]);
	}
	
	private void CheckError(Dictionary ErrorMessage)
	{
		NoConnectionPanel.Show();

		HTTPManager.Instance.RequestCompleted -= OnAccountReceived;
		HTTPManager.Instance.RequestError -= CheckError;
	}
	
	private void OnAccountReceived(Array<Dictionary> response)
	{
		LoadingPanel.Hide();
		NoConnectionPanel.Hide();

		UserDatas = response;
	}

	public void _on_login_button_pressed()
	{
		var username = Username.Text;
		var password = Password.Text;

		if(UserDatas == null)
			return;
		
		foreach(var user in UserDatas)
		{
			if(user["UserName"].ToString() == username && user["Password"].ToString() == password)
			{
				if(user["Role"].ToString() == "Admin")
				{
					HTTPManager.Instance.RequestCompleted -= OnAccountReceived;
					HTTPManager.Instance.RequestError -= CheckError;

					GetTree().ChangeSceneToFile("res://Scenes/UI/AdminControl/AdminPage.tscn");
				}
				else
				{
					HTTPManager.Instance.RequestCompleted -= OnAccountReceived;
					HTTPManager.Instance.RequestError -= CheckError;

					GetTree().ChangeSceneToFile("res://Scenes/UI/StartingScreen/StartingMenu.tscn");
				}
			}
			else
			{
				ErrorMessage.Show();
				ErrorMessage.Text = "Invalid";
			}
		}
	}

	private void _on_to_signup_button_pressed()
	{
		SignupPanel.Show();
	}

	private void _on_reconnect_button_pressed()
	{
		NoConnectionPanel.Hide();
		HTTPManager.Instance.RequestCompleted += OnAccountReceived;
		HTTPManager.Instance.RequestError += CheckError;
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_USER_ACCOUNT"]);
	}

	private void _on_play_anyway_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/UI/StartingScreen/StartingMenu.tscn");
	}
}
