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
		HTTPManager.Instance.RequestCompleted += OnAccountReceived;
		HTTPManager.Instance.RequestError += CheckError;
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_USER_ACCOUNT"]);

		// GameManager.Instance.SavePlayerData("1739364453", new Vector2(0, 0), "1", "5");
	}
	
	private void CheckError(Dictionary ErrorMessage)
	{
		GD.Print("No Connection");
	}
	
	private void OnAccountReceived(Array<Dictionary> response)
	{
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
					GetTree().ChangeSceneToFile("res://Scenes/UI/AdminControl/AdminPage.tscn");
				}
				else
				{
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
}
