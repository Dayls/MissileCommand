using Godot;
using System;

public class Missile : Node2D
{
    Vector2 targetPos;
    [Export]
    int speed = 500;
    bool canMove = true;

#pragma warning disable 649
    [Export]
    PackedScene ExplosionScene;
#pragma warning restore 649
    bool canExplode = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Particles2D>("Fire").Emitting = true;
    }

    public void moveToTarget(Vector2 target)
    {
    	targetPos = target;
    }

    public override void _PhysicsProcess(float delta)
    {
    	if(canMove)
    	{
    		LookAt(targetPos);
    		RotationDegrees += 90;
    		var direction = (targetPos - GlobalPosition).Normalized();
	    	var motion = direction * speed * delta;
	    	GlobalPosition += motion;
    	}

    	if( new Vector2(GlobalPosition - targetPos ).y < 1.5f)
    		destroy();
    }

    void destroy()
    {
    	canMove = false;
    	GetNode<Particles2D>("Fire").OneShot = true;
    	Timer timer = new Timer();
    	AddChild(timer);
    	timer.OneShot = true;
    	timer.WaitTime = 0.5f;
		timer.Connect("timeout", this, "OnDestroyTimeout");
		timer.Start();

		if(canExplode)
		{
			canExplode = false;
			startExplosion();
		}
    }

    void OnDestroyTimeout()
    {
    	CallDeferred("free");
    }

    void startExplosion()
    {
    	MissileExplosion explosion = (MissileExplosion)ExplosionScene.Instance();
    	GetTree().Root.AddChild(explosion);
    	explosion.GlobalPosition = GlobalPosition;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
