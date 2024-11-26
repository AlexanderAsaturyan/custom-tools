using UnityEditor;

public class SimpleTool 
{
    [MenuItem("Tools/My Custom Tool/Show Message")]
    private static void ShowMessage()
    {
        EditorUtility.DisplayDialog("Hello", "This is a custom tool! UPGRADED!", "OK");
    }
}
