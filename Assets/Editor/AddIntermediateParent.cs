using UnityEngine;
using UnityEditor;
 
namespace ubc.ok.ovilab.EditorUtils
{
    /// <summary>
    /// Add gameobject between all selected objects and their
    /// children.  i.e., if an object `abc` is selected which has a
    /// child `xyz`, the hierachy will become `abc`>`abc{intermediate
    /// pareptn prefix}`>`xyz`
    /// </summary>
    public class AddIntermediateParent : EditorWindow {
        private static readonly Vector2Int size = new Vector2Int(250, 100);
        private string intermediateParentPrefix;

        [MenuItem("GameObject/Add intermediate parent")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<AddIntermediateParent>();
            window.minSize = size;
            window.maxSize = size;
        }

        private void OnGUI() {
            EditorGUILayout.HelpBox("Add gameobject between all selected objects and their children." +
                                    "i.e., if an object `abc` is selected which has a child `xyz`," +
                                    "the hierachy will become `abc`>`abc{intermediate pareptn prefix}`>`xyz`", MessageType.Info);
            intermediateParentPrefix = EditorGUILayout.TextField("Children prefix", intermediateParentPrefix);
            if (GUILayout.Button("Add intermediate parent")) {
                GameObject[] selectedObjects = Selection.gameObjects;

                for (int objectI = 0; objectI < selectedObjects.Length; objectI++)
                {
                    Transform selectedObjectT = selectedObjects[objectI].transform;
                    GameObject intermediateParent = new GameObject();
                    intermediateParent.gameObject.name = selectedObjectT.transform.name + intermediateParentPrefix;

                    for (int i = 0; i < selectedObjectT.childCount; i++)
                    {
                        selectedObjectT.GetChild(i).parent = intermediateParent.transform;
                    }

                    intermediateParent.transform.parent = selectedObjectT.transform;
                }
            }
        }
    }
}
