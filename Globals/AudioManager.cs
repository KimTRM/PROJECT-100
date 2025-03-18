using Godot;
using System;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }

    private AudioStreamPlayer musicPlayer;

    public override void _Ready()
    {
        if (Instance == null)
        {
            Instance = this;
            musicPlayer = new AudioStreamPlayer();
            AddChild(musicPlayer);

            musicPlayer.ProcessMode = ProcessModeEnum.Always;
            musicPlayer.Finished += OnMusicFinished; // Ensure looping

            PlayMusic((AudioStream)GD.Load("res://Assets/AudioFiles/Astra__May_2_2024_852_PM.wav"));
        }
        else
        {
            QueueFree(); // Prevent duplicate AudioManagers
        }
    }

    public void PlayMusic(AudioStream music)
    {
        if (musicPlayer == null) return;

        musicPlayer.Stream = music;
        musicPlayer.Play();
    }

    private void OnMusicFinished()
    {
        musicPlayer.Play(); // Manually loop when finished
    }
}
