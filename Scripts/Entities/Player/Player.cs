using Godot;
using System;

[GlobalClass, Icon("Assets/Entities/Player/Player.png")]
public partial class Player : CharacterBody2D
{   
	public Vector2 AnimationDirection = Vector2.Down;
	public Vector2 Direction = Vector2.Zero;
	
	private AnimationPlayer animationPlayer;
	private Sprite2D sprite;
	private Node stateManager;

	public static Player playerInstance;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		stateManager = GetNode<Node>("PlayerStateManager");
		
		playerInstance = this;

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

		if (Direction.X != 0)
		{
			newDirection = Direction.X < 0 ? Vector2.Left : Vector2.Right;
		}   
		else if (Direction.Y != 0)
		{
			newDirection = Direction.Y < 0 ? Vector2.Up : Vector2.Down;
		}
		
		if (Direction == Vector2.Zero && AnimationDirection == newDirection)
		{
			return false;
		}

		AnimationDirection = newDirection;
		
		sprite.FlipH = AnimationDirection == Vector2.Left ? true : false;
		// sprite.Scale = new Vector2( AnimationDirection == Vector2.Left ? -1 : 1 , 1);
		
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
		var health = GameManager.gameManager.health;

		GD.Print($"Health: {health}");
	}
}
