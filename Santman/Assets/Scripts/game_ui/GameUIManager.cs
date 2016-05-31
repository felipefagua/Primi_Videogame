using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    private const string COUNT_PREFIX = "x ";

    public static GameUIManager Instance { get; set; }

    [SerializeField]
    private Text _enemyCountTextField;

    [SerializeField]
    private int _enemiesKilled = 0;

    [SerializeField]
    private Image[] _livesIcons;

    [SerializeField]
    private int _lives;

    [SerializeField]
    private NextLevelArrow _arrow;

    private void Awake() {
        Instance = this;
    }

    public int EnemiesKilledCount {
        set {
            _enemiesKilled = value;
            UpdateEnemiesTextField();    
        }
        get {
            return _enemiesKilled;
        }
    }

    public int LivesCount {
        set {
            _lives = value;
            UpdateLivesIcons();    
        }
        get {
            return _lives;
        }
        
    }

    private void UpdateGameUI() {
        UpdateEnemiesTextField();
        UpdateLivesIcons();
    }

    private void UpdateEnemiesTextField() {
        _enemyCountTextField.text = COUNT_PREFIX + _enemiesKilled;    
    }

    private void UpdateLivesIcons() {
        for (int i = 0; i < _livesIcons.Length; i++) {
            bool visible = i < _lives;
            _livesIcons[i].enabled = visible;
        }
    }

    public void SetArrowVisible(bool visible) {
        _arrow.gameObject.SetActive(visible);
    }

}
