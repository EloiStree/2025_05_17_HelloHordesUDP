using System;
using UnityEngine;
using UnityEngine.Events;
namespace Eloi.Hordes
{

    public class HordesMono_RemeberStringPlayerPrefs : MonoBehaviour
    {

        public void Reset()
        {
            m_guid = Guid.NewGuid().ToString().Replace("-", "");
        }

        public string m_guid;
        public UnityEvent<string> m_onStringSaved = new UnityEvent<string>();

        public bool m_loadOnAwake = true;
        public SaveType m_saveType = SaveType.File;
        public enum SaveType
        {
            PlayerPrefs,
            File
        }


        private void Awake()
        {
            if (m_loadOnAwake)
            {
                LoadSavedString();
            }
        }

        public string GetSavePath()
        {
            string path = Application.persistentDataPath + "/FilePlayerPrefs/" + m_guid;
            return path;
        }
        public void SaveString(string inputText)
        {
            if (m_saveType == SaveType.File)
            {
                try
                {
                    string path = GetSavePath();

                    Debug.Log("Loading string from path: " + path, this.gameObject);
                    if (!System.IO.Directory.Exists(Application.persistentDataPath + "/FilePlayerPrefs"))
                    {
                        System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/FilePlayerPrefs");
                    }
                    System.IO.File.WriteAllText(path, inputText);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error saving string to file: " + e.Message);
                }
            }
            else
            {
                PlayerPrefs.SetString(m_guid, inputText);
                PlayerPrefs.Save();
            }
        }

        public void LoadSavedString()
        {


            if (m_saveType == SaveType.File)
            {

                string path = GetSavePath();
                Debug.Log("Loading string from path: " + path, this.gameObject);
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        string found = System.IO.File.ReadAllText(path);
                        if (string.IsNullOrEmpty(found))
                            return;
                        m_onStringSaved?.Invoke(found);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Error loading string from file: " + e.Message);
                    }
                }
                return;
            }
            else
            {
                if (PlayerPrefs.HasKey(m_guid))
                {
                    string found = PlayerPrefs.GetString(m_guid);
                    if (string.IsNullOrEmpty(found))
                        return;

                    m_onStringSaved?.Invoke(PlayerPrefs.GetString(m_guid));
                }
            }

        }
    }
}