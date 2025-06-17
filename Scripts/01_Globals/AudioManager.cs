using Godot;
using System;

namespace Game.Globals;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }

    private AudioStreamPlayer musicPlayer;
    private const string VolumeKey = "user://volume_setting.save"; // File for saving volume

    private const float MinDb = -30.0f; // Soft limit for volume (mute is -80)
    private const float MaxDb = 0.0f; // Full volume

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

            // Load saved volume setting
            float savedVolume = LoadVolume();
            SetVolume(savedVolume);
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

    public void SetVolume(float linearVolume)
    {
        linearVolume = Mathf.Clamp(linearVolume, 0.0f, 1.0f); // Ensure volume is between 0-1
        float dbVolume = Mathf.Lerp(MinDb, MaxDb, linearVolume); // Convert to dB scale

        musicPlayer.VolumeDb = dbVolume;
        SaveVolume(linearVolume);
    }

    public float GetVolume()
    {
        return Mathf.InverseLerp(MinDb, MaxDb, musicPlayer.VolumeDb);
    }

    private void SaveVolume(float linearVolume)
    {
        var config = new ConfigFile();
        config.SetValue("audio", "volume", linearVolume);
        config.Save(VolumeKey);
    }

    private float LoadVolume()
    {
        var config = new ConfigFile();
        if (config.Load(VolumeKey) == Error.Ok)
        {
            return (float)config.GetValue("audio", "volume", 1.0f); // Default volume is 1.0
        }
        return 1.0f;
    }
}
