using Godot;
using System;
using Godot.Collections;
using DialogueManagerRuntime;

public partial class InteractableArea : Area2D
{
	[Export] public bool CanAutoInteract = false;

	[ExportGroup("Dialogue")]
	[Export] public Resource DialogueResource;
	[Export] public string DialogueStart = "start";

	[ExportGroup("CutScene")]
	[Export] Array<Texture> CutSceneImage = new();
	[Export] Array<string> CutSceneText = new();

	[ExportGroup("PopupWindow")]
	[Export(PropertyHint.MultilineText)] public string WindowText = "";

	[ExportGroup("Others")]
	[Export] public bool ShowCodeBlock = false;
	[Export] public bool switchToQuiz = false;

	[Signal] public delegate void InteractedEventHandler(InteractableArea interactable);

	private void _on_area_entered(Area2D area)
	{
		if (CanAutoInteract)
		{
			Interact();
			CanAutoInteract = false;
		}
	}

	void _on_area_exited(Area2D area)
	{
		if (ShowCodeBlock)
		{
			UiManager.Instance.HideAllUI();
		}
	}

	public void Interact()
	{
		GD.Print("Interaction triggered");

		if (DialogueResource != null)
		{
			GD.Print($"Starting dialogue at: {DialogueStart}");
			DialogueManager.ShowDialogueBalloon(DialogueResource, DialogueStart);
			EmitSignal(SignalName.Interacted, this);
			return;
		}

		if (CutSceneImage.Count > 0 || CutSceneText.Count > 0)
		{
			GD.Print("Playing cutscene");
			CutSceneManager.Instance.ShowCutScene(CutSceneImage, CutSceneText);
			EmitSignal(SignalName.Interacted, this);
			return;
		}

		if (!string.IsNullOrEmpty(WindowText))
		{
			GD.Print($"Showing popup: {WindowText}");
			var popupWindow = UiManager.Instance.uiElements["PopupWindow"] as PopupWindow;
			popupWindow.UpateText(WindowText);
			UiManager.Instance.ChangeCurrentUI(popupWindow);
			EmitSignal(SignalName.Interacted, this);
		}

		if (ShowCodeBlock)
		{
			GD.Print("Showing code block");
			GameManager.Instance.LoadScene("uid://4280cgrri3yh");
			// UiManager.Instance.ChangeCurrentUI(UiManager.Instance.uiElements["MainPanel"]);
			EmitSignal(SignalName.Interacted, this);
		}

		if (switchToQuiz)
		{
			GD.Print("Switching to quiz");
			GameManager.Instance.LoadScene("res://Scenes/UI/ActivitesMenu/ActivitiesMenu.tscn");
			EmitSignal(SignalName.Interacted, this);
		}
	}
}
