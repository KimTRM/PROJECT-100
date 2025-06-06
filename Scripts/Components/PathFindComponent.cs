using Godot;
using System;

[GlobalClass]
public partial class PathFindComponent : Node2D
{
	[Export] private VelocityComponent _velocityComponent;
	[Export] private bool _debugDrawEnable;

	public NavigationAgent2D navigationAgent2D;
	private Timer intervalTimer;

	public override void _Ready()
	{
		InitiatizeNodes();
		SetProcess(OS.IsDebugBuild() && _debugDrawEnable);
	}

	public override void _Draw()
	{
		if (OS.IsDebugBuild() && _debugDrawEnable)
		{
			foreach (Vector2 Point in navigationAgent2D.GetCurrentNavigationPath())
			{
				DrawCircle(ToLocal(Point), 1f, Colors.Orange);

			}
		}
	}

	public override void _Process(double delta)
	{
		QueueRedraw();
	}

	private void InitiatizeNodes()
	{
		navigationAgent2D = new NavigationAgent2D();
		AddChild(navigationAgent2D);
		navigationAgent2D.VelocityComputed += OnVelocityComputed;

		intervalTimer = new Timer();
		AddChild(intervalTimer);
	}

	public void SetTargetPosition(Vector2 targetPosition)
	{
		// if (!intervalTimer.IsStopped()) return;

		intervalTimer.Start();
		navigationAgent2D.TargetPosition = targetPosition;
	}

	public void FollowPath(double delta)
	{
		if (navigationAgent2D.IsNavigationFinished())
		{
			_velocityComponent.Decelerate((float)delta);
			return;
		}

		Vector2 direction = (navigationAgent2D.GetNextPathPosition() - GlobalPosition).Normalized();
		_velocityComponent.AccelerateToVelocity(_velocityComponent.GetVelocity(direction), (float)delta);
		navigationAgent2D.Velocity = _velocityComponent.Velocity;
	}

	private void OnVelocityComputed(Vector2 velocity)
	{
		var newDirection = velocity.Normalized();
		var currentDirection = _velocityComponent.Velocity.Normalized();
		var halfway = newDirection.Lerp(currentDirection, _velocityComponent.acceleration);
		_velocityComponent.Velocity = halfway * _velocityComponent.Velocity.Length();
	}
}
