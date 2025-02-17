using Godot;
using GodotUtilities;
using Godot.Collections;

[Scene]
public partial class QuestionEditor : PanelContainer
{
    [Node("MarginContainer/VBoxContainer/HBoxContainer/MarginContainer/QuestionNumber")]
    public Label _questionNumber;

    [Node("MarginContainer/VBoxContainer/QuestionPanel/MarginContainer/QuestionBox")]
    public LineEdit _questionBox;

    [Node("MarginContainer/VBoxContainer/ChoicePanel/MarginContainer/HBoxContainer/ChoiceA")]
    public LineEdit _choiceA;
    [Node("MarginContainer/VBoxContainer/ChoicePanel/MarginContainer/HBoxContainer/CorrectAnswerA")]
    private CheckBox _correctAnswerA;

    [Node("MarginContainer/VBoxContainer/ChoicePanel2/MarginContainer/HBoxContainer/ChoiceB")]
    public LineEdit _choiceB;
    [Node("MarginContainer/VBoxContainer/ChoicePanel2/MarginContainer/HBoxContainer/CorrectAnswerB")]
    private CheckBox _correctAnswerB;

    [Node("MarginContainer/VBoxContainer/ChoicePanel3/MarginContainer/HBoxContainer/ChoiceC")]
    public LineEdit _choiceC;
    [Node("MarginContainer/VBoxContainer/ChoicePanel3/MarginContainer/HBoxContainer/CorrectAnswerC")]
    private CheckBox _correctAnswerC;

    [Node("MarginContainer/VBoxContainer/ChoicePanel4/MarginContainer/HBoxContainer/ChoiceD")]
    public LineEdit _choiceD;
    [Node("MarginContainer/VBoxContainer/ChoicePanel4/MarginContainer/HBoxContainer/CorrectAnswerD")]
    private CheckBox _correctAnswerD;

    [Node("MarginContainer/VBoxContainer/MarginContainer")]
    private MarginContainer _errorContainer;

    public string ID;
    private string _correctAnswer = string.Empty;
    
    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    private bool ValidateField(LineEdit field)
    {
        bool isValid = !string.IsNullOrEmpty(field.Text);
        field.Modulate = isValid ? Colors.White : Colors.Red;
        return isValid;
    }

    private bool CanSaveData()
    {
        bool isValid = ValidateField(_questionBox) &&
                        ValidateField(_choiceA) &&
                        ValidateField(_choiceB) &&
                        ValidateField(_choiceC) &&
                        ValidateField(_choiceD) &&
                        !string.IsNullOrEmpty(_correctAnswer);

        _errorContainer.Visible = string.IsNullOrEmpty(_correctAnswer);
        return isValid;
    }

    private void _on_save_button_pressed()
    {
        if (!CanSaveData()) return;

        var data = new Dictionary
        {
            { "ID", ID },
            { "Question", _questionBox.Text },
            { "ChoiceA", _choiceA.Text },
            { "ChoiceB", _choiceB.Text },
            { "ChoiceC", _choiceC.Text },
            { "ChoiceD", _choiceD.Text },
            { "CorrectAnswer", _correctAnswer }
        };

        HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["ADD_QUIZ"], data);
    }

    private void _on_delete_button_pressed()
    {
        var data = new Dictionary { { "ID", ID } };
        HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["DELETE_QUIZ"], data);
        QueueFree();
    }

    public void SetAnswer(string answer)
    {
        var checkBoxes = new[] { _correctAnswerA, _correctAnswerB, _correctAnswerC, _correctAnswerD };
        var answers = new[] { "A", "B", "C", "D" };

        for (int i = 0; i < checkBoxes.Length; i++)
        {
            checkBoxes[i].ButtonPressed = answers[i] == answer;
        }
        _correctAnswer = answer;
    }

    private void HandleAnswerToggle(CheckBox pressedBox, string answer, params CheckBox[] otherBoxes)
    {
        if (!pressedBox.ButtonPressed) return;

        foreach (var box in otherBoxes)
        {
            box.ButtonPressed = false;
        }
        _correctAnswer = answer;
    }

    private void _on_correct_answer_a_toggled(bool buttonPressed) =>
        HandleAnswerToggle(_correctAnswerA, "A", _correctAnswerB, _correctAnswerC, _correctAnswerD);

    private void _on_correct_answer_b_toggled(bool buttonPressed) =>
        HandleAnswerToggle(_correctAnswerB, "B", _correctAnswerA, _correctAnswerC, _correctAnswerD);

    private void _on_correct_answer_c_toggled(bool buttonPressed) =>
        HandleAnswerToggle(_correctAnswerC, "C", _correctAnswerA, _correctAnswerB, _correctAnswerD);

    private void _on_correct_answer_d_toggled(bool buttonPressed) =>
        HandleAnswerToggle(_correctAnswerD, "D", _correctAnswerA, _correctAnswerB, _correctAnswerC);
}
