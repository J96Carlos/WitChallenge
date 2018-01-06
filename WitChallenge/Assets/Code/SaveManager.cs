using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//Script by @author - SaveManager
public class SaveManager : MonoBehaviour {

    public string m_DataName;
    public SaveInfo[] m_CharsInfo;

    bool infoUpdated = false;

    string Path { get { return Application.persistentDataPath + "/" + m_DataName + ".bin"; } }

    /// <summary>Sets all the info related to a character on a determined index.</summary>
    public void SetCharValues (int charIndex, string charName, int charEmote, int charAnim) {
        infoUpdated = m_CharsInfo[charIndex].name != charName || m_CharsInfo[charIndex].anim != charAnim || m_CharsInfo[charIndex].emote != charEmote;
        if (!infoUpdated) return;

        m_CharsInfo[charIndex].name = charName;
        m_CharsInfo[charIndex].anim = charAnim;
        m_CharsInfo[charIndex].emote = charEmote;
    }

    /// <summary>Returns all the character data from an index.</summary>
    public void GetCharValues (int index, out string name, out int emote, out int anim) {
        name = m_CharsInfo[index].name;
        emote = m_CharsInfo[index].emote;
        anim = m_CharsInfo[index].anim;
    }

    /// <summary>Saves the characters data.</summary>
    public void Save () {
        if (!infoUpdated) return;
        infoUpdated = false;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(Path);

        formatter.Serialize(fileStream, m_CharsInfo);
        fileStream.Close();
    }

    /// <summary>Loads the characters data.</summary>
    public void Load () {
        if (File.Exists(Path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Path, FileMode.Open);

            m_CharsInfo = (SaveInfo[])formatter.Deserialize(fileStream);

            fileStream.Close();
        }
    }

    [System.Serializable]
    public struct SaveInfo {
        public string name;
        public int emote;
        public int anim;
    }

}