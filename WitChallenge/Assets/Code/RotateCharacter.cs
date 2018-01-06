using UnityEngine;

//Script by @JCarlos - Customize
public class RotateCharacter : MonoBehaviour {

    public Transform m_CharHolder;
    public float m_RotationSpeed = 300;
    public float m_TouchReduction = 100;
    public float m_DecreaseRate = 1.05f;

    float t = 0;

    void Awake () {
        m_CharHolder.forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(-1, 0, -1));
    }

    void Update () {
        if (Input.touchCount > 0 && !MyLibs.MyUtils.IsMouseOverUI()) t = -Input.GetTouch(0).deltaPosition.x / m_TouchReduction;
        else if (Input.GetMouseButton(0) && !MyLibs.MyUtils.IsMouseOverUI()) t = -Input.GetAxis("Mouse X");
        m_CharHolder.transform.Rotate(0, m_RotationSpeed * Time.deltaTime * t, 0, Space.Self);
        t /= m_DecreaseRate;
    }
}