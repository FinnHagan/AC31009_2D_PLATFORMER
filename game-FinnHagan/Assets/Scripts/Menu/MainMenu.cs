using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public TMP_Text volumeValue = null;
    public Slider volumeSlider = null;
    public float defaultVolume = 0.5f;

    public TMP_Text brightnessValue = null;
    public Slider brightnessSlider = null;
    public float defaultBrightness = 1;
    private float brightnessLevel;

    private bool isFullScreen;
    public Toggle toggleFullScreen;

    public GameObject confirmation = null;

    public void StartGame()
    {
        SceneManager.LoadScene(1); //Loads level 1 (its build index is 1)
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void VolumeController(float value)
    {
        AudioListener.volume = value;
        volumeValue.text = value.ToString("0.0"); // Updates the volume text display
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("Volume", AudioListener.volume); // Saves the volume setting in player preferences
        StartCoroutine(ApplyChanges());
    }

    public void BrightnessController(float value)
    {
        brightnessLevel = value;
        brightnessValue.text = value.ToString("0.0"); //Updates brightness  text display
    }

    public void FullScreenController(bool check)

    {
        isFullScreen = check; // Sets the fullscreen flag based on the toggle
    }

    public void ChangeGraphics()
    {
        PlayerPrefs.SetFloat("Brightness", brightnessLevel);
        PlayerPrefs.SetInt("FullScreen", (isFullScreen ? 1 : 0));
        Screen.fullScreen = isFullScreen;

        StartCoroutine(ApplyChanges()); //Applies the saved changes for the changed settings
    }


    //Predefinted default settings which the player can revert to by pressing reset in the respective window
    public void DefaultSettings(string menu)
    {
        if (menu == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brightnessValue.text = defaultBrightness.ToString("0.0");

            toggleFullScreen.isOn = false;
            Screen.fullScreen = false;

            ChangeGraphics();
        }

        if (menu == "Volume")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeValue.text = defaultVolume.ToString("0.0");
            ChangeVolume();
        }
    }

    //Gives a confirmation message every time a setting is applied
    public IEnumerator ApplyChanges()
    {
        confirmation.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        confirmation.SetActive(false);
    }
}
