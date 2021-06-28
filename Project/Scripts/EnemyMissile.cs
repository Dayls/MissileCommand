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
            explode.startExplosion(GlobalPosition, source, (int)GD.RandRange(0.1, 0.5));
            QueueFree();
        }
    }

    void _on_Area2D_body_entered(PhysicsBody2D body)
    {
        if(body.IsInGroup("Citie"))
        {
            Connect("CitieHit", body, "pop");
            EmitSignal("CitieHit");
            source = "enemy";
            die();
            // add score system ( TODO )
        }
        if(body.IsInGroup("Ground"))
        {
            source = "enemy";
            die();
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
