using UnityEngine;
using System.Collections;

public class quit : MonoBehaviour
{
    public void Example()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://www.google.com");
#else
        Application.Quit();
#endif
    }
}