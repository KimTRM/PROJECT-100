using Godot;
using GodotUtilities;

[Scene]
public partial class AdminPage : Control
{
	[Node("MarginContainer/VBoxContainer/HBoxContainer/Content")]
	public ColorRect Content;

	private Node QuizEditorScene;
	private Node AccountsViewerScene;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		AccountsViewerScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/UserAccounts/AccountsViewer.tscn").Instantiate();
		Content?.AddChild(AccountsViewerScene);
	}

	private void RemoveContent()
	{
		foreach (Node node in Content.GetChildren())
		{
			node.QueueFree();
		}
	}

	private void _on_accounts_pressed()
	{
		RemoveContent();
		AccountsViewerScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/UserAccounts/AccountsViewer.tscn").Instantiate();
		Content?.AddChild(AccountsViewerScene);
	}

	private void _on_quiz_editor_pressed()
	{
		RemoveContent();
		QuizEditorScene = ResourceLoader.Load<PackedScene>("uid://pm4uiwv60qkn").Instantiate();
		Content?.AddChild(QuizEditorScene);
	}

	private void _on_scores_pressed()
	{
	}

	private void _on_logout_button_pressed()
	{
		HTTPManager.Instance.RequestCompleted -= ((QuizEditor)QuizEditorScene).OnQuestionsReceived;
		HTTPManager.Instance.RequestCompleted -= ((AccountsViewer)AccountsViewerScene).OnAccountReceived;
		
		GameManager.Instance.LoadScene("res://Scenes/UI/LoginMenu/LoginMenu.tscn");
	}
}
