using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace IO.Localization{
    public static class LocalizationEditorContext {
        
        #if UNITY_EDITOR
        [MenuItem("GameObject/UI/Localization/LocalizedText")]
        public static GameObject CreateButton(){
            Debug.Log("Create localized");
            GameObject go = new GameObject("LocalizedText");
            go.AddComponent<LocalizedText>();
            go.AddComponent<Text>();
            return go;
        }
        #endif

        #if UNITY_EDITOR
        [MenuItem("GameObject/UI/Localization/LocalizedTextMeshProUGUI")]
        public static GameObject CreateTextMeshButton(){
            Debug.Log("Create localized TM Pro UGUI");     

            var types = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.Name == "TextMeshProUGUI"
                select type);

            if (types == null || types.Count() == 0)
            {
                Debug.Log("Component Not Available!");
                ShowPopup("Error","TextMeshPro not available");
                return null;
            }

            Type t = types.First<Type>();

            if (t != null)
            {        
                GameObject go = new GameObject("LocalizedText");
                go.AddComponent<LocalizedTMPro>();    
                Debug.Log("TextMeshPro is Available!");
                go.AddComponent(t);
                return go;
            }
            else
            {
                Debug.Log("TextMeshPro Not Available!");
            }

            return null;
        }

        static void ShowPopup(string p_title, string p_message, string p_ok = "Ok"){
            EditorUtility.DisplayDialog(p_title, p_message, p_ok);
        }

        [MenuItem("GameObject/UI/Localization/LocalizedTextMeshProUGUI", true)]
        public static bool CheckCreateTextMeshButton(){
            var types = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.Name == "TextMeshProUGUI"
                select type);

            if (types == null || types.Count() == 0)
            {
                //ShowPopup("Error","TextMeshPro not available");
                Debug.Log("TextMeshPro Component Not Available!");
                return false;
            }

            return true;
        }

        #endif

        public static int Count<T>(this IEnumerable<T> p_source){
            ICollection<T> c = p_source as ICollection<T>;
            if (c != null)
                return c.Count;

            int result = 0;
            using (IEnumerator<T> enumerator = p_source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    result++;
            }
            return result;
        }
    }

   
}
