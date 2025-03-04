using Godot;
using GodotUtilities;
using System;
using DialogueManagerRuntime;

[Scene]
public partial class CutSceneLoader : CanvasLayer
{
    [Node("MarginContainer2/DialougeText")]
    private RichTextLabel dialougeText;
    [Node("LetterDisplayTimer")]
    private Timer letterDisplayTimer;

    public static int MAX_WIDTH = 256;

    private string Text = "";
    private int LetterIndex = 0;

    private double LetterTime = 0.03;
    private double SpaceTime = 0.06;
    private double PuncutationTime = 0.2;

    public delegate void FinishedDisplayingEventHandler();

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public void DisplayText(string textToDisplay)
    {
        Text = textToDisplay;
        LetterIndex = 0;
        dialougeText.Text = "";
        letterDisplayTimer.Start();
    }

}
