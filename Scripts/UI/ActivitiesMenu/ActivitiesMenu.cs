using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class ActivitiesMenu : MarginContainer
{
    [Node("MarginContainer/ActivitiesContainer")]
    public HFlowContainer activitiesContainer;

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }


}
