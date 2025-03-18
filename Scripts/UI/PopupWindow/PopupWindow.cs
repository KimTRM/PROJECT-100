using Godot;
using GodotUtilities;
using Godot.Collections;
using System;

[Scene]
public partial class PopupWindow : CanvasLayer
{
	[Node("PanelContainer/Label")]
	private RichTextLabel label;

	[Export(PropertyHint.MultilineText)]
	public string Text;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
	}

	public override void _Ready()
	{
		label.Text = Text;
	}

	public void UpateText(string text)
	{
		label.Text = text;
	}

}
