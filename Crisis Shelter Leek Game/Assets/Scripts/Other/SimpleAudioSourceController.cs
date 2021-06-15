using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleAudioSourceController : MonoBehaviour
{

    AudioSource audioSource;

    bool volumeIsFadingOut = false;

    private AudioSource GetAudioSource()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        return audioSource;
    }

    public void FadeOutVolumeOverTime(float time)
    {
        if (!volumeIsFadingOut)
            StartCoroutine(SilenceVolumeInTime(time));
    }

    private IEnumerator SilenceVolumeInTime(float time)
    {
        const float updateIntervalTime = 0.1f;
        float initialVolume = GetAudioSource().volume;

        volumeIsFadingOut = true;

        while (0 <= GetAudioSource().volume)
        {
            GetAudioSource().volume -= initialVolume * updateIntervalTime / time;

            yield return new WaitForSeconds(updateIntervalTime);
        }

        volumeIsFadingOut = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
