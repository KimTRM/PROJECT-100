using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerStateManager : Node
{
	[Export] public NodePath initialStatePath;

	private Dictionary<string, State> _states;
	private State _currentState;

	public override void _Ready()
	{
		// Initialize the states dictionary
		_states = new Dictionary<string, State>();

		// Iterate over all child nodes and add them to the states dictionary if they are State instances
		foreach (Node node in GetChildren())
		{
			if (node is State state)
			{
				_states[node.Name] = state;
				state.sm = this;
				state.player = GetParent<player>(); // Assuming Player is the parent node
				
				state.Exit();
			}
		}

		// Get the initial state from the provided node path
		_currentState = GetNode<State>(initialStatePath);

		
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
