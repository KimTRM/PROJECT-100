using Godot;
using GodotUtilities;
using Godot.Collections;
using System.Linq;

[Scene]
public partial class QuizEditor : MarginContainer
{
	[Node ("VBoxContainer/ScrollContainer/QuestionContainer")]
	private VBoxContainer QuizList;

	[Node ("VBoxContainer/PanelContainer/HBoxContainer/MarginContainer/HBoxContainer/QuestionNumber")]
	private Label NumOfQuestions;

	[Export]
	private Array<Dictionary> Datas = new();

	[Export]
	private Array<QuestionEditor> Quizes = new();
	int index = 0;

	[Export]
	public string QuizCategory;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public void OnQuestionsReceived(Array<Dictionary> response)
	{
		Datas = response;
		NumOfQuestions.Text = Datas.Count.ToString();
		LoadQuestions();

		HTTPManager.Instance.RequestCompleted -= OnQuestionsReceived;
	}

	private void LoadQuestions()
	{
		foreach (Dictionary data in Datas)
		{
			QuestionEditor Question = (QuestionEditor)ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/QuizEditor/QuestionEditor.tscn").Instantiate();
			QuizList.AddChild(Question);
		}	

		foreach (QuestionEditor questionEditor in QuizList.GetChildren().Cast<QuestionEditor>())	
		{
			if (questionEditor is QuestionEditor)
			{
				Quizes.Add(questionEditor);
			}
		}

		foreach (Dictionary data in Datas)
		{
			Quizes[index].ID = $"{data["ID"]}";
			Quizes[index]._questionNumber.Text = $"{index + 1}";
			Quizes[index]._questionBox.Text = $"{data["Question"]}";
			Quizes[index]._choiceA.Text = $"{data["ChoiceA"]}";
			Quizes[index]._choiceB.Text = $"{data["ChoiceB"]}";
			Quizes[index]._choiceC.Text = $"{data["ChoiceC"]}";
			Quizes[index]._choiceD.Text = $"{data["ChoiceD"]}";
			Quizes[index].SetAnswer($"{data["CorrectAnswer"]}");
			Quizes[index].QuizCategory = QuizCategory;
			index++;
		}
	}

	private void _on_add_question_pressed()
	{
		QuestionEditor Qustion = (QuestionEditor)ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/QuizEditor/QuestionEditor.tscn").Instantiate();
		QuizList.AddChild(Qustion);
	
		Qustion._questionNumber.Text = $"{index + 1}";
		Qustion.ID = HTTPManager.Instance.GenarateId();
		Qustion.QuizCategory = QuizCategory;

		index++;
		NumOfQuestions.Text = $"{index}";
	}

	private void _on_reload_questions_pressed()
	{
		foreach (Node child in QuizList.GetChildren())
		{
			child.QueueFree();
		}

		Datas.Clear();
		Quizes.Clear();
		index = 0;

		HTTPManager.Instance.RequestCompleted += OnQuestionsReceived;

		var datas = new Dictionary {{ "QuizCategory", QuizCategory }};
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_SPECIFIC_QUIZ"], datas);
		// HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_QUIZ"]);
	}
}
