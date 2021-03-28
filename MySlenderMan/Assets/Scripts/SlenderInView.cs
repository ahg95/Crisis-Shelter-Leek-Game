using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlenderInView : MonoBehaviour
{
    private GameObject slender;
    private Kino.AnalogGlitch glitch;
    [SerializeField] private float viewOffset = 1f;
    [SerializeField] private float distortionRatio = 0f;
    private bool lerping = false;

    void Start()
    {
        slender = GameObject.Find("Slender");
        glitch = GetComponent<Kino.AnalogGlitch>();
    }

    void Update()
    {
        if (IsSlenderInView())
        {
            if (!lerping)
            {
                lerping = true;
                StartCoroutine(Lerp(3f, 0.75f));
            }
            glitch.colorDrift = distortionRatio;
            glitch.horizontalShake = distortionRatio;
            glitch.scanLineJitter = distortionRatio;
        }
        else
        {
            if (!lerping)
            {
                lerping = true;
                StartCoroutine(Lerp(3f, 0f));
            }
            glitch.colorDrift = distortionRatio;
            glitch.horizontalShake = distortionRatio;
            glitch.scanLineJitter = distortionRatio;
        }
    }

    private bool IsSlenderInView()
    {
        float distanceFromSlender = Vector3.Distance(slender.transform.position, Camera.main.gameObject.transform.position);
        float slenderToCameraAngle = Vector3.Angle(Camera.main.gameObject.transform.forward, slender.transform.position - Camera.main.transform.position);

        if (slenderToCameraAngle < Camera.main.fieldOfView - viewOffset && distanceFromSlender < 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator Lerp(float lerpDuration, float targetValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            distortionRatio = Mathf.Lerp(0, targetValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        distortionRatio = targetValue;
    }
}
