using UnityEngine;
using UnityEngine.SceneManagement;

public class WinToy : MonoBehaviour
{

    [Header("UI Elements")]
    [SerializeField] private GameObject WinScreen;
    public string mainMenuScene = "MainMenu";
    [Header("Settings")]
    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Reached the Toy! You Win!");
            WinScreen.SetActive(true);
            Time.timeScale = 0f;
            hasWon = true;
        }
    }
    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}