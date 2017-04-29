using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IO;
using System.IO;
using IO.Localization;

namespace IO
{
	public abstract class CSVFile : MonoBehaviour
	{
		[SerializeField]
		protected string m_fileName;

        protected char m_delimiterChar {get{return (char)m_delimiter;}}

        [SerializeField]
        protected Delimiter m_delimiter = Delimiter.Tab;

        public enum Delimiter
        {
            Tab = '\t',
            Comma = ',',
            Pipe = '|',
        }

		[SerializeField]
		[Tooltip("Will only try to load a translation if this is set to true.")]
		protected bool m_lookForTranslation = false;

        [SerializeField]
        [Tooltip("Char that separates root file name from translation suffix, ie options-en-us")]
        protected char m_localizationSeparator = '-';

		[Tooltip ("Preffix for surfing inside the Resources/ folder")]
		public string m_path = "CSVs/";

		[SerializeField]
		bool m_OnStart = true;

        [SerializeField]
        bool m_IgnoreFirstLine = true;

		/// <summary>
		/// Comes from the LanguageManager, if using it.
		/// </summary>
		//protected Language m_language;

		bool m_fileLoaded = false;
		public bool FileLoaded { get {return m_fileLoaded; }}

		protected string m_csvFile;

        protected virtual void Start ()
		{
			if (m_OnStart)
			{
				ParseFile ();
			}           
		}

        protected virtual void OnReady(){

        }

		void Init ()
		{
		}

		protected void ParseFile ()
		{
			string fullpath = GetFullPath ();
            //Debug.Log("Fullpath: "+fullpath);

            m_fileLoaded = IOManager.LoadAndParseTextFromResources(fullpath, m_IgnoreFirstLine, ParseLine);

            OnReady();
		}

		protected virtual void ParseLine (string p_line)
		{
		}

		protected virtual string GetFullPath()
		{
			string preffix = m_path;
			//string suffix = "";
			string fullpath = "";
            string filename = m_fileName;

            Language language = null;
            if (m_lookForTranslation)
            {
                language = CheckLanguageManager();
            }

			if (language != null) {
                filename += m_localizationSeparator + language.FolderPath;
			}

            fullpath = preffix + filename;

			return fullpath;
		}

		Language CheckLanguageManager()
		{
            if (LocalizationManager.Instance != null)
			{
				//m_language = LocalizationManager.Instance.LanguageInUse;
				//print ("Language Manager file exists");
                return LocalizationManager.Instance.LanguageInUse;;
			}
			return null;
		}

		void ProcessString (string p_line)
		{
			m_csvFile += p_line + "/n";
		}

		

	}
}

