using Godot;
using System;

public class MoveCloud : Tween
{
    TextureRect ctrlWindow;
    float startPos;
    float endPos;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ctrlWindow = (TextureRect)GetParent().GetNode<TextureRect>("ControlWindow");

        startPos = -ctrlWindow.RectSize.y;
        endPos = startPos - startPos;
    }

    public void animateIn()
    {
        InterpolateProperty( ctrlWindow, "rect_position:y", startPos, endPos, 2.5f, TransitionType.Cubic, EaseType.InOut );
        Start();
    }

    public void animateOut()
    {
        InterpolateProperty( ctrlWindow, "rect_position:y", endPos, startPos, 2.0f, TransitionType.Cubic, EaseType.InOut );
        Start();
    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
