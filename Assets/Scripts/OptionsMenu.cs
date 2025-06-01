using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;
    public Toggle toggle;

    private void Start()
    {
        float value;
        bool result = audioMixer.GetFloat("masterVolume", out value);
        if (result) slider.value = value;

        bool isFull = Screen.fullScreen;
        toggle.isOn = isFull;
    }

    public void SetVolume(float volume)
    {
        if(volume <= -30f)
        {
            audioMixer.SetFloat("masterVolume", -80f);
        }
        else
        {
            audioMixer.SetFloat("masterVolume", volume);
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Resolution[] resolutions = Screen.resolutions;
        Screen.fullScreen = isFullScreen;
        if (isFullScreen) Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, FullScreenMode.FullScreenWindow);
        else Screen.SetResolution(resolutions[0].width, resolutions[0].height, FullScreenMode.Windowed);
    }
}
