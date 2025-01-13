using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Для редактора Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Для сборки
        Application.Quit();
#endif
    }
}
