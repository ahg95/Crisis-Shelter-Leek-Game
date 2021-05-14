using UnityEngine;

[ExecuteInEditMode]
public class RotateChildObjects : MonoBehaviour
{
    public enum AxisToRotateAround
    {
        Up,
        Forward,
        Right
    };
    [SerializeField] private int degreesToTurn = 45;
    [SerializeField] private AxisToRotateAround axisToRotateAround;
    [Space(20)]
    [SerializeField] private GameObject[] children;

    /// <summary>
    /// When the script is enabled, all the Transforms of the child-gameObjects will be rotated in the degreesToTurn angle;
    /// </summary>
    private void Awake()
    {
        enabled = false; // Don't yet rotate when you just added the script. (might want to change degreesToTurn first)
    }
    private void OnEnable()
    {
        children = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject thisChild = transform.GetChild(i).gameObject;
            if (thisChild != transform)
            {
                children[i] = thisChild;
            }
        }

        foreach (GameObject child in children)
        {
            child.transform.RotateAround(transform.position, EnumToVector(), degreesToTurn);
        }

        enabled = false;
    }

    Vector3 EnumToVector()
    {
        switch (axisToRotateAround)
        {
            case AxisToRotateAround.Forward:
                return Vector3.forward;
            case AxisToRotateAround.Right:
                return Vector3.right;
            case AxisToRotateAround.Up:
                return Vector3.up;
            default:
                return Vector3.up;
        }
    }
}
