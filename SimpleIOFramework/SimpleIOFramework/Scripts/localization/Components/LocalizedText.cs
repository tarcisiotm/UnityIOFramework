using UnityEngine;
using UnityEngine.UI;

namespace IO.Localization{
    
    [RequireComponent(typeof(Text))]
    public class LocalizedText : LocalizedComponent {

        protected override void OnReady()
        {
            base.OnReady();
            Text txt = GetComponent<Text>();
            txt.text = m_text;      
        }

        public override void ResetContent()
        {
            base.ResetContent();
            Start();
        }
       
    }
}
