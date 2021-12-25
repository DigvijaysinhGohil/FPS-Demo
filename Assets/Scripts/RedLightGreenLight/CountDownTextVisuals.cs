using TMPro;
using UnityEngine;

public class CountDownTextVisuals : MonoBehaviour {
    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text timerText;

    private void Start() {
        timer.StartTimer();
    }

    private void Update() {
        SetTimeVisuals();
    }

    private void SetTimeVisuals() {
        float remainingTime = timer.remainingTime;

        if (remainingTime < 0) {
            remainingTime = 0;
        }

        int seconds = (int) (remainingTime % 60);
        int minutes = (int) (remainingTime / 60);
        int milliseconds = (int) (remainingTime * 1000 % 1000);

        timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}