using Godot;
using System;

public class Game : Node2D
{
#pragma warning disable 649
	[Export]
	public PackedScene gunScene;
#pragma warning restore 649

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    	GunInstance();
    }

    void GunInstance()
    {
    	for(int i = 1; i < 4; i++)
    	{
    		// instancing guns

    		var gun = (Gun)gunScene.Instance();
    		AddChild(gun);

    		var ground = GetNode<Node2D>("Ground");
    		if(i == 1)
    		{
    			gun.Position = ground.GetNode<Position2D>("Gun 1").GlobalPosition;
    		}
    		if(i == 2)
    		{
    			gun.Position = ground.GetNode<Position2D>("Gun 2").GlobalPosition;
    		}
    		if(i == 3)
    		{
    			gun.Position = ground.GetNode<Position2D>("Gun 3").GlobalPosition;
    		}
    		Vector2 gunPos = gun.Position;
    		gunPos.y -= 30;
    		gun.Position = gunPos;

    		gun.GetNode<Sprite>("Gun").Texture = (Texture)ResourceLoader.Load("res://Sprites/Launcher/Gun.png");

    		// instancing rocket launcers
    		Sprite launcher = new Sprite();
    		AddChild(launcher);
    		launcher.Texture = (Texture)ResourceLoader.Load("res://Sprites/Launcher/Launcher.png");
    		launcher.Scale = new Vector2(0.1f, 0.1f);
    		if(i == 1)
    			launcher.Position = ground.GetNode<Position2D>("Gun 1").GlobalPosition;
    		if(i == 2)
    			launcher.Position = ground.GetNode<Position2D>("Gun 2").GlobalPosition;
    		if(i == 3)
    			launcher.Position = ground.GetNode<Position2D>("Gun 3").GlobalPosition;
    		Vector2 launPos = launcher.Position;
    		launPos.y -= 7;
    		launcher.Position = launPos;
    	}
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	    //GetNode<
	}
}
