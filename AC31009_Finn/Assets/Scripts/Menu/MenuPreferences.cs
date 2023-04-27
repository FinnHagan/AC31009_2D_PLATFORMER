using UnityEngine;

public class MenuPreferences : MonoBehaviour
{
    public bool canUse = false;
    public MainMenu mainMenu;


    private void Awake()
    {
        if(canUse)
        {
            if(PlayerPrefs.HasKey("Volume"))
            {
                float vol = PlayerPrefs.GetFloat("Voulme");
                mainMenu.volumeValue.text = vol.ToString("0.0");
                mainMenu.volumeSlider.value = vol;
                AudioListener.volume = vol;
            }
            else
            {
                mainMenu.DefaultSettings("Default Volume");
            }

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
                    Screen.fullScreen = false;
                    mainMenu.toggleFullScreen.isOn = false;
                }
            }

            if (PlayerPrefs.HasKey("Brightness"))
            {
                float bright = PlayerPrefs.GetFloat("Brightness");

                mainMenu.brightnessValue.text = bright.ToString("0.0");
                mainMenu.brightnessSlider.value = bright;
            }

        }
        
    }
}
