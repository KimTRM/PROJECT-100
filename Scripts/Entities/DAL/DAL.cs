using Godot;
using System;

[GlobalClass, Icon("Assets/Entities/DAL/DAL.png")]
public partial class DAL : CharacterBody2D
{
	public Vector2 AnimationDirection = Vector2.Down;
	public Vector2 Direction = Vector2.Zero;
	
	private AnimationPlayer _animationPlayer;
	private Sprite2D _sprite;
	private Node _stateMachine;
	
	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_stateMachine = GetNode<Node>("State Machine");
		UpdateAnimation("idle");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Direction = new Vector2
		(
			Input.GetAxis("ui_left", "ui_right"),
			Input.GetAxis("ui_up", "ui_down")
		).Normalized();
	}

	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

	public bool SetDirection()
	{
		Vector2 newDirection = AnimationDirection;

		if (Direction == Vector2.Zero) 
		{ 
			return false; 
		}

		if (Direction.X != 0)
		{
			newDirection = Direction.X < 0 ? Vector2.Left : Vector2.Right;
		}   
		else if (Direction.Y != 0)
		{
			newDirection = Direction.Y < 0 ? Vector2.Up : Vector2.Down;
		}
		
		if (AnimationDirection == newDirection)
		{
			return false;
		}

		AnimationDirection = newDirection;
		
		_sprite.FlipH = AnimationDirection == Vector2.Left ? true : false;
		
		return true;
	}

	private string _AnimationDirection()
	{
		if (AnimationDirection == Vector2.Down)
		{
			return "down";
		}
		else if (AnimationDirection == Vector2.Up)
		{
			return "up";
		}
		else
		{
			return "side";
		}
	}

	public void UpdateAnimation(string state)
	{
		_animationPlayer?.Play(state + "_" + _AnimationDirection());
	}
}
