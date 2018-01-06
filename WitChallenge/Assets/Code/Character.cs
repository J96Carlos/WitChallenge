using UnityEngine;
using UnityEngine.UI;

//Script by @JCarlos - Character
public class Character : MonoBehaviour {

    [Header("Refs")]
    [SerializeField]
    Text m_TextCharName;
    [SerializeField]
    Animator m_Animator;
    [SerializeField]
    Image m_EmoteImage;

    [Header("Vals")]
    [SerializeField, Tooltip("The name of each animation.")]
    string[] m_Animations;
    [SerializeField, Tooltip("The Emotes for this character.")]
    EmoteGroup[] m_Emotes;

    string m_CharName;
    int m_CharAnim, m_CharEmote;

    public string CharName {
        get { return m_CharName; }
        set {
            m_CharName = value;
            m_TextCharName.text = value;
        }
    }
    public int CharAnimation {
        get { return m_CharAnim; }
        set {
            m_CharAnim = value;
            m_Animator.SetTrigger(m_Animations[value]);
        }
    }
    public int CharEmote {
        get { return m_CharEmote; }
        set {
            m_CharEmote = value;
            m_EmoteImage.sprite = m_Emotes[value].emote;
        }
    }

    public void SetValues (string name, int anim, int emote) {
        CharName = name;
        CharAnimation = anim;
        CharEmote = emote;
    }

    public string[] Animations { get { return m_Animations; } }
    public EmoteGroup[] Emotes { get { return m_Emotes; } }

    [System.Serializable]
    public struct EmoteGroup {
        public string name;
        public Sprite emote;
    }

}