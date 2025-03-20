using Godot;
using Godot.Collections;
using GodotUtilities;

[Scene]
public partial class LoginMenu : CanvasLayer
{
	[Node("PanelContainer/LoginPanel/MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/PanelContainer/MarginContainer/VBoxContainer/Username")]
	private LineEdit Username;

	[Node("PanelContainer/LoginPanel/MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/PanelContainer2/MarginContainer/VBoxContainer/Password")]
	private LineEdit Password;

	[Node("PanelContainer/LoginPanel/MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/ErrorMessage")]
	private Label ErrorMessage;

	[Node("PanelContainer/SignupPanel")]
	private PanelContainer SignupPanel;

	[Node("PanelContainer/LoadingPanel")]
	private MarginContainer LoadingPanel;

	[Node("PanelContainer/NoConnectionPanel")]
	private MarginContainer NoConnectionPanel;

	[Export]
	private Array<Dictionary> UserDatas;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
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

	public void CheckError(Dictionary ErrorMessage)
	{
		NoConnectionPanel.Show();

		HTTPManager.Instance.RequestError -= CheckError;
	}

	public void OnAccountReceived(Array<Dictionary> response)
	{
		LoadingPanel.Hide();
		NoConnectionPanel.Hide();

		UserDatas = response;

		HTTPManager.Instance.RequestCompleted -= OnAccountReceived;
	}

	public void _on_login_button_pressed()
	{
		var username = Username.Text;
		var password = Password.Text;

		if (UserDatas == null)
			return;

		bool isValidUser = false;

		foreach (var user in UserDatas)
		{
			if (username == user["UserName"].ToString() && password == user["Password"].ToString())
			{
				isValidUser = true;

				if (user["Role"].ToString() == "Admin")
				{
					var data = new Dictionary
					{
						{ "UserID", user["UserID"]},
						{ "FirstName", user["FirstName"] },
						{ "LastName", user["LastName"] },
						{ "UserName", user["UserName"] },
						{ "Password", user["Password"] },
						{ "Role", user["Role"] },
						{ "Status", "Online"}
					};
					HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["ADD_USER_ACCOUNT"], data);

					ErrorMessage.Hide();
					GameManager.Instance.LoadScene("res://Scenes/UI/AdminControl/AdminPage.tscn");
				}
				else if (user["Role"].ToString() == "Student")
				{
					var data = new Dictionary
					{
						{ "UserID", user["UserID"]},
						{ "FirstName", user["FirstName"] },
						{ "LastName", user["LastName"] },
						{ "UserName", user["UserName"] },
						{ "Password", user["Password"] },
						{ "Role", user["Role"] },
						{ "Status", "Online"}
					};
					HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["ADD_USER_ACCOUNT"], data);

					GameManager.Instance.PlayerUsername = user["UserName"].ToString();
					GameManager.Instance.PlayerID = user["UserID"].ToString();

					ErrorMessage.Hide();
					GameManager.Instance.LoadScene("res://Scenes/UI/StartingScreen/StartingMenu.tscn");
				}

				HTTPManager.Instance.RequestError -= CheckError;
				HTTPManager.Instance.RequestCompleted -= OnAccountReceived;

				break;
			}
		}

		// If no valid user was found, show error message
		if (!isValidUser)
		{
			GD.Print("Invalid");
			ErrorMessage.Show();
			ErrorMessage.Text = "Invalid";
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
		GameManager.Instance.LoadScene("res://Scenes/UI/StartingScreen/StartingMenu.tscn");
	}

	void _on_full_screen_toggled(bool button_pressed)
	{
		GD.Print("Toggled" + $": {button_pressed}");
		DisplayServer.WindowSetMode(button_pressed ? DisplayServer.WindowMode.ExclusiveFullscreen : DisplayServer.WindowMode.Windowed);
	}
}
