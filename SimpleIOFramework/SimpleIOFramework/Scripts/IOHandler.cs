using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

namespace IO
{
    public class IOHandler : MonoBehaviour
	{
        public static IOHandler m_instance;
        public static IOHandler Instance { get{ return m_instance;}}

        [SerializeField]
        bool m_DontDestroyOnLoad;

        [SerializeField]
        bool m_debug;

        #region Unity's Callbacks
        void Awake(){
            if (m_instance != null && m_instance != this)
            {
                Destroy(this);
            }
            if (m_DontDestroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }
            m_instance = this;
        }
        #endregion Unity's Callbacks

		//TODO option to save to the cache/temp directory

		/// <summary>
		/// Saves the file to the platform's default persistent path.
		/// </summary>
		public bool SaveFile (string fileName, string fileToSave, bool overwrite = true, bool encrypt = true)
		{
			return SaveFile (fileName, fileToSave, "", overwrite, encrypt);
		}

		/// <summary>
		/// Saves the file to the platform's default persistent path.
		/// </summary>
		/// <returns><c>true</c>, If the file was saved siccessfully <c>false</c> otherwise.</returns>
		/// <param name="fileName">The name to save the file.</param>
		/// <param name="fileToSave">Text to save.</param>
		/// <param name="directory">Sub-directory to save.</param>
		/// <param name="overwrite">If set to <c>true</c> overwrites the file.</param>
		/// <param name="encrypt">If set to <c>true</c> encrypt.</param>
		public bool SaveFile (string fileName, string fileToSave, string directory, bool overwrite = true, bool encrypt = true)
		{ 
			CheckCreateDirectory (directory);

			if (encrypt) {
				fileToSave = Encoding.UTF8.GetString(EncryptionHandler.EncryptStringToBytes (fileToSave));
			}

			string path = GetFullPersistentPath(fileName, directory);

			try { //TODO write it in chunks
				//TODO SCRAMBLE key with unique id from device

				using (StreamWriter fileWriter = overwrite ? File.CreateText (path) : File.AppendText (path)) {
					fileWriter.Write (fileToSave); //Enconding.UTF8
					return true;
				}
			} catch (System.Exception ex) {
				fileToSave = "Exceção de IO na escrita: " + ex.Message;
				print (fileToSave);
				return false;
			}
		}

		/// <summary>
		/// Loads the specified file from the default directory.
		/// </summary>
		/// <returns><c>true</c>, if file was successfully loaded, <c>false</c> otherwise.</returns>
		/// <param name="fileName">File name.</param>
		/// <param name="output">Output string.</param>
		/// <param name="decrypt">If set to <c>true</c> decrypts the file.</param>
		public bool LoadFile (string fileName, out string output, bool decrypt = true)
		{
			return LoadFile (fileName, out output, "", decrypt);
		}

		/// <summary>
		/// Loads the file from a specified directory.
		/// </summary>
		/// <returns><c>true</c>, if file was loaded successfully, <c>false</c> otherwise.</returns>
		/// <param name="fileName">File name.</param>
		/// <param name="output">Output string.</param>
		/// <param name="directory">Directory.</param>
		/// <param name="decrypt">If set to <c>true</c> decrypts.</param>
		public bool LoadFile (string fileName, out string output, string directory, bool decrypt = true)
		{
			if (!DirectoryExists (directory)) {
				output = String.Format(Constants.DIRECTORY_NOT_FOUND, directory);
				return false;
			}

			output = String.Empty;

			if (decrypt) {
				return LoadBinaryFile (fileName, out output, directory, true);
			}

			string filePath = GetFullPersistentPath(fileName, directory);
			try {
				using (StreamReader reader = new StreamReader (filePath)){
					while (true) {
						string line = reader.ReadLine ();
						if (line == null) {
							break;
						}
						output += line;
					}
					return true;
				}
			} catch (System.Exception ex) {
				output = "Exceção de IO na leitura: " + ex.Message;
				return false;
			}
		}


		#region Binary

		/// <summary>
		/// Saves the bytes as binary to file to the default directory.
		/// </summary>
		/// <returns><c>true</c>, if bytes to file were saved successfully, <c>false</c> otherwise.</returns>
		/// <param name="fileName">File name.</param>
		/// <param name="fileToSave">String to save.</param>
		/// <param name="overwrite">If set to <c>true</c> overwrites existing files.</param>
		/// <param name="encrypt">If set to <c>true</c> encrypts.</param>
		public bool SaveBytesToFile (string fileName, string fileToSave, bool overwrite = true, bool encrypt = true)
		{
			return SaveBytesToFile (fileName, fileToSave, "", overwrite, encrypt);
		}

		/// <summary>
		/// Saves the bytes as binary to file to the default directory.
		/// </summary>
		/// <returns><c>true</c>, if bytes to file were saved successfully, <c>false</c> otherwise.</returns>
		/// <param name="fileName">File name.</param>
		/// <param name="fileToSave">String to save.</param>
		/// <param name="directory">Directory to save the file.</param>
		/// <param name="overwrite">If set to <c>true</c> overwrites existing files.</param>
		/// <param name="encrypt">If set to <c>true</c> encrypts.</param>
		public bool SaveBytesToFile (string fileName, string fileToSave, string directory, bool overwrite = true, bool encrypt = true)
		{ 
			CheckCreateDirectory (directory);

			string path = GetFullPersistentPath (fileName, directory);

			try {
				byte[] binaryToSave;

				if(encrypt)
				{
					binaryToSave = EncryptionHandler.EncryptStringToBytes (fileToSave);
				}
				else
				{
					binaryToSave = Encoding.UTF8.GetBytes(fileToSave);
				}

				FileMode fileMode = overwrite ? FileMode.Create : FileMode.Append;

				using (BinaryWriter binaryWriter = new BinaryWriter (File.Open (path,  fileMode))) {
					binaryWriter.Write (binaryToSave);
				}
				return true;
			} catch (System.Exception ex) {
				fileToSave = "Exceção de IO na escrita: " + ex.Message;
				return false;
			}
		}

		/// <summary>
		/// Loads the binary file.
		/// </summary>
		/// <returns><c>true</c>, if binary file was loaded successfully, <c>false</c> otherwise.</returns>
		/// <param name="fileName">File name.</param>
		/// <param name="output">Output string.</param>
		/// <param name="decrypt">If set to <c>true</c> decrypts the file.</param>
		public bool LoadBinaryFile (string fileName, out string output, bool decrypt = true)
		{
			return LoadBinaryFile (fileName, out output, "", decrypt);
		}

		/// <summary>
		/// Loads the binary file on the specified directory.
		/// </summary>
		/// <returns><c>true</c>, if binary file was loaded successfully, <c>false</c> otherwise.</returns>
		/// <param name="nameOfFile">Name of file.</param>
		/// <param name="output">Output string.</param>
		/// <param name="directory">Directory to load the file from.</param>
		/// <param name="decrypt">If set to <c>true</c> decrypts the file.</param>
		public bool LoadBinaryFile (string nameOfFile, out string output, string directory, bool decrypt = true)
		{
			if (!DirectoryExists (directory)) {
				output = Constants.DIRECTORY_NOT_FOUND;
				return false;
			}

			output = String.Empty;

			string fileName = GetFullPersistentPath (nameOfFile, directory);

			if (!FileExists (fileName)) {
				output = Constants.FILE_NOT_FOUND;
				return false;
			}

			try {

				byte[] bytesToRead = File.ReadAllBytes (fileName); //TODO Read in chunks
				if(decrypt)
				{
					output = EncryptionHandler.DecryptStringFromBytes(bytesToRead);
				}
				else
				{
					output = Encoding.UTF8.GetString(bytesToRead);
				}

				return true;

			} catch (System.Exception ex) {
				output = "Exceção de IO na leitura: " + ex.Message;
				return false;
			}
		}
		#endregion Binary


		#region util

		/// <summary>
		/// Checks to see if the sub-directory exists, creates one otherwise.
		/// </summary>
		/// <param name="directory">Directory.</param>
		void CheckCreateDirectory(string directory)
		{
			if (directory != null) {

				print ("Creating directory: " + Application.persistentDataPath +"/"+ directory);
				System.IO.Directory.CreateDirectory (Application.persistentDataPath +"/"+ directory);
			}
		}

		/// <summary>
		/// Checks if the given file exists inside the provided subfolder
		/// </summary>
		/// <returns><c>true</c>, if exists was filed, <c>false</c> otherwise.</returns>
		/// <param name="nameOfFile">Name of file.</param>
		/// <param name="directory">Sub Directory to search.</param>
		/// <param name="extension">File extension.</param>
		public bool FileExists (string fileName, string directory = "", string extension = "")
		{
			//TODO check what happens if directory does not exist
			string path = Application.persistentDataPath + directory;
			string[] files = extension == "" ? Directory.GetFiles (path) : Directory.GetFiles (path, extension);

			for (int i = 0; i < files.Length; ++i) {
				print (files [i] + "   " + fileName);
				if (files [i].Equals(fileName)) { //Path.GetFileName (
					print ("File was found!");
					return true;
				}
			}
			return false;
		}

		public bool DirectoryExists(string directory)
		{
			return Directory.Exists (Application.persistentDataPath +"/"+ directory);
		}

		/// <summary>
		/// Returns the full path for a given file and subdirectory.
		/// </summary>
		/// <returns>The full persistent path.</returns>
		/// <param name="fileName">File name.</param>
		/// <param name="directory">Directory.</param>
		public string GetFullPersistentPath(string fileName, string directory)
		{
            //TODO Safety checks on variables
			return Application.persistentDataPath +"/"+ directory + "/" + fileName;
		}

		#endregion util


		#region Delete
		public bool DeleteFile (string fileName, string directory, out string output)
		{	
			string filePath = GetFullPersistentPath (fileName, directory);
			output = String.Empty;
			if (!FileExists (filePath)) {
				output = Constants.FILE_NOT_FOUND;
				return false;
			}
			try {
				File.Delete (filePath);
				return true;
			} catch (IOException ex) {
				output += ex.Message.ToString ();
				return false;
			}
		}

		public void DeleteAllFiles (string directory)
		{
			string[] files = Directory.GetFiles (Application.persistentDataPath + "/");
			string output = "";
			for (int i = 0; i < files.Length; i++) {
				DeleteFile (files [i], directory, out output);
			}
		}
		#endregion Delete

	}
}
