using UnityEditor;
using UnityEngine;

public class GroupObjects : EditorWindow
{
    [MenuItem("Tools/Group Selected/Pivot from first selected %g")]
    private static void GroupSelected()
    {
        if (!Selection.activeTransform) return;
        var go = new GameObject(Selection.activeTransform.name + " Group");
        go.transform.position = Selection.activeTransform.position;
        Undo.RegisterCreatedObjectUndo(go, "Group Selected");
        go.transform.SetParent(Selection.activeTransform.parent, false);
        foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
        Selection.activeGameObject = go;
    }

    [MenuItem("Tools/Group Selected/Pivot zeroed out #g")]
    private static void GroupSelectedPivot()
    {
        if (!Selection.activeTransform) return;
        var go = new GameObject(Selection.activeTransform.name + " Group");
        Undo.RegisterCreatedObjectUndo(go, "Group Selected");
        go.transform.SetParent(Selection.activeTransform.parent, false);
        foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
        Selection.activeGameObject = go;
    }
}
