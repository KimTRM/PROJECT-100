using Godot;
using GodotUtilities;
using System.Collections.Generic;

[Scene]
public partial class UiManager : CanvasLayer
{
	public static UiManager Instance { get; private set; }
    [Node] private CanvasLayer PauseMenu;
    [Node] private CanvasLayer SettingsMenu;
    [Node] private CanvasLayer CutSceneLoader;
    [Node] private CanvasLayer PopupWindow;
    [Node] private CanvasLayer SceneTransition;

    public Dictionary<string, CanvasLayer> uiElements;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
		Instance = this;
        InitializeUIElements();
    }

    private void InitializeUIElements()
    {
        uiElements = new Dictionary<string, CanvasLayer>
        {
            { "PauseMenu", PauseMenu },
            { "SettingsMenu", SettingsMenu },
            { "CutSceneLoader", CutSceneLoader },
            { "PopupWindow", PopupWindow },
            { "SceneTransition", SceneTransition }
        };

        HideAllUI();
    }

    private void HideAllUI()
    {
        foreach (var uiElement in uiElements.Values)
        {
            if (uiElement != null)
			{
				uiElement.Visible = false;
				uiElement.Layer = 0;
			}
        }
    }

    public void ChangeCurrentUI(CanvasLayer newUI)
    {
        if (newUI == null || !uiElements.ContainsKey(newUI.Name))
        {
            GD.PrintErr($"UI Manager: Attempted to change to an invalid UI: {newUI?.Name ?? "null"}");
            return;
        }

        HideAllUI();
        uiElements[newUI.Name].Visible = true;
		uiElements[newUI.Name].Layer = Layer;
    }
}
