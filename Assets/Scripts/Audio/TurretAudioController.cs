using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TurretAudioController : MonoBehaviour
{
    private AudioSource fireSource;

    private void Awake() {
        fireSource = GetComponent<AudioSource>();
    }

    public void ShootSfx() {
        fireSource.Stop();
        fireSource.Play();
    }
}
