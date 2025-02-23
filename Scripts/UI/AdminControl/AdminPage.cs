using Godot;
using GodotUtilities;

[Scene]
public partial class AdminPage : Control
{
	[Node("MarginContainer/VBoxContainer/HBoxContainer/Content")]
	private ColorRect Content;

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
		QuizEditorScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/QuizEditor/QuizEditor.tscn").Instantiate();
		Content?.AddChild(AccountsViewerScene);
	}

	private void _on_accounts_pressed()
	{
		if (QuizEditorScene == null || Content.HasNode("AccountsViewer"))
			return;

		HTTPManager.Instance.RequestCompleted -= ((QuizEditor)QuizEditorScene).OnQuestionsReceived;
		QuizEditorScene?.QueueFree();
		AccountsViewerScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/UserAccounts/AccountsViewer.tscn").Instantiate();
		Content?.AddChild(AccountsViewerScene);
	}

	private void _on_quiz_editor_pressed()
	{
		if (AccountsViewerScene == null || Content.HasNode("QuizEditor"))
			return;

		HTTPManager.Instance.RequestCompleted -= ((AccountsViewer)AccountsViewerScene).OnAccountReceived;
		AccountsViewerScene?.QueueFree();
		QuizEditorScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/QuizEditor/QuizEditor.tscn").Instantiate();
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
