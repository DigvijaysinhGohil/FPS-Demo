using UnityEngine;

public class CursorVisibilityController : MonoBehaviour
{
    private void OnEnable() {
        CursorVisibility.HideCursor();
    }

    private void OnDisable() {
        CursorVisibility.ShowCursor();
    }
}
