using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseManager : BaseMenuController {

    [SerializeField]
    private KeyCode _pauseKeyCode;

    [SerializeField]
    private bool _gameIsPaused;

    [SerializeField]
    private GameObject _pausePanel;

    [SerializeField]
    private string _mainMenuSceneName = "main_menu";

    private void Start() {
        _pausePanel.SetActive(false);
    }

    // Update is called once per frame
	protected override void Update() {
        if (_gameIsPaused) 
            base.Update();

	    CheckPauseInput();
	}

    protected override void SelectOption(int option) {
        switch (option) {
            case 0:
                Unpause();
                break;
            case 1:
                GotoMainMenu();
                break;
        }
    }

    private void CheckPauseInput() {
        if (Input.GetKeyDown(_pauseKeyCode))
            TogglePause();
    }

    private void TogglePause() {
        if (_gameIsPaused)
            Unpause();
        else
            Pause();
    }

    private void Pause() {
        _gameIsPaused = true;
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
        Debug.Log("Pause");
    }

    public void Unpause() {
        _gameIsPaused = false;
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
        Debug.Log("Unpause");
    }

    public void GotoMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(_mainMenuSceneName);
    }
}