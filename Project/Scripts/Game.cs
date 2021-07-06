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

	Gun[] gunCell = new Gun[4];

	int ammoCount = 0;

	bool showed;
	int iterationCount = 0;

	// score
	int currentScore;
	int scoreRecord;
	string scoreFile = "user://score.save";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Label>("startText").Show();
		showed = true;
		LoadRecordScore();
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
		float delay = randomNumber.RandfRange(0.25f, 1.5f);

		return delay;
	}

	void GunInstance()
	{
		for(int i = 1; i < 4; i++)
		{
			// instancing guns

			Gun gun = (Gun)gunScene.Instance();
			AddChild(gun);

			gun.AddToGroup("Gun");


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
			launcher.Name = $"Launcher {i}";
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if(!showed && iterationCount == 0)
		{
			iterationCount += 1;
			GD.Randomize();
			GunInstance();
			gunCell[0] = null;
			for(int i = 1; i < 4; i++)
			{
				gunCell[i] = (Gun)GetNode<Node2D>($"Gun {i}");
			}
			GetNode<Timer>("StartTimer").Start();
		}
		getInput();
	}

	void getInput()
	{
		if(Input.IsActionJustPressed("mouseLeft") && !showed)
		{
			Vector2 mousePos = GetGlobalMousePosition();
			if ( mousePos.x <= 341.3f )
			{
				if(gunCell[1] != null)
					gunCell[1].ShootRocket (GetGlobalMousePosition ());
				else if(gunCell[2] != null)
					gunCell[2].ShootRocket (GetGlobalMousePosition ());
				else if(gunCell[3] != null)
					gunCell[3].ShootRocket (GetGlobalMousePosition());
				else
					GameOver();
			}
			else if( mousePos.x > 341.3f && mousePos.x <= 682.6f )
			{
				if(gunCell[2] != null)
					gunCell[2].ShootRocket (GetGlobalMousePosition ());
				else if(gunCell[3] != null)
					gunCell[3].ShootRocket (GetGlobalMousePosition ());
				else if(gunCell[1] != null)
					gunCell[1].ShootRocket (GetGlobalMousePosition());
				else
					GameOver();
			}
			else if( mousePos.x > 682.6f && mousePos.x <= 1024 )
			{
				if(gunCell[3] != null)
					gunCell[3].ShootRocket (GetGlobalMousePosition ());
				else if(gunCell[2] != null)
					gunCell[2].ShootRocket (GetGlobalMousePosition ());
				else if(gunCell[1] != null)
					gunCell[1].ShootRocket (GetGlobalMousePosition());
				else
					GameOver();
			}
		}
		if(Input.IsActionJustPressed("ui_cancel"))
		{
			Global global = (Global)GetNode("/root/Global");
			if(!global.gamePaused())
				global.PauseGame();
		}
		if(Input.IsActionJustPressed("ui_accept") && showed == true)
		{
			GetNode<Label>("startText").Hide();
			showed = false;
		}
	}

	public void GameOver()
	{
		Global global = (Global)GetNode("/root/Global");
		global.EndGame();
	}

	public void nullGun(string gunName)
	{
		for(int i = 1; i < 4; i++)
		{
			if(gunName == $"Gun {i}")
				gunCell[i] = null;
		}
	}

	public int countGuns()
	{
		int gunCount = 0;
		foreach( Node2D gun in GetTree().GetNodesInGroup("Gun"))
			gunCount += 1;

		gunCount -= 1;

		if(gunCount == 0)
			GameOver();

		return gunCount;
	}

	public int countCities()
	{
		int citieCount = 0;
		foreach( var citie in GetTree().GetNodesInGroup("Citie"))
			citieCount += 1;

		citieCount -= 1;

		if(citieCount == 0)
			GameOver();

		return citieCount;
	}

	public int countAmmo()
	{
		foreach (Gun gun in GetTree().GetNodesInGroup("Gun"))
		{
			ammoCount += gun.getAmmoCount();
		}
		return ammoCount;
	}

	// level system

	public void ChangeLevel()
	{
		// Todo
	}

	// score system

	public int ChangeScore(int score)
	{
		Global global = (Global)GetNode("/root/Global");
		currentScore += score;
		if(LoadRecordScore() > currentScore)
		{
			scoreRecord = currentScore;
			SaveRecordScore();
			global.changeRecordText(scoreRecord);
		}
		global.changeCurrentText(currentScore);
		var ScoreNode = GetNode<Node2D>("CurrentScore");
		ScoreNode.GetNode<Label>("CurrentScore").Text = $"{currentScore}";
		return currentScore;
	}

	public void SaveRecordScore()
	{
		var file = new File();
		file.Open(scoreFile, File.ModeFlags.Write);
		file.Store32((uint)scoreRecord);
		file.Close();
	}

	public int LoadRecordScore()
	{
		var file = new File();
		if(file.FileExists(scoreFile))
		{
			file.Open(scoreFile, File.ModeFlags.Read);
			scoreRecord = (int)file.Get32();
			file.Close();
		}
		else
			scoreRecord = 0;

		return scoreRecord;
	}
}
