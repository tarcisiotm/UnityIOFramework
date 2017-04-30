using UnityEngine;

namespace IO{

	public class IOSampleTestScript : MonoBehaviour {
        
		void Start()
		{
			string test1= "Memento Mori";

            IOManager.SaveFile ("test1", test1, "");
            IOManager.SaveBytesToFile ("test1encrypted", test1);

			string output;
            IOManager.LoadFile ("test1", out output, null, false);
			print ("Output: " + output);
            IOManager.LoadFile ("test1encrypted", out output, null, true);
			print ("Output decrypted: " + output);

            Debug.Log("Path: "+IOUtils.GetPath("Persistent"));
            Debug.Log("Streaming: "+IOUtils.GetPath("Persistent", IOUtils.FolderType.StreamingAssets));
            Debug.Log("Temp: "+IOUtils.GetPath("Persistent", IOUtils.FolderType.TemporaryCachePath));
            //IOUtils.SavePlayerPrefs("asd", 1);
		}


	}
}
