using System;
using Godot;
using GodotUtilities;

[Scene]
public partial class CutSceneLoader : CanvasLayer
{
    [Node("MarginContainer/CutSceneContainer")]
    private TextureRect cutSceneContainer;
    [Node("MarginContainer2/DialougeText")]
    private RichTextLabel dialougeText;
    [Node("LetterDisplayTimer")]
    public Timer letterDisplayTimer;

    public static int MAX_WIDTH = 256;

    private string Text = "";
    private int LetterIndex = 0;

    private double LetterTime = 0.03;
    private double SpaceTime = 0.06;
    private double PuncutationTime = 0.8;

    [Signal] public delegate void FinishedDisplayingEventHandler();

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes(); 
        }
    }

    public override void _Ready()
    {
        letterDisplayTimer.Timeout += OnLetterDisplayTimeout;
    }

    public void DisplayText(string textToDisplay)
    {
        Text = textToDisplay;
        LetterIndex = 0;
        dialougeText.Text = "";
        letterDisplayTimer.Start(LetterTime);
    }

    private void OnLetterDisplayTimeout()
    {
        if (LetterIndex < Text.Length)
        {
            char currentChar = Text[LetterIndex];
            dialougeText.Text += currentChar;
            LetterIndex++;

            if (currentChar == ' ')
            {
                letterDisplayTimer.Start(SpaceTime);
            }
            else if (".,!?".Contains(currentChar))
            {
                letterDisplayTimer.Start(PuncutationTime);
            }
            else
            {
                letterDisplayTimer.Start(LetterTime);
            }
        }
        else
        {
            letterDisplayTimer.Stop();
            EmitSignal(SignalName.FinishedDisplaying);
        }
    }

    public void ChangeTexture(string texturePath)
    {
        Texture2D texture = (Texture2D)GD.Load(texturePath);
        cutSceneContainer.Texture = texture;
    }

}
