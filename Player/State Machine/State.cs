using Godot;
using System;

public partial class State : Node
{
    public PlayerStateManager sm;
    public player player;

    // Called when the state is ready

    // Called when entering the state
    public virtual void Enter()
    {
        // Logic for entering the state
    }

    // Called when exiting the state
    public virtual void Exit()
    {
        // Logic for exiting the state
    }

    // Called every frame
    public virtual void Update(float delta)
    {
        // Logic for updating the state
    }

    // Called during the physics step
    public virtual void PhysicsUpdate(float delta)
    {
        // Logic for physics update of the state
    }

    // Called when handling input
    public virtual void HandleInput(InputEvent @event)
    {
        // Logic for handling input in the state
    }
}
