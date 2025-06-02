using Godot;

[GlobalClass]
public partial class VelocityComponent : Node
{
	[Export]
	public float maxSpeed = 100;
	[Export]
	public float acceleration = 10;

	public Vector2 Velocity;

	public void AccelerateToVelocity(Vector2 targetVelocity)
	{
		Velocity = Velocity.Lerp(targetVelocity, acceleration);
	}

	public void Decelerate()
	{
		AccelerateToVelocity(Vector2.Zero);
	}

	public Vector2 GetVelocity(Vector2 direction)
	{
		return direction * maxSpeed;
	}

	public void SetMaxSpeed(float newMaxSpeed)
	{
		maxSpeed = newMaxSpeed;
	}

	public void Move(CharacterBody2D character)
	{
		character.Velocity = Velocity;
		character.MoveAndSlide();
	}
}
