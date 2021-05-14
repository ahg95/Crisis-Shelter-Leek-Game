using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteAudio;
    public void MuteAudio()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void ChangeVol(Slider slider)
    {
        AudioListener.volume = slider.value;
    }

    private void OnEnable()
    {
        volumeSlider.value = AudioListener.volume;
        muteAudio.isOn = !AudioListener.pause;
    }
}
