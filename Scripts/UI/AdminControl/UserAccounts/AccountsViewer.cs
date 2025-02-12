using Godot;
using GodotUtilities;
using Godot.Collections;

[Scene]
public partial class AccountsViewer : MarginContainer
{
	[Node ("VBoxContainer/ColorRect/HBoxContainer/MarginContainer/HBoxContainer/NoAccounts")]
	private Label NumOfAccounts;

	[Node ("VBoxContainer/ScrollContainer/Rows/UIDColumn")]
	private VBoxContainer UIdColumn;

	[Node ("VBoxContainer/ScrollContainer/Rows/FirstnameColumn")]
	private VBoxContainer FirstnameColumn;

	[Node ("VBoxContainer/ScrollContainer/Rows/LastnameColumn")]
	private VBoxContainer LastnameColumn;

	[Node ("VBoxContainer/ScrollContainer/Rows/UsernameColumn")]
	private VBoxContainer UsernameColumn;

	[Node ("VBoxContainer/ScrollContainer/Rows/PasswordColumn")]
	private VBoxContainer PasswordColumn;

	[Node ("VBoxContainer/ScrollContainer/Rows/RoleColumn")]
	private VBoxContainer RoleColumn;

	[Export]
	private Array<Dictionary> Datas;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}
	
	public override void _Ready()
	{
		HTTPManager.Instance.RequestCompleted += OnAccountReceived;
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_USER_ACCOUNT"]);
	}

	private void OnAccountReceived(Array<Dictionary> response)
	{
		Datas = response;
		LoadUserAccounts();
	}

	private void LoadUserAccounts()
	{
		NumOfAccounts.Text = Datas.Count.ToString();
		for (int i = 0; i < Datas.Count; i++)
		{
			UIdColumn.AddChild(new Label { Text = Datas[i]["UserID"].ToString() });
			FirstnameColumn.AddChild(new Label { Text = Datas[i]["FirstName"].ToString() });
			LastnameColumn.AddChild(new Label { Text = Datas[i]["LastName"].ToString() });
			UsernameColumn.AddChild(new Label { Text = Datas[i]["UserName"].ToString() });
			PasswordColumn.AddChild(new Label { Text = Datas[i]["Password"].ToString() });
			RoleColumn.AddChild(new Label { Text = Datas[i]["Role"].ToString() });
		}
	}
}
