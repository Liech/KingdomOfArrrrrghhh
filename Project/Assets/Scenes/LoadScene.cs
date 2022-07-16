using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string SceneName = "MainScene";
    public static string currentScene = "MainScene";
    public void loadScene() {
        SceneManager.LoadScene(SceneName);
        currentScene = SceneName;
    }
    public void loadCurrentScene() {
        SceneManager.LoadScene(currentScene);
    }
}
