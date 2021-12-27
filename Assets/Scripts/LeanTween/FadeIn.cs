using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class FadeIn : MonoBehaviour {
    private CanvasGroup canvasGrp;

    [SerializeField] private float time = .5f;
    [SerializeField] private float maxAlpha = 1f;

    [HideInInspector] public bool useDelay;
    [HideInInspector] public float delay;

    [HideInInspector] public bool overrideWithCurve;
    [HideInInspector] public LeanTweenType easeType = LeanTweenType.easeOutCubic;
    [HideInInspector] public AnimationCurve easeCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [HideInInspector] public UnityEvent OnTweenComplete;
    private CanvasGroup CanvasGrp {
        get {
            if (canvasGrp == null) {
                canvasGrp = GetComponent<CanvasGroup>();
            }

            return canvasGrp;
        }
    }

    private void OnEnable() {
        Fade();
    }

    public void Fade() {
        LTDescr tween = LeanTween.value(gameObject, value => { CanvasGrp.alpha = value; }, 0, maxAlpha, time);

        if (useDelay) {
            tween.setDelay(delay);
        }

        if (overrideWithCurve) {
            tween.setEase(easeCurve);
        }
        else {
            tween.setEase(easeType);
        }

        tween.setOnComplete(() => OnTweenComplete?.Invoke());
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(FadeIn))]
public class FadeInEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        FadeIn controller = target as FadeIn;

        EditorGUILayout.Space(5);
        controller.useDelay = GUILayout.Toggle(controller.useDelay, "Use delay");

        if (controller.useDelay) {
            controller.delay = EditorGUILayout.FloatField("Delay", controller.delay);
        }

        EditorGUILayout.Space(1);
        controller.overrideWithCurve = GUILayout.Toggle(controller.overrideWithCurve, "Override with curve");

        EditorGUILayout.Space(1);

        if (controller.overrideWithCurve) {
            controller.easeCurve = EditorGUILayout.CurveField("Spawn curve", controller.easeCurve);
        }
        else {
            controller.easeType = (LeanTweenType) EditorGUILayout.EnumPopup("Ease type", controller.easeType);
        }

        EditorGUILayout.Space(5);
        SerializedProperty spawnEvent = serializedObject.FindProperty("OnTweenComplete");
        EditorGUIUtility.LookLikeControls();
        EditorGUILayout.PropertyField(spawnEvent);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif