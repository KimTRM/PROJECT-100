using Godot;
using Godot.Collections;
using GodotUtilities;
using System;

[Scene]
public partial class ClassNameList : MarginContainer
{
	[Node("MarginContainer/VBoxContainer/ScrollContainer/ClassListContainer")]
	private HFlowContainer ClassListContainer;

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
		HTTPManager.Instance.RequestCompleted += OnClassesReceived;
	}

	private void OnClassesReceived(Array<Dictionary> response)
	{
		Data = response;
		HTTPManager.Instance.RequestCompleted -= OnClassesReceived;
	}

	private void LoadClasses()
	{
		foreach (Dictionary data in Data)
		{
			Activity activity = (Activity)ResourceLoader.Load<PackedScene>("res://Scenes/UI/ActivitesMenu/Activity.tscn").Instantiate();
			activity.ActivityName.Text = data["LessonTitle"].ToString();

			ClassListContainer.AddChild(activity);

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

	void _on_add_class_button_pressed()
	{
		Activity activity = (Activity)ResourceLoader.Load<PackedScene>("res://Scenes/UI/ActivitesMenu/Activity.tscn").Instantiate();
		activity.ActivityName.Text = "New Quiz";
		ClassListContainer.AddChild(activity);

			activity.button.Connect("pressed", Callable.From(() =>
			{
				var QuizEditorScene = (QuizEditor)ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/QuizEditor/QuizEditor.tscn").Instantiate();

				HTTPManager.Instance.RequestCompleted += QuizEditorScene.OnQuestionsReceived;
				// var datas = new Dictionary {{ "QuizCategory", data["LessonTitle"].ToString() }};
				// HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_SPECIFIC_QUIZ"], datas);

				// QuizEditorScene.QuizCategory = data["LessonTitle"].ToString();

				QueueFree();
				GetParent().AddChild(QuizEditorScene);
			}));
	}

}
