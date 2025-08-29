using Game.Globals;
using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class SettingsMenu : CanvasLayer
{
    [Node("MarginContainer/PanelContainer/VBoxContainer/MarginContainer/VBoxContainer/VolumeSlider")]
    private HSlider VolumeSlider;

    private bool isUpdatingSlider = false; // Prevents infinite loops

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes();
        }
    }

    public override void _Ready()
    {
        Hide();

        VolumeSlider.MaxValue = 1.0f;
        VolumeSlider.MinValue = 0.0f;

        // Sync slider with actual volume
        if (AudioManager.Instance != null)
        {
            isUpdatingSlider = true;
            VolumeSlider.Value = AudioManager.Instance.GetVolume();
            isUpdatingSlider = false;
        }

        VolumeSlider.ValueChanged += OnVolumeChanged;
    }

    private void OnVolumeChanged(double value)
    {
        if (AudioManager.Instance != null && !isUpdatingSlider)
        {
            AudioManager.Instance.SetVolume((float)value);
        }
    }

    public void UpdateSliderUI()
    {
        if (AudioManager.Instance != null)
        {
            isUpdatingSlider = true;
            VolumeSlider.Value = AudioManager.Instance.GetVolume();
            isUpdatingSlider = false;
        }
    }

    private void _on_resolutions_item_selected(int index)
    {
        switch (index)
        {
            case 0:
                DisplayServer.WindowSetSize(new Vector2I(1920, 1080));
                break;
            case 1:
                DisplayServer.WindowSetSize(new Vector2I(1600, 900));
                break;
            case 2:
                DisplayServer.WindowSetSize(new Vector2I(1280, 720));
                break;
        }
    }

    private void _on_full_screen_toggled(bool button_pressed)
    {
        DisplayServer.WindowSetMode(button_pressed ? DisplayServer.WindowMode.ExclusiveFullscreen : DisplayServer.WindowMode.Windowed);
    }

    private void _on_save_settings_button_pressed()
    {
        Hide();
    }
}
