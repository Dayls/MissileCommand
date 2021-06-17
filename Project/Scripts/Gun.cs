using Godot;
using System;

public class Gun : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    double degree;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Timer timer = new Timer();
		AddChild(timer);
		timer.Connect("timeout", this, "OnTimerTimeout");
		timer.OneShot = false;
		timer.WaitTime = 1.5f;
		timer.Start();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var gun = GetNode<Sprite>("Gun");

		/*if(gun.RotationDegrees < -90)
			gun.RotationDegrees = -90;
		else if(gun.RotationDegrees > 90)
			gun.RotationDegrees = 90;
		else if(gun.RotationDegrees >= -90 && gun.RotationDegrees <= 90)
		{
			gun.LookAt(GetGlobalMousePosition());
			gun.RotationDegrees += 90;
		}*/

		gun.LookAt(GetGlobalMousePosition());
		gun.RotationDegrees += 90;
		GD.Print(gun.RotationDegrees);

		/*if( CheckMouse(this.GetNode<Sprite>("Gun"), this.GetNode<Sprite>("Gun").Position ) < -90)
		{
			gun.RotationDegrees = -90;
		}
		else if(CheckMouse(this.GetNode<Sprite>("Gun"), this.GetNode<Sprite>("Gun").Position ) > 90)
		{
			gun.RotationDegrees = 90;
		}
		else //if(gun.RotationDegrees >= -90 && gun.RotationDegrees <= 90)
		{
			gun.LookAt(GetGlobalMousePosition());
			gun.RotationDegrees += 90;
		}*/
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

	void OnTimerTimeout()
	{
		//GD.Print(degree);
	}
}
