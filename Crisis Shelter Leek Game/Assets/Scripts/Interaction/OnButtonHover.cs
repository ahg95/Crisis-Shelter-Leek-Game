using UnityEngine;
using UnityEngine.EventSystems;

public class OnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    SmoothRotationFree rotateCamera;
    public float rotateSpeed = 1f;
    private bool startTimeCount = false;
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
            rotateCamera.MoveCamera(rotateSpeed);
        }
    }
}
