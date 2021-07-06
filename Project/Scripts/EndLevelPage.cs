using Godot;
using System;

public class EndLevelPage : Control
{
    int rocketPoints;
    int citiePoints;

    int perRocketTimer = 0;
    int perCitieTimer = 0;

    int gp = 0;
    int cp = 0;

    float gunPosX = 15;
    float citiePosX = 80;

    TextureRect ctrlWindow;
    Sprite gun;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ctrlWindow = GetNode<TextureRect>("ControlWindow");
        gun = (Sprite)ctrlWindow.GetNode<Sprite>("Gun");
    }

    public void AddPointPerRocket(int rocketCount)
    {
        rocketPoints = rocketCount;
    }

    public void AddPointPerCitie(int citieCount)
    {
        citiePoints = citieCount;
    }

    void _on_pointPerRocket_timeout()
    {
        perRocketTimer += 1;

        if(perRocketTimer < rocketPoints)
        {
            Sprite gunSprite = (Sprite)ctrlWindow.GetNode<Sprite>("Gun").Duplicate();
            ctrlWindow.AddChild(gunSprite);
            gunSprite.Position += new Vector2(gunPosX, 0);
            gunPosX += 15;

            gp += 10;

            ctrlWindow.GetNode<Label>("GunPoint").Text = $"{gp}";
        }
    }

    void _on_pointPerCitie_timeout()
    {
        perCitieTimer += 1;

        if(perCitieTimer < citiePoints)
        {
            Sprite citieSprite = (Sprite)ctrlWindow.GetNode<Sprite>("Citie").Duplicate();
            ctrlWindow.AddChild(citieSprite);
            citieSprite.Position += new Vector2(citiePosX, 0);
            citiePosX += 80;

            cp += 100;

            ctrlWindow.GetNode<Label>("CitiePoint").Text = $"{cp}";
        }
    }

    public void setTotalPoints(int total)
    {
        ctrlWindow.GetNode<Label>("TotatlPointNumber").Text = $"{total}";
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
