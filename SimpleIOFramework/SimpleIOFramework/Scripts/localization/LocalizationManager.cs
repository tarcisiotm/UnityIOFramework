using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IO;
using System;

namespace IO.Localization
{
    public class LocalizationManager : MonoBehaviour
	{
        [SerializeField]
        bool m_saveToPlayerPrefs = true;
		//TODO check for / on the beginning and end of path strings
        [SerializeField]
        bool m_dontDestroyOnLoad = false;

		[Tooltip ("Root path for languages inside the Resources/ folder")]
		[SerializeField]
		string m_languageSettingPath;

        static LocalizationManager m_instance;
        public static LocalizationManager Instance {get { return m_instance;}}

		[SerializeField]
		string m_saveFileName = "languageSetting";

		public List<Language> m_languages;

        [SerializeField]
		Language m_currentLanguage;

		public Language LanguageInUse { get { return m_currentLanguage; }}

        void Awake(){
            if (m_instance != null && m_instance != this)
            {
                Destroy(gameObject);
            }
            m_instance = this;
            if (m_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            Init();
        }

        protected virtual void Init(){
        }

		public void OnEnable()
		{
			if (m_languages == null || m_languages.Count == 0)
			{
                Destroy(this);
				return;
			}
			InitLanguage ();
		}

		// Use this for initialization
		void Start ()
		{
		}

		Language GetFirstLanguage()
		{
			if (m_languages != null && m_languages.Count > 0)
			{
				return m_languages [0];
			}
			return null;
		}
        #region Load
        void InitLanguage(){
            
            if (m_languages == null || m_languages.Count == 0)
            {
                Debug.LogWarning("There are no languages available!");
                return;
            }

            if (m_saveToPlayerPrefs)
            {
                InitLanguageFromPlayerPrefs();
            }
            else
            {
                InitLanguageFromFile();
            }
        }

        void InitLanguageFromPlayerPrefs(){
            string id = IOPlayerPrefs.LoadString(m_saveFileName, m_languages[0].ID);
            //Debug.Log("Default value: " + id);
            m_currentLanguage =  GetLanguageByID(id);
            if (m_currentLanguage != null)
            {
                SaveLanguage(m_currentLanguage);
            }
        }

		void InitLanguageFromFile()
		{
			string output = string.Empty;
            bool fileExists = IOManager.LoadFile (m_saveFileName, out output, m_languageSettingPath);
			//print ("Output: "+output);

			if (!fileExists)
			{
				m_currentLanguage = GetFirstLanguage ();
				SaveLanguageToFile (m_currentLanguage);
                //print ("Current Language from default value: " + m_currentLanguage.Name);
				return;
			}
	
			try{
				m_currentLanguage = JsonUtility.FromJson<Language> (output);
				//print ("Current language from json: " + m_currentLanguage.FolderPath);
			}
			catch(System.Exception ex)
			{
                Debug.LogError ("Could not fetch JSON: "+ex.StackTrace);
			}
		
		}
        #endregion Load

        #region Save
        bool SaveLanguage(Language p_newLanguage){
          
            if (p_newLanguage == null || m_languages == null || m_languages.Count == 0 || !m_languages.Contains(p_newLanguage))
            {
                Debug.LogError ("Problem getting the first language");
                return false;
            }

            return m_saveToPlayerPrefs ? SaveLanguageToPlayerPrefs(p_newLanguage) : SaveLanguageToFile(p_newLanguage);
        }

        bool SaveLanguageToPlayerPrefs(Language p_newLanguage){
            IOPlayerPrefs.SaveString(m_saveFileName, p_newLanguage.ID);
            return false;
        }

		bool SaveLanguageToFile(Language p_newLanguage)
		{
			bool result = false;

			string output = JsonUtility.ToJson(p_newLanguage);

			//print ("Output as JSON: " + output);

            result = IOManager.SaveFile (m_saveFileName, output, m_languageSettingPath ,true, false);

			if (result) {
				//print ("File saved successfully!: " + output);
			}

			return result;
		}
        #endregion Save

        public void SetLanguage(Language p_newLanguage){
            if(m_languages.Contains(p_newLanguage) && p_newLanguage != LanguageInUse){
                m_currentLanguage = p_newLanguage;
                SaveLanguage(p_newLanguage);
                //Notify components
            }
        }

        #region Util
        public Language GetLanguageByID(string p_id){
            for (int i = 0; i < m_languages.Count; i++)
            {
                //Debug.Log(m_languages[i].ID + "   " + p_id);
                if (m_languages[i].ID == p_id)
                {
                    return m_languages[i];
                }
            }
            return null;
        }
        #endregion Util
	}

    public static class LocalizationManagerExtensions{

        public static int IndexOfLanguage(this List<Language> p_list, Language p_language){
            for (int i = 0; i < p_list.Count; i++)
            {
                if (IsEqualLanguage(p_list[i], p_language))
                {
                    return i;
                }
            }
            return -1;
        }

        public static bool IsEqualLanguage(Language p_firstLanguage, Language p_secondLanguage){
            if (p_firstLanguage.ID == p_secondLanguage.ID &&
               p_firstLanguage.Description == p_secondLanguage.Description)
            {
                return true;
            }
            return false;
        }

    }
}
