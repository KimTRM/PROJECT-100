using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class ConditionBlock : CodeBlock
{
	[Node] private TemplateEditor TemplateEditor;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes();
			TemplateEditor.DragStarted += StartDrag;
		}
	}


}
