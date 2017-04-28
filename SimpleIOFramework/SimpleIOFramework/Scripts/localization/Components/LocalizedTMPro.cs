using TMPro;
using UnityEngine;

namespace IO.Localization{

    public class LocalizedTMPro : LocalizedComponent
    {
        protected override void OnReady()
        {
            base.OnReady();
            TextMeshProUGUI txt = GetComponent<TextMeshProUGUI>();
            Debug.Log("On Ready");
            if (txt != null)
            {
                GetComponent<TextMeshProUGUI>().text = m_text;
            }
        }
    }
}

