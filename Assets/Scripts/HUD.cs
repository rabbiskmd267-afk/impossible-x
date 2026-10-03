
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text levelText;
    public Text failText;
    public PlayerController player;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (GameManager.Instance == null) return;

        if (levelText != null)
            levelText.text =
                "LEVEL " + GameManager.Instance.level;

        if (failText != null)
            failText.text =
                "FAILS " + GameManager.Instance.fails;
    }

    public void JumpButton()
    {
        if (player != null)
            player.Jump();
    }

    public void RestartButton()
    {
        if (player != null)
            player.Respawn();
    }
}
