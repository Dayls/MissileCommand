using Godot;
using System;

public class ControlPage : Control
{
    MoveCloud tween;
    TextureRect controlWindow;
    Global global;
    Label controlText;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tween = (MoveCloud)GetNode<Tween>("Tween");
        controlWindow = (TextureRect)GetNode<TextureRect>("ControlWindow");
        global = (Global)GetNode("/root/Global");
        controlText = (Label)controlWindow.GetNode<Label>("controlText");
    }

    public void ReloadScene()
    {
        _ExitTree();
        QueueFree();
    }

    public void changeCurrentScoreText(int score)
    {
        controlWindow.GetNode<Label>("CurrentScoreNumber").Text = $"\n\n\n{score}";
    }

    public void changeRecordScoreText(int record)
    {
        controlWindow.GetNode<Label>("RecordScoreNumber").Text = $"\n\n\n{record}";
    }

    public void AnimateIn()
    {
        tween.animateIn();
    }

    public void AnimateOut()
    {
        tween.animateOut();
    }

    public void Pause()
    {
        controlText.Text = "\nGame paused";
        tween.animateIn();
    }

    public void Resume()
    {
        tween.animateOut();
    }

    void _on_Resume_pressed()
    {
        global.ResumeGame();
    }

    public void Start()
    {
        global.StartGame();
    }

    void _on_Restart_pressed()
    {
        global.StartGame();
    }

    public void gameOver()
    {
        controlWindow.GetNode<Label>("controlText").Text = "\nGAME OVER";
        tween.animateIn();
    }
}
