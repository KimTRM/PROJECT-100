using Godot;

public partial class State : Node
{
	// Called when entering the state
    public virtual void Enter()
    {}

    // Called when exiting the state
    public virtual void Exit()
    {}

    // Called every frame
    public virtual void Update(double delta)
    {}

    // Called during the physics step
    public virtual void PhysicsUpdate(double delta)
    {}

    public virtual void HandleInput(InputEvent @event)
    {}
}
