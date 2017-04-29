using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IO.Localization
{
	[System.Serializable]
	public class Language {

		[SerializeField]
		string m_language;
		[SerializeField]
		string m_folderPath;

		public string Name { get { return m_language;}}
		public string FolderPath { get { return m_folderPath;}}

	}
}
