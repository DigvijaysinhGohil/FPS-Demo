using System.Collections;
using UnityEngine;

public class SafeZone : MonoBehaviour {
    [SerializeField] private TurretsController turretsController;

    private void OnTriggerEnter(Collider other) {
        Character character = other.GetComponent<Character>();

        if (character != null) {
            if (character is RedLightGreenLightBotController) {
                RedLightGreenLightBotController bot = (RedLightGreenLightBotController) character;
                bot.safetyState = SafetyState.Safe;
                StartCoroutine(CrStopBotFromRunning(bot));
            }

            turretsController.RemoveTargetFromList(character);
        }
    }

    private IEnumerator CrStopBotFromRunning(RedLightGreenLightBotController bot) {
        yield return new WaitForSecondsRealtime(1f);
        bot.state = CharacterState.Idle;
    }
}