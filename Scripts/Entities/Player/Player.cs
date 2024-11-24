using Godot;
using System;

[GlobalClass, Icon("Assets/Entities/Player/Player.png")]
public partial class Player : CharacterBody2D
{   
	private Vector2 AnimationDirection = Vector2.Down;
	public Vector2 Direction = Vector2.Zero;
	
	private AnimationPlayer AnimationPlayer;
	private Sprite2D sprite;
	private Node stateManager;
	
	/**
	 * This function loads all the necessary assets to the RAM
	 * for faster rendering.
	 */
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		stateManager = GetNode<Node>("PlayerStateManager");
		
		UpdateAnimation("idle");
	}
	
	/**
	 * This function runs the game.
	 * <para>This also has a built-in FPS computation.</para>
	 */
	public override void _Process(double delta)
	{
		Direction = new Vector2
		(
			Input.GetAxis("Left", "Right"),
			Input.GetAxis("Up", "Down")
		).Normalized();
	}
	
	/**
	 * This function handles the physics of the game.
	 */
	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}
	
	/**
	 * This function sets your direction.
	 * <para>? is like if-else statement.</para>
	 */
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
		AnimationPlayer?.Play(state + "_" + _AnimationDirection());
	}
}
