using Godot;
using GodotUtilities;
using Godot.Collections;

[Scene]
public partial class SignupPanel : PanelContainer
{
    [Node("MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/HBoxContainer/UsernamePanel2/MarginContainer/VBoxContainer/FirstName")]
    private LineEdit Firstname;

    [Node("MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/HBoxContainer/UsernamePanel3/MarginContainer/VBoxContainer/LastName")]
    private LineEdit Lastname;

    [Node("MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/UsernamePanel/MarginContainer/VBoxContainer/UserName")]
    private LineEdit Username;

    [Node("MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/PasswordPanel/MarginContainer/VBoxContainer/Password")]
    private LineEdit Password;

    [Node("MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/PasswordPanel2/MarginContainer/VBoxContainer/ConfirmPassword")]
    private LineEdit ConfirmPassword;

    [Node("MarginContainer/VBoxContainer/MarginContainer2/VBoxContainer/ErrorMessage")]
    private Label ErrorMessage;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    private void ErrorTrapping()
    {
        Firstname.Modulate = string.IsNullOrEmpty(Firstname.Text) ? new Color(1, 0, 0) : new Color(1, 1, 1);
        Lastname.Modulate = string.IsNullOrEmpty(Lastname.Text) ? new Color(1, 0, 0) : new Color(1, 1, 1);
        Username.Modulate = string.IsNullOrEmpty(Username.Text) ? new Color(1, 0, 0) : new Color(1, 1, 1);
        Password.Modulate = string.IsNullOrEmpty(Password.Text) ? new Color(1, 0, 0) : new Color(1, 1, 1);
        ConfirmPassword.Modulate = string.IsNullOrEmpty(ConfirmPassword.Text) || ConfirmPassword.Text != Password.Text ? new Color(1, 0, 0) : new Color(1, 1, 1);
    }

    private bool CanSaveData()
    {
        ErrorTrapping();
        if (string.IsNullOrEmpty(Firstname.Text) || string.IsNullOrEmpty(Lastname.Text) || string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(Password.Text) || string.IsNullOrEmpty(ConfirmPassword.Text) || ConfirmPassword.Text != Password.Text)
        {
            ErrorMessage.Show();
            ErrorMessage.Text = "Please fill out all fields";
            return false;
        }
        else
        {
            ErrorMessage.Hide();
            return true;
        }
    }

    public void _on_signup_button_pressed()
    {
        if (!CanSaveData()) return;

        var data = new Dictionary
            {
                { "UserID", HTTPManager.Instance.GenarateId()},
                { "FirstName", Firstname.Text },
                { "LastName", Lastname.Text },
                { "UserName", Username.Text },
                { "Password", Password.Text },
                { "Role", "Student" }
            };
        HTTPManager.Instance.QueueRequest(HTTPManager.Instance.Commands["ADD_USER_ACCOUNT"], data);
        Hide();
    }
}
