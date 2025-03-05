using Godot;
using GodotUtilities;
using Godot.Collections;

[Scene]
public partial class ActivitiesMenu : MarginContainer
{
	[Node("MarginContainer/ActivitiesContainer")]
	public HFlowContainer activitiesContainer;

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
		GD.Print("Test");
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

			activitiesContainer.AddChild(activity);

			activity.button.Connect("pressed", Callable.From(() =>
			{
				LoadQuiz(data);
			}));
		}
	}

	void LoadQuiz(Dictionary data)
	{
		var QuizMenu = (QuizMenu)ResourceLoader.Load<PackedScene>("res://Scenes/UI/QuizMenu/QuizMenu.tscn").Instantiate();

		HTTPManager.Instance.RequestCompleted += QuizMenu.OnQuizReceived;
		var datas = new Dictionary { { "QuizCategory", data["LessonTitle"].ToString() } };
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_SPECIFIC_QUIZ"], datas);

		QueueFree();
		GetParent().AddChild(QuizMenu);
	}
}
