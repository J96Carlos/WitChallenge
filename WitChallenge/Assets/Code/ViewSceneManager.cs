using UnityEngine;
using UnityEngine.SceneManagement;

//Script by @author - ViewSceneManager
public class ViewSceneManager : MonoBehaviour {

    [Header("Vals")]
    [SerializeField]
    string m_CustomSceneName;
    [SerializeField]
    LayerMask m_LayerMask;

    [Header("Refs")]
    [SerializeField]
    SaveManager m_SaveManager;
    [SerializeField]
    Character[] m_Characters;

    void Awake () {
        m_SaveManager.Load();
        for (int x = 0; x < m_Characters.Length; ++x) {
            string t_name;
            int t_emote, t_anim;
            m_SaveManager.GetCharValues(x, out t_name, out t_emote, out t_anim);
            m_Characters[x].SetValues(t_name, t_anim, t_emote);
        }
    }

    void Update () {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            ResolveClick(Input.GetTouch(0).position);
        } else if (Input.GetMouseButtonDown(0)) {
            ResolveClick(Input.mousePosition);
        }
    }

    void ResolveClick (Vector2 screenPos) {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit info;
        if (Physics.Raycast(ray, out info, Mathf.Infinity, m_LayerMask)) {
            Character t_char = info.collider.GetComponent<Character>();
            if (t_char) {
                for (int t = 0; t < m_Characters.Length; ++t) {
                    if (t_char == m_Characters[t]) {
                        PlayerPrefs.SetInt("SelectedChar", t);
                        SceneManager.LoadScene(m_CustomSceneName);
                    }
                }
            }
        }
    }


}