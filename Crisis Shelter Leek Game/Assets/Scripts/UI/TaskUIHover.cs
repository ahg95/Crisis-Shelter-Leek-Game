using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TaskUIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator taskUIAnim;

    private void Start()
    {
        taskUIAnim = gameObject.GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        taskUIAnim.SetBool("Hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        taskUIAnim.SetBool("Hover", false);

    }
}
