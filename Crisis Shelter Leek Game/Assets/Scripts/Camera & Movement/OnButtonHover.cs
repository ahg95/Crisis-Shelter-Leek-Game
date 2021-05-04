using UnityEngine;
using UnityEngine.EventSystems;

public class OnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] SmoothRotationFree rotateCameraFree;
    [SerializeField] SmoothRotationWithLimits rotateCameraLimits;
    public float rotateSpeed = 1f;
    public bool startTimeCount = false;
    [SerializeField] bool vertical = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        startTimeCount = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        startTimeCount = false;
    }
    private void Update()
    {
        if (startTimeCount)
        {
            if (vertical)
            {
                rotateCameraLimits.MoveCameraVertical(rotateSpeed);
            }
            else
            {
                rotateCameraFree.MoveCameraHor(rotateSpeed);
            }
        }
    }
}
