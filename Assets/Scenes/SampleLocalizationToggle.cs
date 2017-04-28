using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Localization;

[RequireComponent(typeof(Button))]
public class SampleLocalizationToggle : MonoBehaviour {

    Button m_button;
    LocalizationManager m_localizationManager;
    void Awake(){
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(OnClick);
    }

    void OnClick(){
    }
	// Use this for initialization
	void Start () {
        m_localizationManager = LocalizationManager.Instance;
   

	}

    void NextLanguage(){
        
        if (m_localizationManager.m_languages.Count < 2)
        {
            return;
        }

        bool foundLanguageInUse = false;
        for (int i = 0; i < m_localizationManager.m_languages.Count; i++)
        {
            if (foundLanguageInUse)
            {
               //set language
            }
            if (m_localizationManager.m_languages[i] == m_localizationManager.LanguageInUse)
            {
                foundLanguageInUse = true;
                if(i == m_localizationManager.m_languages.Count -1){
                   //set language
                }
            }
        }       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
