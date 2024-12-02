using Godot;
using System;

public partial class PlayerState : Node
{
    public PlayerStateManager sm;
    public Player player;

    // Called when entering the state
    public virtual void Enter()
    {}

    // Called when exiting the state
    public virtual void Exit()
    {}

    // Called every frame
    public virtual void Update(float delta)
    {}

    // Called during the physics step
    public virtual void PhysicsUpdate(float delta)
    {}

    public virtual void HandleInput(InputEvent @event)
    {}
}
