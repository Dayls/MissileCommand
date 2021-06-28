using Godot;
using System;

public class Citie : Node2D
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    void pop()
    {
        // add score system ( TODO )
        GetNode<AnimatedSprite>("AnimatedSprite").Playing = true;
        
    }

    void _on_AnimatedSprite_animation_finished()
    {
        CallDeferred("free");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
