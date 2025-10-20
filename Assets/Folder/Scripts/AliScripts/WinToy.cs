using UnityEngine;
using UnityEngine.SceneManagement;

public class WinToy : MonoBehaviour
{

    // [Header("UI Elements")]
    // [SerializeField] private GameObject WinScreen;
    // public string nextLevel;
    // public string mainMenuScene = "MainMenu";
    [Header("Settings")]
    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Reached the Toy! You Win!");
            Time.timeScale = 0f;
            hasWon = true;
        }
    }
    public void OnNextButton()
    {
        Time.timeScale = 1f;
    }

    public void OnRetryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
    }

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}