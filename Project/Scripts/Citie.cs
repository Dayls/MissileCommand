using Godot;
using System;

public class Citie : Node2D
{
    Game mainNode;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        mainNode = (Game)GetTree().Root.GetNode<Node2D>("Game");
    }

    void pop()
    {
        // add score system ( TODO )
        GetNode<AnimatedSprite>("AnimatedSprite").Playing = true;
    }

    void _on_AnimatedSprite_animation_finished()
    {
        mainNode.countCities();
        CallDeferred("free");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
