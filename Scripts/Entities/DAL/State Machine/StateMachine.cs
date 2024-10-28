using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	[Export] public NodePath initialStatePath;

	private Dictionary<string, States> _states;
	private States _currentState;
	
	public override void _Ready()
	{
		// Initialize the states dictionary
		_states = new Dictionary<string, States>();

		// Iterate over all child nodes and add them to the states dictionary if they are State instances
		foreach (Node node in GetChildren())
		{
			if (node is States state)
			{
				_states[node.Name] = state;
				state.stateMachine = this;
				state.DAL = GetParent<DAL>(); // Assuming Player is the parent node
				
				state.Exit();
			}
		}

		// Get the initial state from the provided node path
		_currentState = GetNode<States>(initialStatePath);

		
		_currentState.Enter();
	}

	public override void _Process(double delta)
	{
		_currentState.Update((float)delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentState.PhysicsUpdate((float)delta);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		_currentState.HandleInput(@event);
	}

	public void TransitionTo(string key)
	{
		// Check if the state exists and if it is different from the current state
		if (_states.ContainsKey(key) && _currentState != _states[key])
		{
			_currentState.Exit();
			_currentState = _states[key];
			_currentState.Enter();
		}
	}
}
