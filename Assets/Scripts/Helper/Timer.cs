using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour{
    private bool hasTimerStarted = false;
    private float counter;
    
    [SerializeField] private float duration;
    public float remainingTime;

    [Space] public UnityEvent OnTimerExpire;

    private void Update(){
        if (hasTimerStarted){
            float elapsedTime = Time.time - counter;
            remainingTime = duration - elapsedTime;

            if (elapsedTime >= duration){
                StopTimer();
                OnTimerExpire?.Invoke();
            }
        }
    }

    public void StartTimer(){
        counter = Time.time;
        hasTimerStarted = true;
    }
    
    public void StartTimer(float duration){
        this.duration = duration;
        counter = Time.time;
        hasTimerStarted = true;
    }

    public void ResumeTimer(){
        duration = remainingTime;
        counter = Time.time;
        hasTimerStarted = true;
    }

    public void StopTimer(){
        hasTimerStarted = false;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Timer))]
public class TimerEditor : Editor{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        Timer controller = target as Timer;

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Stop Timer")){
            controller.StopTimer();
        }

        if (GUILayout.Button("Resume Timer")){
            controller.ResumeTimer();
        }

        GUILayout.EndHorizontal();
    }
}
#endif