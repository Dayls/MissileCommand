using Godot;
using System;

public class EnemyMissile : RigidBody2D
{
    [Export]
    public int minSpeed;
    [Export]
    public int maxSpeed;

    [Signal]
    public delegate void CitieHit();

    string source = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void die()
    {
        if(source == null)
        {
            var explode = (Explode)GetNode("/root/Explode");
            explode.startExplosion(GlobalPosition, "player", (int)GD.RandRange(0.25, 1.5));
            QueueFree();
        }
        else
        {
            var explode = (Explode)GetNode("/root/Explode");
            explode.startExplosion(GlobalPosition, "enemy", (int)GD.RandRange(0.1, 0.5));
            QueueFree();
        }
    }

    void _on_Area2D_body_entered(PhysicsBody2D body)
    {
        source = "enemy";
        if(body.IsInGroup("Citie"))
        {
            die();
            // add score system ( TODO )
        }
        if(body.IsInGroup("Ground"))
        {
            die();
        }
        if(body.IsInGroup("Gun"))
        {
            die();
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
