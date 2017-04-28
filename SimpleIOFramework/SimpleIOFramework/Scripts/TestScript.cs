using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IO.Extensions;

namespace IO{
	[RequireComponent(typeof(IOHandler))]
	public class TestScript : MonoBehaviour {

		IOHandler m_IOHandler;

		void Start()
		{
			m_IOHandler = GetComponent<IOHandler> ();
			string test1= "Memento Mori";

			m_IOHandler.SaveFile ("test1", test1, "", true, false);
			m_IOHandler.SaveBytesToFile ("test1encrypted", test1);

			string output;
			m_IOHandler.LoadFile ("test1", out output, false);
			print ("Output: " + output);
			m_IOHandler.LoadFile ("test1encrypted", out output, true);
			print ("Output decrypted: " + output);
            bool asd;
            Debug.Log("Path: "+IOUtils.GetPath("Persistent"));
            Debug.Log("Streaming: "+IOUtils.GetPath("Persistent", IOUtils.FolderType.StreamingAssets));
            Debug.Log("Temp: "+IOUtils.GetPath("Persistent", IOUtils.FolderType.TemporaryCachePath));
            //IOUtils.SavePlayerPrefs("asd", 1);
		}


	}
}
