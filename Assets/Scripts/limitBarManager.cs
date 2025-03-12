using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LimitBarManager : MonoBehaviour
{
    [Header("Game Settings")]
    public int pickableLimit = 1000;

    [Header("UI References")]
    public Image limitBar;

    public Text countText;

    void Update()
    {
        int currentCount = GameObject.FindGameObjectsWithTag("input data").Length + GameObject.FindGameObjectsWithTag("output data").Length;

        // Calculate the fill fraction: current count divided by the limit, clamped between 0 and 1.
        float fillAmount = Mathf.Clamp01((float)currentCount / pickableLimit);

        // Update the limit bar's fill.
        if (limitBar != null)
        {
            limitBar.fillAmount = fillAmount;
        }

        // Optionally update the text element to show the count.
        if (countText != null)
        {
            countText.text = currentCount + " / " + pickableLimit;
        }

        // Check if the limit has been reached or exceeded.
        if (currentCount >= pickableLimit)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over! Pickable limit reached.");
        SceneManager.LoadScene("GameOverScene");
    }
}
