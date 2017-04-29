using TMPro;
using UnityEngine;

namespace IO.Localization{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTMPro : LocalizedComponent
    {

        protected override void OnReady()
        {
            base.OnReady();
            TextMeshProUGUI txt = GetComponent<TextMeshProUGUI>();
            txt.text = m_text;
        }
    }
}

