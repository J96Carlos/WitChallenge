using UnityEngine;
using UnityEngine.UI;

//Script by @JCarlos - ToggleHolder
public class ToggleHolder : MonoBehaviour {

    [SerializeField]
    Toggle m_Toggle;
    [SerializeField]
    Text m_Text;

    int m_Index;
    bool m_AnimToggle;
    CustomManager m_Manager;

    /// <summary>Inits the toggle.</summary>
    public void Init (int index, bool animToggle, bool on, string animName, ToggleGroup group, CustomManager menu) {
        m_AnimToggle = animToggle;
        m_Index = index;
        m_Toggle.group = group;
        m_Manager = menu;
        m_Toggle.isOn = on;
        m_Text.text = animName;
    }

    public void UI_ChangeParameter () {
        if (m_Toggle.isOn) {
            if (m_AnimToggle) m_Manager.ChangeAnim(m_Index);
            else m_Manager.ChangeEmote(m_Index);
        }
    }

}