using UnityEngine;
using UnityEngine.EventSystems;

//Script by @JCarlos - MyUtils
namespace MyLibs {

    public static class MyUtils {

        /// <returns>Returns true if the mouse is over an UI element.</returns>
        public static bool IsMouseOverUI () {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}