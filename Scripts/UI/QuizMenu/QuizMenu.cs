using Godot;
using GodotUtilities;
using Godot.Collections;
using System.Linq;

[Scene]
public partial class QuizMenu : MarginContainer
{
	[Node("MarginContainer2/HBoxContainer/CurrentItem")]
	public Label currentItem;
	[Node("MarginContainer2/HBoxContainer/TotalItems")]
	public Label totalItems;

	[Node("QuizContainer/VBoxContainer/Question")]
	public Label question;

	[Node("QuizContainer/VBoxContainer/MarginContainer/ChoiceButtonContainer/ChoiceButtonA")]
	public Button choiceButtonA;
	[Node("QuizContainer/VBoxContainer/MarginContainer/ChoiceButtonContainer/ChoiceButtonB")]
	public Button choiceButtonB;
	[Node("QuizContainer/VBoxContainer/MarginContainer/ChoiceButtonContainer/ChoiceButtonC")]
	public Button choiceButtonC;
	[Node("QuizContainer/VBoxContainer/MarginContainer/ChoiceButtonContainer/ChoiceButtonD")]
	public Button choiceButtonD;

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

	public override void _Ready()
	{
		HTTPManager.Instance.RequestCompleted += OnQuizReceived;
		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["GET_QUIZ"]);
	
	}

	public void OnQuizReceived(Array<Dictionary> response)
	{
		Datas = response;
		
		SetQuizData
		(
			Datas[0]["Question"].ToString(), 
			Datas[0]["ChoiceA"].ToString(), 
			Datas[0]["ChoiceB"].ToString(),
			Datas[0]["ChoiceC"].ToString(),
			Datas[0]["ChoiceD"].ToString()
		);

		SetAnswer(Datas[0]["CorrectAnswer"].ToString());
		SetAnswer("A");
	}

	public void SetQuizData(string itemQuestion, string choiceA, string choiceB, string choiceC, string choiceD)
	{
		question.Text = itemQuestion;
		choiceButtonA.Text = choiceA;
		choiceButtonB.Text = choiceB;
		choiceButtonC.Text = choiceC;
		choiceButtonD.Text = choiceD;
	}

	public void SetAnswer(string answer)
    {
		currentAnswers.Add(answer);
    }
}
