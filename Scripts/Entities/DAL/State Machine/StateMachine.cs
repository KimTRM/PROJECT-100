using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	[Export] public NodePath initialStatePath;

	private Dictionary<string, DalState> _states;
	private DalState _currentDalState;
	
	public override void _Ready()
	{
		// Initialize the states dictionary
		_states = new Dictionary<string, DalState>();

		// Iterate over all child nodes and add them to the states dictionary if they are State instances
		foreach (Node node in GetChildren())
		{
			if (node is DalState state)
			{
				_states[node.Name] = state;
				state.stateMachine = this;
				state.DAL = GetParent<DAL>(); // Assuming Player is the parent node
				
				state.Exit();
			}
		}

		// Get the initial state from the provided node path
		_currentDalState = GetNode<DalState>(initialStatePath);

		
		_currentDalState.Enter();
	}

	public override void _Process(double delta)
	{
		_currentDalState.Update((float)delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentDalState.PhysicsUpdate((float)delta);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		_currentDalState.HandleInput(@event);
	}

	public void TransitionTo(string key)
	{
		// Check if the state exists and if it is different from the current state
		if (_states.ContainsKey(key) && _currentDalState != _states[key])
		{
			_currentDalState.Exit();
			_currentDalState = _states[key];
			_currentDalState.Enter();
		}
	}
}
