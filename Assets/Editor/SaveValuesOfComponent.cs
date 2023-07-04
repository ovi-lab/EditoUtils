using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
 
namespace ubc.ok.ovilab.EditorUtils
{
    /// <summary>
    /// Adding menu items to save context
    /// </summary>
    public class SaveValuesOfComponent
    {
        private static Dictionary<Transform, (Vector3, Quaternion)> transforms = new Dictionary<Transform, (Vector3, Quaternion)>();

        [MenuItem("CONTEXT/Transform/Save position && rotation")]
        public static void MenuItem(MenuCommand command)
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            if (EditorApplication.isPlaying)
            {
                Transform target = (Transform)command.context;
                (Vector3, Quaternion) values = (target.localPosition, target.localRotation);

                if (transforms.ContainsKey(target))
                {
                    transforms[target] = values;
                }
                else
                {
                    transforms.Add(target, values);
                }
            }
            else
            {
                Debug.LogWarning($"Not in editor playmode");
            }
        }

        public static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                foreach (KeyValuePair<Transform, (Vector3 pos, Quaternion rot)> t in transforms)
                {
                    Debug.Log($"Setting values for Transform {t.Key.name}");
                    Undo.RecordObject(t.Key, "Saved value from play mode");
                    t.Key.localPosition = t.Value.pos;
                    t.Key.localRotation = t.Value.rot;
                    EditorUtility.SetDirty(t.Key);
                }

                transforms.Clear();
            }
        }
    }
}
