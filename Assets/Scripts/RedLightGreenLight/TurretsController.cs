using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretsController : MonoBehaviour {
    private System.Random random;
    private Character player;

    [SerializeField] private float minThreshold = .2f;
    [SerializeField] private float maxThreshold = .5f;

    [SerializeField] private LightsController lightsController;
    [SerializeField] private Timer lightTimer;

    [Space] public List<Character> targets;

    [Space, SerializeField] private TurretAudioController turretAudio;

    private void Awake() {
        random = new System.Random();
        targets = new List<Character>(new List<Character>(FindObjectsOfType<Character>()).OrderBy(target => random.Next()));
        player = targets.Find(target => target is PlayerController);
        lightsController.OnRedLightStarted.AddListener(KillTargets);
    }

    private void OnDestroy() {
        lightsController.OnRedLightStarted.RemoveListener(KillTargets);
    }

    private void Update() {
        if (player == null) {
            return;
        }

        if (!lightsController.IsGreenLightOn &&
            (player.state == CharacterState.Run || player.state == CharacterState.Walk)) {
            KillPlayer();
        }
    }

    private void KillPlayer() {
        StartCoroutine(CrKillPlayer());
    }

    private IEnumerator CrKillPlayer() {
        yield return new WaitForSeconds(Random.Range(minThreshold, maxThreshold));
        lightTimer.StopTimer();

        if (player.state == CharacterState.Run || player.state == CharacterState.Walk) {
            if (targets.Contains(player)) {
                turretAudio.ShootSfx();
                player.Die();
            }
        }

        lightTimer.ResumeTimer();
    }

    private void KillTargets() {
        StartCoroutine(CrKillTargets());
    }

    private IEnumerator CrKillTargets() {
        yield return new WaitForSeconds(Random.Range(minThreshold, maxThreshold));
        lightTimer.StopTimer();

        Character[] targetsToKill = targets.Where(target => target.state != CharacterState.Idle).ToArray();

        foreach (Character character in targetsToKill) {
            turretAudio.ShootSfx();
            character.Die();

            yield return new WaitForSeconds(Random.Range(.5f, 1f));
            RemoveTargetFromList(character);
        }

        lightTimer.ResumeTimer();
    }

    public void KillAll() {
        player = null;
        StopAllCoroutines();
        StartCoroutine(CrKillAll());
    }

    private IEnumerator CrKillAll() {
        yield return new WaitForSeconds(Random.Range(minThreshold, maxThreshold));
        lightTimer.StopTimer();

        Character[] targetsToKill = targets.ToArray();

        foreach (Character character in targetsToKill) {
            turretAudio.ShootSfx();
            character.Die();

            yield return new WaitForSeconds(Random.Range(.5f, 1f));
            RemoveTargetFromList(character);
        }
    }

    public void RemoveTargetFromList(Character targetToRemove) {
        targets.Remove(targetToRemove);
    }
}