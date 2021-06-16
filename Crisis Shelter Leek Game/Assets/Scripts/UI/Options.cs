using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteAudio;
    [SerializeField] private Slider sensitivitySlider;

    private RotateCamera cameraRotationScript;

    private void Start()
    {
        if (GameObject.FindWithTag("Player") != null)
        cameraRotationScript = GameObject.FindWithTag("Player").GetComponent<RotateCamera>();
    }
    public void MuteAudio()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    public void ChangeVol(Slider slider)
    {
        AudioListener.volume = slider.value;
    }

    public void ChangeSensitivity(Slider slider)
    {
        cameraRotationScript.rotationSpeedMultiplier = slider.value;
    }
    public void setSensitivity(string sensitivity)
    {
        if (sensitivity == "low")
        {
            cameraRotationScript.rotationSpeedMultiplier = sensitivitySlider.minValue;
            sensitivitySlider.value = sensitivitySlider.minValue;
        }

        if (sensitivity == "medium")
        {
            cameraRotationScript.rotationSpeedMultiplier = (sensitivitySlider.maxValue + sensitivitySlider.minValue) / 2;
            sensitivitySlider.value = (sensitivitySlider.maxValue + sensitivitySlider.minValue) / 2;
        }

        if (sensitivity == "high")
        {
            cameraRotationScript.rotationSpeedMultiplier = sensitivitySlider.maxValue;
            sensitivitySlider.value = sensitivitySlider.maxValue;
        }

        if (sensitivity == "default")
        {
            cameraRotationScript.rotationSpeedMultiplier = 2.25f;
            sensitivitySlider.value = 2.25f;
        }

    }

    private void OnEnable()
    {
        volumeSlider.value = AudioListener.volume;
        muteAudio.isOn = !AudioListener.pause;
    }
}
