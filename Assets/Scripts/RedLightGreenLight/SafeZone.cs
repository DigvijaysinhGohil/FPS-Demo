using System.Collections;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        RedLightGreenLightBotController bot = other.GetComponent<RedLightGreenLightBotController>();

        if (bot != null) {
            bot.safetyState = SafetyState.Safe;

            StartCoroutine(CrStopBotFromRunning(bot));
        }
    }

    private IEnumerator CrStopBotFromRunning(RedLightGreenLightBotController bot) {
        yield return new WaitForSecondsRealtime(1f);
        bot.state = CharacterState.Idle;
    }
}
