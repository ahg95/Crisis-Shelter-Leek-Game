using UnityEngine;
using UnityEngine.EventSystems;

public class OnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    SmoothRotationFree rotateCamera;
    public float rotateSpeed = 1f;
    private bool startTimeCount = false;
    [SerializeField] bool vertical = false;
    private void Start()
    {
        rotateCamera = Camera.main.GetComponent<SmoothRotationFree>();
    }
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
            if (!vertical)
            {
                rotateCamera.MoveCameraHor(rotateSpeed);
            }
            else
            {
                rotateCamera.MoveCameraVer(rotateSpeed);
            }
        }
    }
}
