using Godot;
using System;

public class Global : Node
{
    ControlPage controlPage;
    Node mainNode;
    Node2D zIndexNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        mainNode = GetTree().Root.GetNode<Node2D>("Game");
        zIndexNode = mainNode.GetNode<Node2D>("ZIndexNode");
        controlPage = (ControlPage)zIndexNode.GetNode<ControlPage>("ControlPage");
    }

    public bool gamePaused()
    {
        if(GetTree().Paused == true)
            return true;
        else
            return false;
    }

    public void EndGame()
    {
        GetTree().Paused = true;
        controlPage.gameOver();
    }

    public void PauseGame()
    {
        GetTree().Paused = true;
        controlPage.Pause();
    }

    public void ResumeGame()
    {
        GetTree().Paused = false;
        controlPage.Resume();
    }

    public void StartGame()
    {
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }

    public void changeRecordText(int record)
    {
        controlPage.changeRecordScoreText(record);
    }

    public void changeCurrentText(int score)
    {
        controlPage.changeCurrentScoreText(score);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
