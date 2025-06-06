using Godot;
using GodotUtilities;
using Godot.Collections;
using System.Linq;
using System;

[Scene]
public partial class QuizMenu : CanvasLayer
{
	[Node("MarginContainer2/HBoxContainer/CurrentItem")]
	public Label currentItem;
	[Node("MarginContainer2/HBoxContainer/TotalItems")]
	public Label totalItems;

	[Node("QuizContainer/VBoxContainer/Question")]
	public Label question;

	[Node("QuizContainer/VBoxContainer/MarginContainer/ChoiceButtonContainer")]
	public HBoxContainer choiceButtonContainer;

	[Node("QuizContainer/VBoxContainer/MarginContainer/ChoiceButtonContainer/ChoiceButtonA")]
	public Button choiceButtonA;
	[Node("QuizContainer/VBoxContainer/MarginContainer/ChoiceButtonContainer/ChoiceButtonB")]
	public Button choiceButtonB;
	[Node("QuizContainer/VBoxContainer/MarginContainer/ChoiceButtonContainer/ChoiceButtonC")]
	public Button choiceButtonC;
	[Node("QuizContainer/VBoxContainer/MarginContainer/ChoiceButtonContainer/ChoiceButtonD")]
	public Button choiceButtonD;

	int index = 0;

	[Export]
	private Array<Dictionary> Datas = new();
	[Export]
	private Array<string> currentAnswers = new();

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public void RandomizeQuiz(int maxQuestions = 0)
	{
		Datas = [.. Datas.OrderBy(x => new Random().Next()).ToList()];

		if (maxQuestions > 0 && maxQuestions < Datas.Count)
		{
			Datas = [.. Datas.Take(maxQuestions)];
		}
	}

	public void OnQuizReceived(Array<Dictionary> response)
	{
		Datas = response;

		RandomizeQuiz(5);

		SetQuizData();

		HTTPManager.Instance.RequestCompleted -= OnQuizReceived;
	}

	public void SetQuizData()
	{
		currentItem.Text = $"{index + 1}";
		totalItems.Text = $"{Datas.Count}";
		question.Text = Datas[index]["Question"].ToString();
		choiceButtonA.Text = Datas[index]["ChoiceA"].ToString();
		choiceButtonB.Text = Datas[index]["ChoiceB"].ToString();
		choiceButtonC.Text = Datas[index]["ChoiceC"].ToString();
		choiceButtonD.Text = Datas[index]["ChoiceD"].ToString();
	}

	public void SetAnswer(string answer)
	{
		currentAnswers.Add(answer);
	}

	private void CheckAnswers()
	{
		int correctCount = 0;
		for (int i = 0; i < Datas.Count; i++)
		{
			if (i < currentAnswers.Count && Datas[i]["CorrectAnswer"].ToString() == currentAnswers[i])
			{
				correctCount++;
			}
		}

		var QuizResults = (QuizResults)ResourceLoader.Load<PackedScene>("res://Scenes/UI/QuizMenu/QuizResults.tscn").Instantiate();

		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["ADD_SCORE"], new Dictionary
		{
			{ "ScoreID", HTTPManager.Instance.GenerateId() },
			{ "PlayerID", GameManager.Instance.PlayerID },
			{ "ChapterID", Datas[0]["QuizCategory"].ToString() },
			{ "Score" , correctCount }
		});

		QuizResults.SetQuizResults(GameManager.Instance.PlayerUsername, GameManager.Instance.PlayerID, correctCount, Datas.Count - correctCount);
		QueueFree();
		GetParent().AddChild(QuizResults);
	}

	void _on_choice_button_a_pressed()
	{
		if (index < Datas.Count - 1)
		{
			SetAnswer("A");
			index++;
			SetQuizData();
		}
		else
		{
			SetAnswer("A");
			CheckAnswers();
		}
	}
	void _on_choice_button_b_pressed()
	{
		if (index < Datas.Count - 1)
		{
			SetAnswer("B");
			index++;
			SetQuizData();
		}
		else
		{
			SetAnswer("B");
			CheckAnswers();
		}
	}
	void _on_choice_button_c_pressed()
	{
		if (index < Datas.Count - 1)
		{
			SetAnswer("C");
			index++;
			SetQuizData();
		}
		else
		{
			SetAnswer("C");
			CheckAnswers();
		}
	}
	void _on_choice_button_d_pressed()
	{
		if (index < Datas.Count - 1)
		{
			SetAnswer("D");
			index++;
			SetQuizData();
		}
		else
		{
			SetAnswer("D");
			CheckAnswers();
		}
	}
}
