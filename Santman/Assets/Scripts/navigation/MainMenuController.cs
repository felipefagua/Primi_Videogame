using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : BaseMenuController {

    [SerializeField]
    protected float _delay;

    [SerializeField]
    protected string _level1SceneSceneName;

    [SerializeField]
    protected string _creditsSceneName;

	protected override void SelectOption(int option) {
        switch (option) {
            case 0:
                GotoLevel1Scene();
                break;
            case 1:
                GotoCreditsScene();
                break;
        }
    }

    public void GotoLevel1Scene() {
        MoveSelectedOptionBar(0);
        Invoke("LoadLevel1Scene", _delay);
    }

    public void GotoCreditsScene() {
        MoveSelectedOptionBar(1);
        Invoke("LoadCreditsScene", _delay);
    }

    private void LoadLevel1Scene() {
        SceneManager.LoadScene(_level1SceneSceneName);
    }

    private void LoadCreditsScene() {
        SceneManager.LoadScene(_creditsSceneName);
    }
}
