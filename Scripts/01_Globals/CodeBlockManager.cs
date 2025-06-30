using Godot;
using System;

public partial class CodeBlockManager : Node
{
	[Signal] public delegate void DragManagerReadyEventHandler(DragManager dragManager);

	public static CodeBlockManager Instance { get; private set; }
	[Export]
	public DragManager dragManager;

	public override void _Ready()
	{
		Instance = this;
		DragManagerReady += OnDragManagerReady;
	}

	private void OnDragManagerReady(DragManager dragManager)
	{
		this.dragManager = dragManager;
	}
}
