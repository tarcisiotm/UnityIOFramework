using UnityEngine;
using UnityEngine.UI;

namespace IO.Localization{
    
    public class LocalizedText : LocalizedComponent {
        
        protected override void OnReady()
        {
            base.OnReady();
            Text txt = GetComponent<Text>();
            Debug.Log("On Ready");
            if (txt != null)
            {
                GetComponent<Text>().text = m_text;
            }
        }

    }
}
