using System.Collections;
using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI clickText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private InterstitialController interstitialController;
    private int clicks;
    private Coroutine currentTimerCoroutine;

    private void Start()
    {
        highscoreText.text = $"Highscore: {PlayerPrefs.GetInt("clicks")}";
    }

    public void HandleButtonClick()
    {
        clicks++;
        clickText.text = clicks.ToString() + " Clicks";
        Debug.Log("The button was clicked!");

        if (currentTimerCoroutine != null)
            return;

        currentTimerCoroutine = StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float remainingTime = duration;

        while (remainingTime > 0)
        {
            timerText.text = $"Tiempo: {(int)remainingTime} segundos";

            yield return null;

            remainingTime -= Time.deltaTime;
        }

        timerText.text = "Tiempo: 0 segundos";
        clickText.text = "0 Clicks";
        duration = 10;
        
        if (clicks > PlayerPrefs.GetInt("clicks"))
        {
            highscoreText.text = $"Highscore: {clicks}";
            PlayerPrefs.SetInt("clicks", clicks);
            PlayerPrefs.Save();
        }
        else
        {
            interstitialController.ShowAd();
        }

        clicks = 0;
        duration = 10;
        currentTimerCoroutine = null;
    }

    public void PowerUp()
    {
        if (currentTimerCoroutine != null)
            return;

        duration = 12;
        timerText.text = $"Tiempo: {duration} segundos";
    }
}
