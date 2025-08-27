using Godot;
using System;

public partial class PlayerManager : Node
{
    public static PlayerManager Instance { get; private set; }

    private PackedScene PLAYER = (PackedScene)GD.Load("uid://b65nxfrri78o0");
    public Player player;
    public bool playerSpawned = false;

    public override void _Ready()
    {
        Instance = this;

        AddPlayerInstance();
    }

    private void AddPlayerInstance()
    {
        player = (Player)PLAYER.Instantiate();
        AddChild(player);
    }

    public void SetPlayerPosition(Vector2 newPos)
    {
        player.GlobalPosition = newPos;
    }

    public void SetAsParent(Node2D parent)
    {
        if (player.GetParent() != null)
        {
            player.GetParent().RemoveChild(player);
        }
        parent.CallDeferred("add_child", player);
    }

    public void UnparentPlayer(Node2D parent)
    {
        parent.RemoveChild(player);
        AddChild(player);
    }
}
