using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class LightsController : MonoBehaviour {
    private bool isGreenLightOn = false;

    [SerializeField] private float minDuration = 3f;
    [SerializeField] private float maxDuration = 15f;

    [Space, SerializeField] private MeshRenderer greenLight;
    [SerializeField] private MeshRenderer redLight;

    [Header("Materials"), SerializeField] private Material redLightMat;
    [SerializeField] private Material redLightEmitMat;
    [SerializeField] private Material greenLightMat;
    [SerializeField] private Material greenLightEmitMat;

    [Space, SerializeField] private Timer timer;

    [Space]
    public UnityEvent OnGreenLightStarted;
    public UnityEvent OnRedLightStarted;

    public bool IsGreenLightOn {
        get { return isGreenLightOn; }
        private set {
            isGreenLightOn = value;

            if (isGreenLightOn) {
                OnGreenLightStarted?.Invoke();
            }
            else {
                OnRedLightStarted?.Invoke();
            }
        }
    }

    private void Awake() {
        timer.OnTimerExpire.AddListener(ChangeLight);
        TurnOnRedLight();
    }

    private void OnDestroy() {
        timer.OnTimerExpire.RemoveListener(ChangeLight);
    }

    private void ChangeLight() {
        if (IsGreenLightOn) {
            TurnOnRedLight();
        }
        else {
            TurnOnGreenLight();
        }
    }

    private void SetRandomTimeDuration() {
        float duration = Random.Range(minDuration, maxDuration);
        timer.StartTimer(duration);
    }

    public void TurnOnGreenLight() {
        IsGreenLightOn = true;
        greenLight.material = greenLightEmitMat;
        redLight.material = redLightMat;
        SetRandomTimeDuration();
    }

    public void TurnOnRedLight() {
        IsGreenLightOn = false;
        redLight.material = redLightEmitMat;
        greenLight.material = greenLightMat;
        SetRandomTimeDuration();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LightsController))]
public class LightsControllerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        LightsController controller = target as LightsController;

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Green Light")) {
            controller.TurnOnGreenLight();
        }

        if (GUILayout.Button("Red Light")) {
            controller.TurnOnRedLight();
        }

        GUILayout.EndHorizontal();
    }
}
#endif