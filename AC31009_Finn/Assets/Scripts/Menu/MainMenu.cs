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
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void VolumeController(float value)
    {
        AudioListener.volume = value;
        volumeValue.text = value.ToString("0.0");
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
        StartCoroutine(ApplyChanges());
    }

    public void BrightnessController(float value)
    {
        brightnessLevel = value;
        brightnessValue.text = value.ToString("0.0");
    }

    public void FullScreenController(bool check)

    {
        isFullScreen = check;
    }

    public void ChangeGraphics()
    {
        PlayerPrefs.SetFloat("Brightness", brightnessLevel);
        PlayerPrefs.SetInt("FullScreen", (isFullScreen ? 1 : 0));
        Screen.fullScreen = isFullScreen;

        StartCoroutine(ApplyChanges());
    }



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

    public IEnumerator ApplyChanges()
    {
        confirmation.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        confirmation.SetActive(false);
    }
}
