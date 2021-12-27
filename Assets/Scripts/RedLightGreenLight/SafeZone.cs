using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SafeZone : MonoBehaviour {
    [SerializeField] private TurretsController turretsController;

    [Space] public UnityEvent OnWin;

    private void OnTriggerEnter(Collider other) {
        Character character = other.GetComponent<Character>();

        if (character != null) {
            if (character is RedLightGreenLightBotController) {
                RedLightGreenLightBotController bot = (RedLightGreenLightBotController) character;
                bot.safetyState = SafetyState.Safe;
                StartCoroutine(CrStopBotFromRunning(bot));
            }
            else if (character is PlayerController) {
                StartCoroutine(CrPlayerWon());
            }

            turretsController.RemoveTargetFromList(character);
        }
    }

    private IEnumerator CrStopBotFromRunning(RedLightGreenLightBotController bot) {
        yield return new WaitForSecondsRealtime(1f);
        bot.state = CharacterState.Idle;
    }

    private IEnumerator CrPlayerWon() {
        yield return new WaitForSecondsRealtime(1f);

        OnWin?.Invoke();
    }
}