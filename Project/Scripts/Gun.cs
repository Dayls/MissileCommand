using Godot;
using System;

public class Gun : Node2D
{
	bool canFire = false;
	public PackedScene missileScene = (PackedScene)ResourceLoader.Load("res://Scenes/Missile.tscn");

    double degree;
    int rotationMax = 80;

    int ammo = 10;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var gun = GetNode<Sprite>("Gun");

		float checkRotation = CheckMouse(this.GetNode<Sprite>("Gun"), this.GetNode<Sprite>("Gun").Position);

		if( checkRotation > -rotationMax && checkRotation < rotationMax )
		{
			canFire = true;
			gun.LookAt(GetGlobalMousePosition());
			gun.RotationDegrees += 90;
		}
		else if(checkRotation > -rotationMax - 5 && checkRotation < rotationMax + 5)
			canFire = true;
		else
			canFire = false;
	}

	public float CheckMouse( Sprite gunSprite, Vector2 pos )
	{
		gunSprite = new Sprite();
		AddChild(gunSprite);
		gunSprite.Position = pos;
		gunSprite.LookAt(GetGlobalMousePosition());
		gunSprite.RotationDegrees += 90;
		//GD.Print(gunSprite.RotationDegrees);

		return gunSprite.RotationDegrees;
		
	}

	public void ShootRocket( Vector2 mousePos )
	{
		if(canFire && ammo != 0)
		{
			Missile missile = (Missile)missileScene.Instance();
			AddChild(missile);
			missile.moveToTarget(mousePos);

			changeAmmoCount(1);
		}
	}

	void die()
	{
		// destroying launcher sprite
		if(	this.Name== "Gun 1")
			GetParent().GetNode<Sprite>("Launcher 1").QueueFree();
		if(this.Name == "Gun 2")
			GetParent().GetNode<Sprite>("Launcher 2").QueueFree();
		if(this.Name == "Gun 3")
			GetParent().GetNode<Sprite>("Launcher 3").QueueFree();

		// spawning explosion
		var explode = (Explode)GetNode("/root/Explode");
        explode.startExplosion(GlobalPosition, "enemy", (int)GD.RandRange(1, 2));
        CallDeferred("free");

        // counting the amount of guns in the scene tree
        Game mainNode = (Game)GetTree().Root.GetNode<Node2D>("Game");
		mainNode.countGuns();

		// removing ammo
		changeAmmoCount(10);
	}

	public void changeAmmoCount(int number)
	{
		ammo -= number;
		if(ammo < 0)
			ammo = 0;
		switch(Name)
		{
			case "Gun 1":
				Ammo ammo1 = (Ammo)GetParent().GetNode<CanvasLayer>("Ammo");
				ammo1.changeText(ammo);
				break;
			case "Gun 2":
				Ammo ammo2 = (Ammo)GetParent().GetNode<CanvasLayer>("Ammo2");
				ammo2.changeText(ammo);
				break;
			case "Gun 3":
				Ammo ammo3 = (Ammo)GetParent().GetNode<CanvasLayer>("Ammo3");
				ammo3.changeText(ammo);
				break;
			default:
			break;
		}
	}
}
