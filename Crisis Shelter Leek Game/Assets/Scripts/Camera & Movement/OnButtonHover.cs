using UnityEngine;
using UnityEngine.EventSystems;

public class OnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RotateCamera playerRotate;
    [SerializeField] private float rotateSpeed = 1f;
    private bool startTimeCount = false;
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
                playerRotate.MoveVerticalLimited(rotateSpeed);
            }
            else
            {
                playerRotate.RotateFree(rotateSpeed);
            }
        }
    }
    private void OnEnable()
    {
        if (playerRotate == null)
        {
            playerRotate = GetComponentInParent<RotateCamera>();
        }
    }
}
