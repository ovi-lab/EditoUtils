using UnityEngine;
using UnityEditor;
 
namespace ubc.ok.ovilab.EditorUtils
{
    /// <summary>
    /// Rename the children of the selected components to {childrenPrefix}{index}, index starting from start index.
    /// </summary>
    public class RenameChildren : EditorWindow {
        private static readonly Vector2Int size = new Vector2Int(250, 100);
        private string childrenPrefix;
        private int startIndex;
        [MenuItem("GameObject/Rename children")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<RenameChildren>();
            window.minSize = size;
            window.maxSize = size;
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("Rename the children of the selected components to {childrenPrefix}{index}, index starting from start index.", MessageType.Info);
            childrenPrefix = EditorGUILayout.TextField("Children prefix", childrenPrefix);
            startIndex = EditorGUILayout.IntField("Start index", startIndex);
            if (GUILayout.Button("Rename children")) {
                GameObject[] selectedObjects = Selection.gameObjects;
                for (int objectI = 0; objectI < selectedObjects.Length; objectI++) {
                    Transform selectedObjectT = selectedObjects[objectI].transform;
                    for (int childI = 0, i = startIndex; childI < selectedObjectT.childCount; childI++)
                    {
                        selectedObjectT.GetChild(childI).name = $"{childrenPrefix}{i++}";
                    }
                }
            }
        }
    }
}
