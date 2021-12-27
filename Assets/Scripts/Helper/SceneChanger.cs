using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    [SerializeField] private GameObject loader;
    
    public void LoadScene(string sceneName) {
        if (loader != null) {
            loader.SetActive(true);
        }
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(string sceneName) {
        if (loader != null) {
            loader.SetActive(true);
        }
        SceneManager.LoadSceneAsync(sceneName);
    }
}
