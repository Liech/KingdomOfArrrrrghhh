using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string SceneName = "MainScene";
    public static string currentScene = "MainScene";
    [Header("Audio")]
    public AK.Wwise.Event ClickMenuItem;
    public void loadScene() {
        Debug.Log("ClickMenu");
        ClickMenuItem.Post(gameObject);
        SceneManager.LoadScene(SceneName);
        currentScene = SceneName;
    }
    public void loadCurrentScene() {
        Debug.Log("ClickMenu");
        ClickMenuItem.Post(gameObject);
        SceneManager.LoadScene(currentScene);
    }
}
