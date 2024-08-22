using Godot;
using System;

[GlobalClass, Icon("Entities/Player/Sprites/Player.png")]
public partial class player : CharacterBody2D
{   
	public Vector2 AnimationDirection = Vector2.Down;
	public Vector2 Direction = Vector2.Zero;
	
	private AnimationPlayer animationPlayer;
	private Sprite2D sprite;
	private Node stateManager;
	private PlayerStateManager sm;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		stateManager = GetNode<Node>("PlayerStateManager");
		
		UpdateAnimation("idle");
	}

	public override void _Process(double delta)
	{
		Direction = new Vector2
		(
			Input.GetAxis("Left", "Right"),
			Input.GetAxis("Up", "Down")
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
		
		//sprite.Scale.X = AnimationDirection == Vector2.LEFT ? 1 : -1;
		if (AnimationDirection == Vector2.Left)
		{
			sprite.Scale = new Vector2(-1, 1);
		}
		else
		{
			sprite.Scale = new Vector2(1,1);
		}
		
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
		animationPlayer?.Play(state + "_" + _AnimationDirection());
	}
}
