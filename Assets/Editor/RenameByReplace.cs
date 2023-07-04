using UnityEngine;
using UnityEditor;
 
namespace ubc.ok.ovilab.EditorUtils
{
    /// <summary>
    /// Replace the old string value with a new value for the selected objects.
    /// </summary>
    public class RenameByReplace : EditorWindow {
        private static readonly Vector2Int size = new Vector2Int(250, 100);
        private string oldValue;
        private string newValue;

        [MenuItem("GameObject/Rename by replace")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<RenameByReplace>();
            window.minSize = size;
            window.maxSize = size;
        }

        private void OnGUI() {
            EditorGUILayout.HelpBox("Replace the old string value with a new value for the selected objects.", MessageType.Info);
            oldValue = EditorGUILayout.TextField("Old value", oldValue);
            newValue = EditorGUILayout.TextField("New Value", newValue);
            if (GUILayout.Button("Rename")) {
                GameObject[] selectedObjects = Selection.gameObjects;

                for (int objectI = 0; objectI < selectedObjects.Length; objectI++)
                {
                    selectedObjects[objectI].transform.name = selectedObjects[objectI].transform.name.Replace(oldValue, newValue);
                }
            }
        }
    }
}
