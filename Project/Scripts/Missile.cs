using Godot;
using System;

public class Missile : Node2D
{
    Vector2 targetPos;
    [Export]
    int speed = 500;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void moveToTarget(Vector2 target)
    {
    	targetPos = target;
    }

    public override void _PhysicsProcess(float delta)
    {
    	LookAt(targetPos);
    	RotationDegrees += 90;
    	
    	var direction = (targetPos - GlobalPosition).Normalized();
    	var motion = direction * speed * delta;
    	GlobalPosition += motion;

    	if( new Vector2(GlobalPosition - targetPos).y < 1)
    		CallDeferred("free");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
