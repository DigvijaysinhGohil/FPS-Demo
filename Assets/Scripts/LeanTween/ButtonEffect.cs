using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Button button;

    [HideInInspector] public float timeHover = .5f;
    [HideInInspector] public bool overrideWithCurveHover;
    [HideInInspector] public LeanTweenType easeTypeHover = LeanTweenType.easeOutCubic;
    [HideInInspector] public AnimationCurve easeCurveHover = AnimationCurve.Linear(0, 0, 1, 1);

    [HideInInspector] public float timeClick = .5f;
    [HideInInspector] public bool overrideWithCurveClick;
    [HideInInspector] public LeanTweenType easeTypeClick = LeanTweenType.easeOutCubic;
    [HideInInspector] public AnimationCurve easeCurveClick = AnimationCurve.Linear(0, 0, 1, 1);

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickScale);
    }

    private void OnDestroy() {
        button.onClick.RemoveListener(ClickScale);
    }

    private void ClickScale() {
        LTDescr tween = LeanTween.value(gameObject, scale => { transform.localScale = scale; }, Vector3.one,
            Vector3.one * 1.5f, timeHover);

        if (overrideWithCurveClick) {
            tween.setEase(easeCurveClick);
        }
        else {
            tween.setEase(easeTypeClick);
        }
    }

    private void HoverScale(Vector3 size) {
        LTDescr tween = LeanTween.scale(gameObject, size, timeHover);

        if (overrideWithCurveHover) {
            tween.setEase(easeCurveHover);
        }
        else {
            tween.setEase(easeTypeHover);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        HoverScale(Vector3.one * 1.2f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        HoverScale(Vector3.one);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ButtonEffect))]
public class ButtonEffectEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        ButtonEffect controller = target as ButtonEffect;

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Hover Settings");
        controller.timeHover = EditorGUILayout.FloatField("Hover time", controller.timeHover);
        controller.overrideWithCurveHover = GUILayout.Toggle(controller.overrideWithCurveHover, "Override with curve");

        EditorGUILayout.Space(1);

        if (controller.overrideWithCurveHover) {
            controller.easeCurveHover = EditorGUILayout.CurveField("Spawn curve", controller.easeCurveHover);
        }
        else {
            controller.easeTypeHover = (LeanTweenType) EditorGUILayout.EnumPopup("Ease type", controller.easeTypeHover);
        }

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Click Settings");
        controller.timeClick = EditorGUILayout.FloatField("Click time", controller.timeClick);
        controller.overrideWithCurveClick = GUILayout.Toggle(controller.overrideWithCurveClick, "Override with curve");

        EditorGUILayout.Space(1);

        if (controller.overrideWithCurveClick) {
            controller.easeCurveClick = EditorGUILayout.CurveField("Spawn curve", controller.easeCurveClick);
        }
        else {
            controller.easeTypeClick = (LeanTweenType) EditorGUILayout.EnumPopup("Ease type", controller.easeTypeClick);
        }
    }
}
#endif