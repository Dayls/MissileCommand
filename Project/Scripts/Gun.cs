using Godot;
using System;

public class Gun : Node2D
{
	bool canFire = false;
	public PackedScene missileScene = (PackedScene)ResourceLoader.Load("res://Scenes/Missile.tscn");

    double degree;
    int rotationMax = 80;

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
		if(canFire)
		{
			Missile missile = (Missile)missileScene.Instance();
			AddChild(missile);
			missile.moveToTarget(mousePos);
		}
	}
}
