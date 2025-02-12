using Godot;
using GodotUtilities;
using Godot.Collections;
using System.Linq;

[Scene]
public partial class QuizEditor : MarginContainer
{
	[Node ("VBoxContainer/ScrollContainer/QuestionContainer")]
	private VBoxContainer QuizList;

	[Node ("VBoxContainer/ColorRect/HBoxContainer/MarginContainer/HBoxContainer/QuestionNumber")]
	private Label NumOfQuestions;

	[Export]
	private Array<Dictionary> Datas = new();

	[Export]
	private Array<QuestionEditor> Quizes = new();
	int index = 0;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

    public override void _Ready()
	{
		HTTPManager.Instance.RequestCompleted += OnQuestionsReceived;
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_QUIZ"]);
	}

	private void OnQuestionsReceived(Array<Dictionary> response)
	{
		Datas = response;
		NumOfQuestions.Text = Datas.Count.ToString();
		LoadQuestions();
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
			Quizes[index].QuestionNumber.Text = $"{index + 1}";
			Quizes[index].Question.Text = $"{data["Question"]}";
			Quizes[index].ChoiceA.Text = $"{data["ChoiceA"]}";
			Quizes[index].ChoiceB.Text = $"{data["ChoiceB"]}";
			Quizes[index].ChoiceC.Text = $"{data["ChoiceC"]}";
			Quizes[index].ChoiceD.Text = $"{data["ChoiceD"]}";
			Quizes[index].toggle_answer($"{data["CorrectAnswer"]}");
			index++;
		}
	}

	private void _on_add_question_pressed()
	{
		Node Qustion = ResourceLoader.Load<PackedScene>("res://Scenes/UI/AdminControl/QuizEditor/QuestionEditor.tscn").Instantiate();
		QuizList.AddChild(Qustion);
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

		HTTPManager.Instance.RequestCompleted -= OnQuestionsReceived;
		HTTPManager.Instance.RequestCompleted += OnQuestionsReceived;
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_QUIZ"]);
	}
}
