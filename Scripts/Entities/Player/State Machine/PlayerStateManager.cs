using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerStateManager : Node
{
	[Export] public NodePath initialStatePath;

	private Dictionary<string, PlayerState> _states;
	private PlayerState _currentPlayerState;

	public override void _Ready()
	{
		// Initialize the states dictionary
		_states = new Dictionary<string, PlayerState>();

		// Iterate over all child nodes and add them to the states dictionary if they are State instances
		foreach (Node node in GetChildren())
		{
			if (node is PlayerState state)
			{
				_states[node.Name] = state;
				state.sm = this;
				state.player = GetParent<Player>(); // Assuming Player is the parent node
				
				state.Exit();
			}
		}

		// Get the initial state from the provided node path
		_currentPlayerState = GetNode<PlayerState>(initialStatePath);

		
		_currentPlayerState.Enter();
	}

	public override void _Process(double delta)
	{
		_currentPlayerState.Update((float)delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentPlayerState.PhysicsUpdate((float)delta);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		_currentPlayerState.HandleInput(@event);
	}

	public void TransitionTo(string key)
	{
		// Check if the state exists and if it is different from the current state
		if (_states.ContainsKey(key) && _currentPlayerState != _states[key])
		{
			_currentPlayerState.Exit();
			_currentPlayerState = _states[key];
			_currentPlayerState.Enter();
		}
	}
}
