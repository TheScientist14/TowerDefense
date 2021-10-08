using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;


public class StartButtonBehaviour : MonoBehaviour
{
    void OnMouseDown()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Main.unity");
    }
}
