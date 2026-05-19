using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    void Start()
    {
        Invoke("nextScene", 5f);
    }
    void nextScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}