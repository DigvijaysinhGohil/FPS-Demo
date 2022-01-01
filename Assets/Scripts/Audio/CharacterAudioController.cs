using UnityEngine;

public class CharacterAudioController : MonoBehaviour {
    [SerializeField] private AudioSource deathSource;
    [SerializeField] private AudioSource runSource;

    public void DeathSfx() {
        if (!deathSource.isPlaying) {
            deathSource.Play();
        }
    }

    public void RunSfx(bool play) {
        if (!play) {
            runSource.Stop();
            return;
        }
        
        if (!runSource.isPlaying) {
            runSource.Play();
        }
    }

    [ContextMenu("Get run source")]
    public void GetRunSource() {
        runSource = transform.Find("RunningFootStepSfx").GetComponent<AudioSource>();
    }
}