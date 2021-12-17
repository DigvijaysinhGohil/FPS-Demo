using UnityEditor;
using UnityEngine;

public class LightsController : MonoBehaviour{
    [SerializeField] private float minDuration = 3f;
    [SerializeField] private float maxDuration = 15f;

    [Space, SerializeField] private MeshRenderer greenLight;
    [SerializeField] private MeshRenderer redLight;

    [Header("Materials"), SerializeField] private Material redLightMat;
    [SerializeField] private Material redLightEmitMat;
    [SerializeField] private Material greenLightMat;
    [SerializeField] private Material greenLightEmitMat;

    private void OnDestroy(){
        TurnOnRedLight();
    }

    public void TurnOnGreenLight(){
        greenLight.material = greenLightEmitMat;
        redLight.material = redLightMat;
    }

    public void TurnOnRedLight(){
        redLight.material = redLightEmitMat;
        greenLight.material = greenLightMat;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LightsController))]
public class LightsControllerEditor : Editor{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        LightsController controller = target as LightsController;

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Green Light")){
            controller.TurnOnGreenLight();
        }

        if (GUILayout.Button("Red Light")){
            controller.TurnOnRedLight();
        }

        GUILayout.EndHorizontal();
    }
}
#endif