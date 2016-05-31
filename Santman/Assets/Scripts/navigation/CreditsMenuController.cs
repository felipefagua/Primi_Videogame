using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsMenuController : MonoBehaviour {

    [SerializeField]
    private float _delay;

    [SerializeField]
    private string _mainMenuSceneSceneName;

    public void GotoMainMenuScene() {
        Invoke("LoadMainMenuScene", _delay);
    }

    public void LoadMainMenuScene() {
        SceneManager.LoadScene(_mainMenuSceneSceneName);
    }
}
