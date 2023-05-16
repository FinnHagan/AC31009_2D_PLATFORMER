using UnityEngine;

public class MenuPreferences : MonoBehaviour
{
    public bool canUse = false;
    public MainMenu mainMenu;

    private void Awake()
    {
        if (canUse)
        {
            // Retrieve volume value from player preferences
            if (PlayerPrefs.HasKey("Volume"))
            {
                float vol = PlayerPrefs.GetFloat("Volume");
                mainMenu.volumeValue.text = vol.ToString("0.0");
                mainMenu.volumeSlider.value = vol;
                AudioListener.volume = vol;
            }
            else
            {
                // If the "Volume" key doesn't exist, set default volume settings in the main menu
                mainMenu.DefaultSettings("Default Volume");
            }

            // Retrieve full screen choice from player preferences
            if (PlayerPrefs.HasKey("FullScreen"))
            {
                int fs = PlayerPrefs.GetInt("FullScreen");
                if (fs == 1)
                {
                    Screen.fullScreen = true;
                    mainMenu.toggleFullScreen.isOn = true;
                }
                else
                {
                    // If fullscreen value is false, disable fullscreen and set the toggle state to off in the main menu
                    Screen.fullScreen = false;
                    mainMenu.toggleFullScreen.isOn = false;
                }
            }

            // Retrieve brightness value from player preferences
            if (PlayerPrefs.HasKey("Brightness"))
            {
                float bright = PlayerPrefs.GetFloat("Brightness");
                mainMenu.brightnessValue.text = bright.ToString("0.0");
                mainMenu.brightnessSlider.value = bright;
            }
        }
    }
}
