using Godot;
using System;

public class MissileTrail : Godot.Line2D
{
    //Color playerColor = new Color(0.37f, 0.43f, 0.91f, 1);
    //Color enemycolor = new Color(1, 0.36f, 0.8f, 1);

	NodePath target;
    Vector2 point;
    [Export]
    NodePath targetPath;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    	target = GetNode<NodePath>("targetPath");
       //SetAsToplevel(true);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	   point = target.GlobalPosition;
	   AddPoint(point);	
	}
}
