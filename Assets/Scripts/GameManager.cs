
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int level = 1;
    public int fails = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        level = PlayerPrefs.GetInt("Level", 1);
        fails = PlayerPrefs.GetInt("Fails", 0);
    }

    public void RegisterFail()
    {
        fails++;
        PlayerPrefs.SetInt("Fails", fails);
        PlayerPrefs.Save();
    }

    public void CompleteLevel()
    {
        level++;
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void Restart()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
