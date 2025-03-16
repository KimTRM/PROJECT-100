using Godot;
using Godot.Collections;
using GodotUtilities;

[Scene]
public partial class LoginMenu : Control
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

		HTTPManager.Instance.RequestCompleted -= OnAccountReceived;
		HTTPManager.Instance.RequestError -= CheckError;
	}

	public void OnAccountReceived(Array<Dictionary> response)
	{
		LoadingPanel.Hide();
		NoConnectionPanel.Hide();

		UserDatas = response;

		HTTPManager.Instance.RequestCompleted -= OnAccountReceived;
		HTTPManager.Instance.RequestError -= CheckError;
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
					ErrorMessage.Hide();
					GameManager.Instance.LoadScene("res://Scenes/UI/AdminControl/AdminPage.tscn");
				}
				else if (user["Role"].ToString() == "Student")
				{
					GameManager.Instance.PlayerUsername = user["UserName"].ToString();
					GameManager.Instance.PlayerID = user["UserID"].ToString();

					ErrorMessage.Hide();
					GameManager.Instance.LoadScene("res://Scenes/UI/StartingScreen/StartingMenu.tscn");
				}

				// Remove event listeners
				HTTPManager.Instance.RequestError -= CheckError;
				HTTPManager.Instance.RequestCompleted -= OnAccountReceived;

				break; // Exit loop since we found a valid user
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
}
