using Godot;
using Godot.Collections;

[GlobalClass]
public partial class StateMachine : Node
{
    [Export] 
	private Dictionary<string, State> states = new Dictionary<string, State>();
	
	private State currentState;
	
    public void StartMachine(Dictionary<string, State> initStates)
    {
        currentState = initStates["IdleState"];

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
