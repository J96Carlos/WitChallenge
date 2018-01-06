using System.Collections;
using UnityEngine;

//Script by @JCarlos - Bilboard
public class Bilboard : MonoBehaviour {

    [SerializeField, Tooltip("If null, the script will look for the main camera.")]
    Camera cam = null;

    void Awake () {
        if (!cam) cam = Camera.main;
        StartCoroutine(BilboardUpdate());
    }

    IEnumerator BilboardUpdate () {
        while (true) {
            transform.forward = cam.transform.forward;
            yield return null;
        }
    }

}