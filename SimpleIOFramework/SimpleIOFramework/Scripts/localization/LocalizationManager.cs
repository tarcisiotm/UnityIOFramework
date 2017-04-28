using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IO;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
	{
		//TODO check for / on the beginning and end of path strings
        [SerializeField]
        bool m_dontDestroyOnLoad = false;

		[Tooltip ("Root path for languages inside the Resources/ folder")]
		[SerializeField]
		string m_languageSettingPath;

        LocalizationManager m_instance;
        LocalizationManager Instance {get { return m_instance;}}

		[SerializeField]
		string m_saveFileName = "languageSetting";

		public List<Language> m_languages;

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

		void InitLanguage()
		{
			string output = string.Empty;
			bool fileExists = IOHandler.Instance.LoadFile (m_saveFileName, out output, m_languageSettingPath, false);
			print ("Output: "+output);

			if (!fileExists)
			{
				m_currentLanguage = GetFirstLanguage ();
				SaveLanguageFile (m_currentLanguage);
                print ("Current Language from default value: " + m_currentLanguage.Name);
				return;
			}
	
			try{
				m_currentLanguage = JsonUtility.FromJson<Language> (output);
				print ("Current language from json: " + m_currentLanguage.FolderPath);
			}
			catch(System.Exception ex)
			{
				print ("Could not fetch JSON: "+ex.StackTrace);
			}
		
		}

		bool SaveLanguageFile(Language p_newLanguage)
		{
			bool result = false;

			if (p_newLanguage == null || m_languages == null || m_languages.Count == 0 || !m_languages.Contains(p_newLanguage))
			{
				print ("Problem getting the first language");
				return false;
			}

			string output = JsonUtility.ToJson(p_newLanguage);

			print ("Output as JSON: " + output);

			result = IOHandler.Instance.SaveFile (m_saveFileName, output, m_languageSettingPath ,true, false);

			if (result) {
				print ("File saved successfully!: " + output);
			}

			return result;
		}

	}
}
