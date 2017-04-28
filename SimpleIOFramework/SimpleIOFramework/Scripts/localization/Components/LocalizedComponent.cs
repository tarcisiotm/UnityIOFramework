using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IO;

namespace IO.Localization{
    public abstract class LocalizedComponent : CSVFile {

        [SerializeField]
        protected LocalizedFileHelper m_localizationHelper;

        [SerializeField]
        protected string m_key;

        protected string m_text;

        bool m_foundKey = false;
       
        protected override void Start () {
            if (m_localizationHelper != null)
            {
                StartCoroutine(WaitForLocalizationHelper());
            }
            else
            {
                base.Start();
            }
        }

        IEnumerator WaitForLocalizationHelper(){
            yield return null;
            //yield return new WaitUntil(m_localizationHelper.IsReady);
            m_text = m_localizationHelper.GetLocalizedString(m_key);
            m_foundKey = true;
            //Debug.Log("Key: " + m_text);
            OnReady();
        }

        protected override void ParseLine (string p_line)
        {
            if (m_foundKey)
            {
                return;
            }
            if (p_line != "") {
                KeyValuePair<string, string> pair = IOUtils.ParseLine(p_line, m_delimiterChar);
                if (pair.Key == m_key)
                {
                    m_text = pair.Value;
                    m_foundKey = true;
                }
            }
        }

    

    }
}
