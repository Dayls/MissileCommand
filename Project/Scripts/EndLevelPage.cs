using Godot;
using System;

public class EndLevelPage : Control
{
    int rocketPoints = 0;
    int citiePoints = 0;

    int perRocketTimer = 0;
    int perCitieTimer = 0;

    int rp = 0;
    int cp = 0;

    float gunPosX = 15;
    float citiePosX = 80;

    TextureRect ctrlWindow;
    Sprite gun;
    MoveCloud tween;
    Game mainNode;

    PackedScene beep = (PackedScene)ResourceLoader.Load("res://Scenes/BeepSound.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ctrlWindow = GetNode<TextureRect>("ControlWindow");
        tween = (MoveCloud)GetNode<Tween>("Tween");
        gun = (Sprite)ctrlWindow.GetNode<Sprite>("Gun");
        mainNode = (Game)GetTree().Root.GetNode<Node2D>("Game");
    }

    public void AnimateIn(int rocketCount, int citieCount)
    {
        GetTree().Paused = true;

        tween.animateIn();

        AddPointPerRocket(rocketCount);
        AddPointPerCitie(citieCount);

        Timer timer = new Timer();
        AddChild(timer);
        timer.Connect("timeout", this, "AnimateOutTimer");
        timer.WaitTime = 5.5f;
        timer.OneShot = true;
        timer.Start();
    }

    public void AnimateOut()
    {
        GetTree().Paused = false;

        tween.animateOut();

        Timer timer = new Timer();
        AddChild(timer);
        timer.Connect("timeout", this, "AnimateOutQueueFree");
        timer.WaitTime = 5.5f;
        timer.OneShot = true;
        timer.Start();
    }

    void AnimateOutTimer()
    {
        AnimateOut();
        mainNode.FreeRockets();
        mainNode.ResetAmmo();
    }

    void AnimateOutQueueFree()
    {
        foreach(Node addedNode in GetTree().GetNodesInGroup("Added"))
        {
            addedNode.QueueFree();
        }

        // nulling variables
        rocketPoints = 0;
        citiePoints = 0;

        perRocketTimer = 0;
        perCitieTimer = 0;

        rp = 0;
        cp = 0;

        gunPosX = 15;
        citiePosX = 80;
    }

    

    void AddPointPerRocket(int rocketCount)
    {
        rocketPoints = rocketCount;
        if(rocketCount != 0)
            GetNode<Timer>("pointPerRocket").Start();
    }

    void AddPointPerCitie(int citieCount)
    {
        citiePoints = citieCount;
        if(citieCount != 0)
            GetNode<Timer>("pointPerCitie").Start();

        setTotalPoints();
    }

    void _on_pointPerRocket_timeout()
    {
        // beeping
        AudioStreamPlayer beepSound = (AudioStreamPlayer)beep.Instance();
        AddChild(beepSound);
        beepSound.Play();

        // instancing rocket sprites
        perRocketTimer += 1;

        if(perRocketTimer <= rocketPoints)
        {
            Sprite gunSprite = (Sprite)ctrlWindow.GetNode<Sprite>("Gun").Duplicate();
            ctrlWindow.AddChild(gunSprite);
            gunSprite.AddToGroup("Added");
            gunSprite.Position += new Vector2(gunPosX, 0);
            gunPosX += 15;

            rp += 5;

            ctrlWindow.GetNode<Label>("GunPoint").Text = $"{rp}";
        }
        else
            GetNode<Timer>("pointPerRocket").Stop();
    }

    void _on_pointPerCitie_timeout()
    {
        // beeping
        AudioStreamPlayer beepSound = (AudioStreamPlayer)beep.Instance();
        AddChild(beepSound);
        beepSound.Play();

        // instancing citie sprites
        perCitieTimer += 1;

        if(perCitieTimer <= citiePoints)
        {
            Sprite citieSprite = (Sprite)ctrlWindow.GetNode<Sprite>("Citie").Duplicate();
            ctrlWindow.AddChild(citieSprite);
            citieSprite.AddToGroup("Added");
            citieSprite.Position += new Vector2(citiePosX, 0);
            citiePosX += 80;

            cp += 100;

            ctrlWindow.GetNode<Label>("CitiePoint").Text = $"{cp}";
        }
        else
            GetNode<Timer>("pointPerCitie").Stop();
    }

    public void setTotalPoints()
    {
        ctrlWindow.GetNode<Label>("TotalPointNumber").Text = $"{rocketPoints * 5 + citiePoints * 100}";
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
