using UnityEditor;
using UnityEngine;

public class GroupObjects : EditorWindow
{
    [MenuItem("Tools/Group Selected/Group (Pivot First selected) %g")]
    public static void GroupSelected()
    {
        if (!Application.isPlaying)
        {
            if (!Selection.activeTransform) return;
            var go = new GameObject(Selection.activeTransform.name + " Group");
            go.transform.position = Selection.activeTransform.position;
            Undo.RegisterCreatedObjectUndo(go, "Group Selected");
            go.transform.SetParent(Selection.activeTransform.parent, false);
            foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
            Selection.activeGameObject = go;
        }
    }

    [MenuItem("Tools/Group Selected/Group (Pivot Zeroed Out) #g")]
    public static void GroupSelectedPivot()
    {
        if (!Application.isPlaying)
        {
            if (!Selection.activeTransform) return;
            var go = new GameObject(Selection.activeTransform.name + " Group");
            Undo.RegisterCreatedObjectUndo(go, "Group Selected");
            go.transform.SetParent(Selection.activeTransform.parent, false);
            foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
            Selection.activeGameObject = go;
        }
    }

    [MenuItem("Tools/Group Selected/Group (Pivot Averaged) #a")]
    private static void GroupSelectAverage()
    {
        if (!Application.isPlaying)
        {
            GameObject go = GetAveragePositionObject(Selection.gameObjects);

            Undo.RegisterCreatedObjectUndo(go, "Group Selected");
            go.transform.SetParent(Selection.activeTransform.parent, false);
            foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
            Selection.activeGameObject = go;
        }
    }

    public static GameObject GetAveragePositionObject(GameObject[] objectsToGroup)
    {
        if (!Application.isPlaying)
        {
            if (objectsToGroup.Length <= 0) return null;
            GameObject go = new GameObject();

            Vector3 average = Vector3.zero;
            foreach (GameObject selectedObject in objectsToGroup)
            {
                average += selectedObject.transform.position;
            }
            average /= objectsToGroup.Length;

            go.transform.position = average;

            return go;
        }
        else
        {
            return null;
        }
    }
}
