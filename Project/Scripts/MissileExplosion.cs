using Godot;
using System;

public class MissileExplosion : Sprite
{
    string source;

    [Signal]
    public delegate void EnemyHit();

    [Signal]
    public delegate void CitieHit();

    [Signal]
    public delegate void GunHit();

    Game mainNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Explode");
        mainNode = (Game)GetTree().Root.GetNode<Node2D>("Game");

        PackedScene sound = (PackedScene)ResourceLoader.Load("res://Scenes/Explosion sound.tscn");
        AudioStreamPlayer explosionSound = (AudioStreamPlayer)sound.Instance();
        AddChild(explosionSound);
        explosionSound.Play();
    }

    void die()
    {
    	QueueFree();
    }

    public void setSource(string sourceString)
    {
        source = sourceString;
    }

    void _on_Area2D_body_entered(PhysicsBody2D body)
    {
        if(source == "player")
        {
            if(body.IsInGroup("Enemy"))
            {
                mainNode.ChangeScore(25);
                Connect("EnemyHit", body, "die");
                EmitSignal("EnemyHit");
            }
        }
        if(source == "enemy")
        {
            if(body.IsInGroup("Citie"))
            {
                if(!IsConnected("CitieHit", body, "pop"))
                {
                    Connect("CitieHit", body, "pop");
                    EmitSignal("CitieHit");
                }
            }
            if(body.IsInGroup("Gun"))
            {
                if(!IsConnected("GunHit", body, "die"))
                {
                    Connect("GunHit", body, "die");
                    EmitSignal("GunHit");
                }
            }
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
