using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class UiManager : CanvasLayer
{
	[Node] private CanvasLayer PauseMenu;
	[Node] private CanvasLayer SettingsMenu;
	[Node] private CanvasLayer CutSceneLoader;
	[Node] private CanvasLayer PopupWindow;
	[Node] private CanvasLayer SceneTransition;

    public override void _Notification(int what)
    {
        if(what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

    public override void _Ready()
	{
		
	}

	public void ChangeCurrentUI(CanvasLayer newUI)
	{
		
	}
}
