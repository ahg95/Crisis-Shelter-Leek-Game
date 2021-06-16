using UnityEditor;

public class MaximizeScreen : EditorWindow
{
    [MenuItem("Tools/Maximize/Maximize Screen %q")]
    private static void MaxScreen()
    {
        if (!EditorApplication.isPlaying)
        {
            EditorWindow.focusedWindow.maximized = !EditorWindow.focusedWindow.maximized;
        }
    }
}
