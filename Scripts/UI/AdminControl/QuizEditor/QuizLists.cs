using Godot;
using GodotUtilities;
using Godot.Collections;
using System;

[Scene]
public partial class QuizLists : MarginContainer
{
	[Node("MarginContainer/VBoxContainer/ScrollContainer/QuizListContainer")]
	private VBoxContainer QuizList;

	[Export]
	private Array<Dictionary> Data = new();

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		HTTPManager.Instance.RequestCompleted += OnLessonsReceived;
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_LESSON"]);
	}

	public void OnLessonsReceived(Array<Dictionary> response)
	{
		Data = response;
		LoadLessons();
		HTTPManager.Instance.RequestCompleted -= OnLessonsReceived;
	}

	private void LoadLessons()
	{
		foreach (Dictionary data in Data)
		{
			Activity activity = (Activity)ResourceLoader.Load<PackedScene>("res://Scenes/UI/ActivitesMenu/Activity.tscn").Instantiate();
			activity.ActivityName.Text = data["LessonTitle"].ToString();

			QuizList.AddChild(activity);

			activity.button.Connect("pressed", Callable.From(() =>
			{
				var QuizEditorScene = (QuizEditor)ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/QuizEditor/QuizEditor.tscn").Instantiate();

				HTTPManager.Instance.RequestCompleted += QuizEditorScene.OnQuestionsReceived;
				var datas = new Dictionary { { "QuizCategory", data["LessonTitle"].ToString() } };
				HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_SPECIFIC_QUIZ"], datas);

				QuizEditorScene.QuizCategory = data["LessonTitle"].ToString();

				QueueFree();
				GetParent().AddChild(QuizEditorScene);
			}));
		}
	}

	private void _on_add_question_pressed()
	{
		AddElement addElement = (AddElement)ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/AddElement/AddElement.tscn").Instantiate();
		addElement.UpdateElementDetails("ADD QUIZ", "Quiz Name");

		AddChild(addElement);

		addElement.AddButton.Connect("pressed", Callable.From(() =>
		{
			if (addElement.GetLineData() == "") return;

			Activity activity = (Activity)ResourceLoader.Load<PackedScene>("res://Scenes/UI/ActivitesMenu/Activity.tscn").Instantiate();
			activity.ActivityName.Text = addElement.GetLineData();
			QuizList.AddChild(activity);

			activity.button.Connect("pressed", Callable.From(() =>
			{
				var QuizEditorScene = (QuizEditor)ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/QuizEditor/QuizEditor.tscn").Instantiate();

				HTTPManager.Instance.RequestCompleted += QuizEditorScene.OnQuestionsReceived;
				var datas = new Dictionary {{ "QuizCategory", addElement.GetLineData()}};
				HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_SPECIFIC_QUIZ"], datas);

				// QuizEditorScene.QuizCategory = data["LessonTitle"].ToString();

				QueueFree();
				GetParent().AddChild(QuizEditorScene);
			}));

			addElement.QueueFree();
		}));
	}

	private void _on_reload_questions_pressed()
	{
		foreach (Node child in QuizList.GetChildren())
		{
			child.QueueFree();
		}

		HTTPManager.Instance.RequestCompleted += OnLessonsReceived;
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_LESSON"]);
	}
}
