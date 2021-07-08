using Godot;
using System;

public class Missile : Node2D
{
    Vector2 targetPos = new Vector2(0, 0);
    [Export]
    int speed = 500;
    bool canMove = true;

    Node XHolder;

#pragma warning disable 649
    [Export]
    PackedScene ExplosionScene;
#pragma warning restore 649
    bool canExplode = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if(GetNode<Particles2D>("Fire").Visible == true)
            GetNode<Particles2D>("Fire").Emitting = true;

        XHolder = GetNode<Node>("XHolder");
    }

    public void moveToTarget(Vector2 target)
    {
    	targetPos = target;
    }

    public override void _PhysicsProcess(float delta)
    {
        if(targetPos != new Vector2(0,0))
        {
            //XHolder.RotationDegrees = 0;
            XHolder.GetNode<Sprite>("X").Position = targetPos;
            XHolder.GetNode<Sprite>("X").Show();
        }
    	if(canMove)
    	{
    		LookAt(targetPos);
    		RotationDegrees += 90;
    		var direction = (targetPos - GlobalPosition).Normalized();
	    	var motion = direction * speed * delta;
	    	GlobalPosition += motion;
    	}

    	if( new Vector2(GlobalPosition - targetPos ).y < 1.5f)
    		die();
    }

    void die()
    {
        XHolder.GetNode<Sprite>("X").Hide();

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
            var explode = (Explode)GetNode("/root/Explode");
			explode.startExplosion(GlobalPosition, "player", (int)1.25);
		}
    }

    void OnDestroyTimeout()
    {
    	CallDeferred("free");
    }

    

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
