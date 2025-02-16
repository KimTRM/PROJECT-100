using Godot;
using Godot.Collections;

public partial class StateMachine : Node
{
	[Export] 
	public NodePath initialStatePath;

    [Export] 
	private Dictionary<string, State> states = new Dictionary<string, State>();
	
	private State currentState;

	public override void _Ready()
    {
		foreach (Node node in GetChildren())
		{
			if (node is State state)
			{
				states[node.Name] = state;
				
				state.Exit();
			}
		}

		currentState = GetNode<State>(initialStatePath);

		
		currentState.Enter();
    }

    public override void _Input(InputEvent @event)
    {
        currentState.HandleInput(@event);
    }

    public override void _Process(double delta)
    {
        currentState.Update(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        currentState.PhysicsUpdate(delta);
    }

    public void Transition(string StateName)
    {
        if (states.ContainsKey(StateName) && currentState != states[StateName])
		{
			currentState.Exit();
			currentState = states[StateName];
			currentState.Enter();
		}
    }
}
