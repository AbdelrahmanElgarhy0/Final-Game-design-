using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = AudioManager.instance.musicSource.volume;
    }

    public void OnValueChanged(float value)
    {
        AudioManager.instance.SetVolume(value);
    }
}
