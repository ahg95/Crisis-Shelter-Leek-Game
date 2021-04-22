using UnityEngine;

[ExecuteInEditMode]
public class DeleteLater : MonoBehaviour
{
    void Awake()
    {
        GetComponent<OnButtonHover>().rotateSpeed = GetComponent<OnButtonHover>().rotateSpeed * 1.25f;
        DestroyImmediate(this, false);
    }
}
