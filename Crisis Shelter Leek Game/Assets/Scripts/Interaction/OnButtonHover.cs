using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private UnityEvent onHover = new UnityEvent();
    private float timeCount = 0;
    [SerializeField]
    private float executionInterval = 1f;
    private bool startTimeCount = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        startTimeCount = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        startTimeCount = false;
        timeCount = 0;
    }
    private void Update()
    {
        if (startTimeCount)
        {
            timeCount += Time.deltaTime;

            if (timeCount > executionInterval)
            {
                timeCount = 0;
                onHover.Invoke();
            }
        }
    }
}
