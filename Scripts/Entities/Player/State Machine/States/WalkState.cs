using Godot;
using System;

public partial class WalkState : State
{
	[Export (PropertyHint.Enum, "Slow: 150, Normal: 200, Fast: 500")] 
	public int Speed = 200;

	[Export (PropertyHint.Enum, "White,Yellow,Orange")]
	string color;

	[Export]
	public Godot.Collections.Dictionary pointsDict = new Godot.Collections.Dictionary
	{
    {"White", 50},
    {"Yellow", 75},
    {"Orange", 100},
	{4, new []{1,2,3}},
    {7, "Hello"},
    {"sub_dict", new Godot.Collections.Dictionary{{"sub_key", "Nested value"}}}
	};

Variant foo = 5;


	public override void Enter()
	{
		player.UpdateAnimation("walk");

		int points = (int)pointsDict[color];
		
		GD.Print($"Points: {points}");

		switch (foo.VariantType)
{
    	case Variant.Type.Nil:
        GD.Print("foo is null");
        break;
  		case Variant.Type.Int:
        GD.Print("foo is an integer");
        break;
    	case Variant.Type.Object:
        // Note that Objects are their own special category.
        // You can convert a Variant to a GodotObject and use reflection to get its name.
        GD.Print($"foo is a(n) {foo.AsGodotObject().GetType().Name}");
        break;
}
	}

	public override void Update(float delta)
	{
		if (player.Direction == Vector2.Zero)
		{
			sm.TransitionTo("Idle");
		}

		player.Velocity = player.Direction * Speed;

		if (player.SetDirection())
		{
			player.UpdateAnimation("walk");
		}
	}
}
