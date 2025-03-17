using Godot;
using GodotUtilities;

[Scene]
public partial class AdminPage : CanvasLayer
{
	[Node("MarginContainer/VBoxContainer/HBoxContainer/Content")]
	public PanelContainer Content;

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

	private void _on_class_pressed()
	{
		RemoveContent();
		var ClasslistScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/ClassList/ClassNameList.tscn").Instantiate();
		Content?.AddChild(ClasslistScene);
	}

	private void _on_logout_button_pressed()
	{
		GameManager.Instance.LoadScene("res://Scenes/UI/LoginMenu/LoginMenu.tscn");
	}
}
