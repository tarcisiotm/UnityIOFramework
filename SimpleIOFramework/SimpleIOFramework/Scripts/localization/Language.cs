using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IO.Localization
{
	[System.Serializable]
	public class Language {

		[SerializeField]
        string m_description;
		[SerializeField]
        string m_ID;

		public string Description { get { return m_description;}}
		public string ID { get { return m_ID;}}

	}
}
