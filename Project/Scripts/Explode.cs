using Godot;
using System;

public class Explode : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void startExplosion(Vector2 globPos, string source, int scale)
    {
        PackedScene ExplosionScene = (PackedScene)ResourceLoader.Load("res://Scenes/MissileExplosion.tscn");
        MissileExplosion explosion = (MissileExplosion)ExplosionScene.Instance();
        GetTree().Root.CallDeferred("add_child", explosion);
        explosion.GlobalPosition = globPos;
        explosion.setSource(source);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
