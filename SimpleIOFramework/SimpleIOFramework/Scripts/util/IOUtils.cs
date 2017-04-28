using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IO{
    public static class IOUtils {

        #region Variables
        public enum FolderType
        {
            PersistentDataPath,
            StreamingAssets,
            TemporaryCachePath
        }

        static Dictionary <FolderType, string> TypeToPathDic = new Dictionary<FolderType, string>() {
            {FolderType.PersistentDataPath, Application.persistentDataPath},  
            {FolderType.StreamingAssets, GetStreamingAssetsPath()},  
            {FolderType.TemporaryCachePath, Application.temporaryCachePath} 
        };
        #endregion Variables

        #region Paths
        public static string GetPath(string p_filename, FolderType p_folderType = FolderType.PersistentDataPath){
            string output = TypeToPathDic[p_folderType] + p_filename;
            return output;
        }
   
        public static string GetStreamingAssetsPath(){
            string path = "";
            #if UNITY_EDITOR || UNITY_IOS
            path = string.Concat("file://", Application.streamingAssetsPath);
            #else
            path = Application.streamingAssetsPath;
            #endif
            return path + "/";
        }

        static string GetPersistentDataPath(){
            return Application.persistentDataPath;
        }

        static string GetTemporaryCachePath(){
            return Application.temporaryCachePath;
        }
        #endregion Paths

        #region Util
        public static KeyValuePair<string, string> ParseLine (string p_line, char p_delimiter)
        {
            if (p_line != "") {
                string[] c = p_line.Split (p_delimiter);

                if (c.Length == 2) {
                    //Debug.Log("Line: "+c[0]+"  "+c[1]);
                    return new KeyValuePair<string, string>(c[0], c[1]);
                }
            }
            return default(KeyValuePair<string,string>);
        }

        public static void Add<T,T>(this Dictionary<T,T> p_dic, KeyValuePair<T,T> p_valuePair){
            p_dic.Add(p_valuePair.Key, p_valuePair.Value);
        }
        #endregion Util

    }
}
