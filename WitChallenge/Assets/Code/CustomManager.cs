using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Script by @JCarlos - CustomizationMenu
public class CustomManager : MonoBehaviour {

    [Header("Vals")]
    [SerializeField]
    string m_MainSceneName;

    [Header("Refs")]
    [SerializeField]
    Transform m_CharHolder;
    [SerializeField]
    SaveManager m_SaveManager;
    [SerializeField]
    Character[] m_Chars;

    [Header("UI Refs")]
    [SerializeField]
    ToggleHolder m_TogglePref;
    [SerializeField]
    InputField m_InputCharName;
    [SerializeField]
    ToggleGroup m_ToggleGroupAnims = null, m_ToggleGroupsEmotes = null;
    [SerializeField]
    GameObject m_RectAnim = null, m_RectEmotes = null;
    [SerializeField]
    GridLayoutGroup m_GridAnim = null, m_GridEmotes = null;
    [SerializeField]
    Button m_AnimationBtn = null, m_EmotesBtn = null;
    [SerializeField]
    Text[] m_ResizeTexts;

    int m_CharIndex;
    Character m_Char;
    RectTransform m_AnimHolder, m_EmoteHolder;


    #region Methods

    #region UnityMethods
    void Awake () {
        //Get components
        m_AnimHolder = m_GridAnim.GetComponent<RectTransform>();
        m_EmoteHolder = m_GridEmotes.GetComponent<RectTransform>();

        //Load save
        m_SaveManager.Load();
        m_CharIndex = PlayerPrefs.GetInt("SelectedChar", 0);
        m_Char = SpawnCharacter();
        string cName;
        int cEmote, cAnim;
        m_SaveManager.GetCharValues(m_CharIndex, out cName, out cEmote, out cAnim);
        m_Char.SetValues(cName, cAnim, cEmote);

        //Init
        StartCoroutine(ResizeMenu());
        Init();

        //Pos-Init
        UI_ChangeScroll(true);

    }
    #endregion

    #region PrivateMethods
    Character SpawnCharacter () {
        Character c = Instantiate(m_Chars[m_CharIndex]);
        c.transform.SetParent(m_CharHolder);
        c.transform.localPosition = Vector3.zero;
        c.transform.localScale = Vector3.one;
        c.transform.rotation = Quaternion.identity;
        return c;
    }

    void Init () {
        m_InputCharName.text = m_Char.CharName;
        for (int x = 0; x < m_Char.Animations.Length; ++x) {
            ToggleHolder h = Instantiate(m_TogglePref);
            h.Init(x, true, x == m_Char.CharAnimation, m_Char.Animations[x], m_ToggleGroupAnims, this);
            h.transform.SetParent(m_GridAnim.transform);
        }
        for (int x = 0; x < m_Char.Emotes.Length; ++x) {
            ToggleHolder h = Instantiate(m_TogglePref);
            h.Init(x, false, x == m_Char.CharEmote, m_Char.Emotes[x].name, m_ToggleGroupsEmotes, this);
            h.transform.SetParent(m_GridEmotes.transform);
        }
        m_AnimHolder.sizeDelta = new Vector2(m_AnimHolder.sizeDelta.x, m_Char.Animations.Length * 52);
        m_EmoteHolder.sizeDelta = new Vector2(m_EmoteHolder.sizeDelta.x, m_Char.Emotes.Length * 52);
    }

    #endregion

    #region PublicMethods
    /// <summary>Switchs between the animation scroll and the emotes scroll.</summary>
    /// <param name="animations">true if you want the animation scroll.</param>
    public void UI_ChangeScroll (bool animations) {
        m_RectAnim.SetActive(animations);
        m_RectEmotes.SetActive(!animations);
        m_EmotesBtn.interactable = animations;
        m_AnimationBtn.interactable = !animations;
    }

    /// <summary>Changes the Char name.</summary>
    public void UI_ChangeCharName () {
        m_Char.CharName = m_InputCharName.text;
    }

    /// <summary>Sets the Char animation.</summary>
    /// <param name="index">The animation index.</param>
    public void ChangeAnim (int index) {
        m_Char.CharAnimation = index;
    }

    /// <summary>Sets the Char emote.</summary>
    /// <param name="index"></param>
    public void ChangeEmote (int index) {
        m_Char.CharEmote = index;
    }

    /// <summary>Applies all the changed settings and loads the main scene.</summary>
    public void UI_Apply () {
        m_SaveManager.SetCharValues(m_CharIndex, m_Char.CharName, m_Char.CharEmote, m_Char.CharAnimation);
        m_SaveManager.Save();
        SceneManager.LoadScene(m_MainSceneName);
    }

    /// <summary>Discards the changes and loads the main scene.</summary>
    public void UI_Discard () {
        SceneManager.LoadScene(m_MainSceneName);
    }
    #endregion

    #endregion

    #region Courotines
    /// <summary>Courotine to check if the screen resolution over the gameplay, without interfer with the main thread.</summary>
    IEnumerator ResizeMenu () {
        Vector2Int screenSize = Vector2Int.zero;
        while (true) {
            Vector2Int newScreenSize = new Vector2Int(Screen.width, Screen.height);
            if (screenSize != newScreenSize) {
                screenSize = newScreenSize;
                m_GridAnim.cellSize = m_GridEmotes.cellSize = new Vector2(Screen.width * 0.3f - 35, 50);
                StartCoroutine(ResizeText(m_ResizeTexts));
            }
            yield return null;
        }
    }

    /// <summary>Courotine to resize all the texts with the same size for a better UI look.</summary>
    IEnumerator ResizeText (Text[] texts) {
        yield return new WaitForEndOfFrame();
        int min = 1000;
        foreach (Text t in texts) min = Mathf.Min(min, t.cachedTextGenerator.fontSizeUsedForBestFit);
        foreach (Text t in texts) t.resizeTextMaxSize = min;
    }
    #endregion

}