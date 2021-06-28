using Godot;
using System;

public class Game : Node2D
{
#pragma warning disable 649
	[Export]
	public PackedScene gunScene;
	[Export]
	public PackedScene enemyScene;
#pragma warning restore 649

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    	GD.Randomize();
    	GunInstance();
    	GetNode<Timer>("StartTimer").Start();
    }

    void OnStartTimerTimeout()
    {
    	GetNode<Timer>("EnemyTimer").WaitTime = RandomNumberGen();
    	GetNode<Timer>("EnemyTimer").Start();
    }

    void OnEnemyTimerTimeout()
    {
    	var missileSpawnLocation = GetNode<PathFollow2D>("Path2D/PathFollow2D");
    	missileSpawnLocation.Offset = GD.Randi();

    	var enemyMissile = (EnemyMissile)enemyScene.Instance();
    	AddChild(enemyMissile);

    	float direction = missileSpawnLocation.Rotation + Mathf.Deg2Rad(90);

    	enemyMissile.Position = missileSpawnLocation.Position;

    	direction += (float)GD.RandRange(Mathf.Deg2Rad(-45), Mathf.Deg2Rad(45));

    	// avoiding missiles from leaving the screen
    	if(new Vector2(missileSpawnLocation.Position).x < 128 && direction >= 135)
    		direction -= 45;
    	if(new Vector2(missileSpawnLocation.Position).x > 896 && direction <= 45)
    		direction += 45;

    	enemyMissile.Rotation = direction;
    	enemyMissile.GetNode<Sprite>("Sprite").RotationDegrees += 90;

    	var velocity = new Vector2((float)GD.RandRange(enemyMissile.minSpeed, enemyMissile.maxSpeed), 0);
        enemyMissile.LinearVelocity = velocity.Rotated(direction);
    }

    float RandomNumberGen()
    {
    	var randomNumber = new RandomNumberGenerator();
    	randomNumber.Randomize();
    	float delay = randomNumber.RandfRange(0.7f, 2.5f);

    	return delay;
    }

    void GunInstance()
    {
    	for(int i = 1; i < 4; i++)
    	{
    		// instancing guns

    		Gun gun = (Gun)gunScene.Instance();
    		AddChild(gun);

    		var ground = GetNode<Node2D>("Ground");
    		if(i == 1)
    		{
    			gun.Position = ground.GetNode<Position2D>("Gun 1").GlobalPosition;
    			gun.Name = ("Gun 1");
    		}
    		if(i == 2)
    		{
    			gun.Position = ground.GetNode<Position2D>("Gun 2").GlobalPosition;
    			gun.Name = ("Gun 2");
    		}
    		if(i == 3)
    		{
    			gun.Position = ground.GetNode<Position2D>("Gun 3").GlobalPosition;
    			gun.Name = ("Gun 3");
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
	    getInput();
	}

	void getInput()
	{
		if(Input.IsActionJustPressed("mouseLeft"))
		{
			Vector2 mousePos = GetGlobalMousePosition();
			if ( mousePos.x <= 341.3f )
			{
				Gun gun = (Gun)GetNode<Node2D>("Gun 1");
				gun.ShootRocket (GetGlobalMousePosition ());
			}
			else if( mousePos.x > 341.3f && mousePos.x <= 682.6f )
			{
				Gun gun = (Gun)GetNode<Node2D>("Gun 2");
				gun.ShootRocket(GetGlobalMousePosition ());
			}
			else if( mousePos.x > 682.6f && mousePos.x <= 1024 )
			{
				Gun gun = (Gun)GetNode<Node2D>("Gun 3");
				gun.ShootRocket(GetGlobalMousePosition ());
			}
		}
	}
}
