using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text Hint;
    public Text Version;

    public void Awake()
    {
        string[] quips = new string[3]
        {
            "Working Title", 
            $"Version {Application.version}!",
            "Funnie Easter Egg Text"
        };

        Hint.text = quips[Random.Range(0, quips.Length)];
        Version.text = Application.version;
    }

    public void Update()
    {
        // Sin(Time * Speed) * Amplitude + Offset
        Hint.fontSize = (int)(Mathf.Sin(Time.time * 10f) * 5f + 45f);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenOptions()
    {
        Debug.LogWarning("Options not implemented");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
