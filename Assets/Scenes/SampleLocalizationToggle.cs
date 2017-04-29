using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IO.Localization;

[RequireComponent(typeof(Button))]
public class SampleLocalizationToggle : MonoBehaviour {

    Button m_button;
    LocalizationManager m_localizationManager;
    void Awake(){
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(OnClick);
    }

	// Use this for initialization
	void Start () {
        m_localizationManager = LocalizationManager.Instance;
	}

    void OnClick(){
        
        if (m_localizationManager.m_languages.Count < 2)
        {
            return;
        }
      
        int index = m_localizationManager.m_languages.IndexOfLanguage(m_localizationManager.LanguageInUse);

        if (index < 0)
        {
            Debug.Log("Index: " + index + "   " + m_localizationManager.m_languages.Count + "   " + m_localizationManager.LanguageInUse.Name);
            return;
        }

        index = index + 1 == m_localizationManager.m_languages.Count ? 0 : index + 1;

        m_localizationManager.SetLanguage(m_localizationManager.m_languages[index]);
        GetComponentInChildren<LocalizedText>().ResetContent();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
