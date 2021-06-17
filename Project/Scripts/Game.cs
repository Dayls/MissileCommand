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
    		var gun = (Gun)gunScene.Instance();
    		AddChild(gun);

    		var ground = GetNode<Node2D>("Ground");
    		if(i == 1)
    		{
    			gun.Position = ground.GetNode<Position2D>("Gun 1").GlobalPosition;
    			gun.GetNode<Sprite>("Gun").Texture = (Texture)ResourceLoader.Load("res://Sprites/Guns/Gun 1.png");
    		}
    		if(i == 2)
    		{
    			gun.Position = ground.GetNode<Position2D>("Gun 2").GlobalPosition;
    			gun.GetNode<Sprite>("Gun").Texture = (Texture)ResourceLoader.Load("res://Sprites/Guns/Gun 2.png");
    		}
    		if(i == 3)
    		{
    			gun.Position = ground.GetNode<Position2D>("Gun 3").GlobalPosition;
    			gun.GetNode<Sprite>("Gun").Texture = (Texture)ResourceLoader.Load("res://Sprites/Guns/Gun 1.png");
    			gun.GetNode<Sprite>("Gun").FlipH = true;
    		}
    	}
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
	    //GetNode<
	}
}
