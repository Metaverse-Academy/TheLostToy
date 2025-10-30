using UnityEngine;
using UnityEngine.SceneManagement;

public class ggg : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("nextsvenee", 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void nextsvenee()
    {
        SceneManager.LoadScene("GameDesign 1");
    }
}
