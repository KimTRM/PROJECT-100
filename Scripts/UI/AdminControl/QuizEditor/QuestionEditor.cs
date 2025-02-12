using Godot;
using GodotUtilities;
using Godot.Collections;

[Scene]
public partial class QuestionEditor : PanelContainer
{
	[Node ("MarginContainer/VBoxContainer/HBoxContainer/MarginContainer/QuestionNumber")]
	public Label QuestionNumber;

	[Node ("MarginContainer/VBoxContainer/QuestionPanel/MarginContainer/QuestionBox")]
	public LineEdit Question;

	[Node ("MarginContainer/VBoxContainer/ChoicePanel/MarginContainer/HBoxContainer/ChoiceA")]
	public LineEdit ChoiceA;

	[Node ("MarginContainer/VBoxContainer/ChoicePanel/MarginContainer/HBoxContainer/CorrectAnswerA")]
	public CheckBox CorrectAnswerA;

	[Node ("MarginContainer/VBoxContainer/ChoicePanel2/MarginContainer/HBoxContainer/ChoiceB")]
	public LineEdit ChoiceB;

	[Node ("MarginContainer/VBoxContainer/ChoicePanel2/MarginContainer/HBoxContainer/CorrectAnswerB")]
	public CheckBox CorrectAnswerB;

	[Node ("MarginContainer/VBoxContainer/ChoicePanel3/MarginContainer/HBoxContainer/ChoiceC")]		
	public LineEdit ChoiceC;

	[Node ("MarginContainer/VBoxContainer/ChoicePanel3/MarginContainer/HBoxContainer/CorrectAnswerC")]
	public CheckBox CorrectAnswerC;

	[Node ("MarginContainer/VBoxContainer/ChoicePanel4/MarginContainer/HBoxContainer/ChoiceD")]
	public LineEdit ChoiceD;

	[Node ("MarginContainer/VBoxContainer/ChoicePanel4/MarginContainer/HBoxContainer/CorrectAnswerD")]
	public CheckBox CorrectAnswerD;

	public string ID;
	private string _CorrectAnswer = "";

    public override void _Notification(int what)
    {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}


    private bool CanSaveData()
	{
		Question.Modulate = Question.Text == "" ? new Color(1, 0, 0) : new Color(1, 1, 1);
		ChoiceA.Modulate = ChoiceA.Text == "" ? new Color(1, 0, 0) : new Color(1, 1, 1);
		ChoiceB.Modulate = ChoiceB.Text == "" ? new Color(1, 0, 0) : new Color(1, 1, 1);
		ChoiceC.Modulate = ChoiceC.Text == "" ? new Color(1, 0, 0) : new Color(1, 1, 1);
		ChoiceD.Modulate = ChoiceD.Text == "" ? new Color(1, 0, 0) : new Color(1, 1, 1);

		if (_CorrectAnswer == "")
		{
			GetNode<MarginContainer>("MarginContainer/VBoxContainer/MarginContainer").Show();
		}
		else
		{
			GetNode<MarginContainer>("MarginContainer/VBoxContainer/MarginContainer").Hide();
		}

		return Question.Text != "" && ChoiceA.Text != "" && ChoiceB.Text != "" && ChoiceC.Text != "" && ChoiceD.Text != "";
	}

	private void _on_save_button_pressed()
	{
		GD.Print("Save Question");
		if (CanSaveData())
		{
			var data = new Dictionary
			{
				{"ID", ID},
				{ "Question", Question.Text },
				{ "Option1", ChoiceA.Text },
				{ "Option2", ChoiceB.Text },
				{ "Option3", ChoiceC.Text },
				{ "Option4", ChoiceD.Text },
				{ "Answer", _CorrectAnswer }
			};

			HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["UPDATE_QUIZ"], data);
		}
	}

	private void _on_delete_button_pressed()
	{
		var data = new Dictionary
		{
			{"ID", ID}
		};

		HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["DELETE_QUIZ"], data);
		QueueFree();
	}

	public void toggle_answer(string answer)
	{
		_CorrectAnswer = answer;
		Color greenColor = new Color(0, 1, 0);
		Color whiteColor = new Color(1, 1, 1);

		CorrectAnswerA.ButtonPressed = answer == "A"? true : false;
		CorrectAnswerB.ButtonPressed = answer == "B"? true : false;
		CorrectAnswerC.ButtonPressed = answer == "C"? true : false;
		CorrectAnswerD.ButtonPressed = answer == "D"? true : false;

	}

	private void _on_correct_answer_a_pressed(bool button_pressed)
	{
		if (button_pressed)
		{
			CorrectAnswerB.ButtonPressed = false;
			CorrectAnswerC.ButtonPressed = false;
			CorrectAnswerD.ButtonPressed = false;
			_CorrectAnswer = "A";
		}
	}

	private void _on_correct_answer_b_pressed(bool button_pressed)
	{
		if (button_pressed)
		{
			CorrectAnswerA.ButtonPressed = false;
			CorrectAnswerC.ButtonPressed = false;
			CorrectAnswerD.ButtonPressed = false;
			_CorrectAnswer = "B";
		}
	}

	private void _on_correct_answer_c_pressed(bool button_pressed)	
	{
		if (button_pressed)
		{
			CorrectAnswerA.ButtonPressed = false;
			CorrectAnswerB.ButtonPressed = false;
			CorrectAnswerD.ButtonPressed = false;
			_CorrectAnswer = "C";
		}
	}

	private void _on_correct_answer_d_pressed(bool button_pressed)
	{
		if (button_pressed)
		{
			CorrectAnswerA.ButtonPressed = false;
			CorrectAnswerB.ButtonPressed = false;
			CorrectAnswerC.ButtonPressed = false;
			_CorrectAnswer = "D";
		}
	}
}
