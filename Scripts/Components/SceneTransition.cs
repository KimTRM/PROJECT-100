using Godot;
using GodotUtilities;
using System;
using System.Threading.Tasks;

[Scene]
public partial class SceneTransition : CanvasLayer
{
	[Node ("Control/AnimationPlayer")]
	private AnimationPlayer _animationPlayer;

	public override void _Notification(int what)
    {
        if(what == NotificationSceneInstantiated)
		{
			WireNodes();
		}
    }

	public override void _Ready()
	{
		Hide();
	}
	
	public async Task FadeIn()
	{
		_animationPlayer.Play("FadeIn");
		await ToSignal(_animationPlayer, "animation_finished");
	}

	public async Task FadeOut()
	{
		_animationPlayer.Play("FadeOut");
		await ToSignal(_animationPlayer, "animation_finished");
	}
}
