using Godot;
using System;

public class Ammo : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    Label ammoLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
       	ammoLabel = GetNode<Label>("Label");
		ammoLabel.Text = 10.ToString();
    }

    public void changeText(int text)
    {
    	ammoLabel.Text = text.ToString();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
