using UnityEditor;

public class MaximizeScreen : EditorWindow
{
    [MenuItem("Tools/Maximize/Maximize Screen _escape")]
    private static void MaxScreen()
    {
        EditorWindow.focusedWindow.maximized = !EditorWindow.focusedWindow.maximized;
    }
}
