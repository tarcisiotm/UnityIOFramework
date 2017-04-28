using System.Collections;
using System.Collections.Generic;
using IO;

namespace IO.Localization{

    /// <summary>
    /// Gameobject that will cache text so file reading will only happen once for this given file
    /// regardless of how many objects are reading from it.
    /// </summary>
    public class LocalizedFileHelper : CSVFile
    {
        Dictionary<string, string> m_fileDic = new Dictionary<string, string>();
        bool m_ready = false;
        public bool Ready {get { return m_ready;} }

        protected override void Start()
        {
            base.Start();
            m_ready = true;
        }

        public bool IsReady(){
            return m_ready;
        }

        protected override void ParseLine (string p_line)
        {
            if (p_line != "") {
                m_fileDic.Add<string, string>(IOUtils.ParseLine(p_line, m_delimiterChar));
            }
        }

        /// <summary>
        /// Given a key, will return the corresponding localized string.
        /// </summary>
        /// <returns>The localized string.</returns>
        /// <param name="p_key">The key.</param>
        public string GetLocalizedString(string p_key){
            string output;
            m_fileDic.TryGetValue(p_key, out output);
            return output;
        }
    }
}
