using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance { set; get; }

    [SerializeField]
    private int _enemiesToKill;

    [SerializeField]
    private int _enemiesKilled;

    [SerializeField]
    private string _nextLevelName;

    [SerializeField]
    private float _restartTime;

    [SerializeField]
    private GameObject _gotoNextLevelObstacle;

    [SerializeField]
    private CollisionTrigger _gotoNextLevelTrigger;

    private ArrayList actors = new ArrayList();

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        SortActorsLayer();
    }

    private void SortActorsLayer() {
        SortActorsByY();
        int startLayer = 2;
        SetActorsLayer(startLayer);
    }

    private void SetActorsLayer(int startLayer) {
        for (int i = 0; i < actors.Count; i++) {
            Transform spriteTransform = (Transform)actors[i];
            SpriteRenderer sprite = spriteTransform.GetComponent<SpriteRenderer>();
            sprite.sortingOrder = startLayer + i;
        }
    }

    private void SortActorsByY() {
        for (int i = actors.Count; i > 0; i--) {
            for (int j = 0; j < i - 1; j++) {
                Transform actor1 = (Transform)actors[j];
                Transform actor2 = (Transform)actors[j + 1];

                if (actor1 != null && actor2 != null &&
                    actor1.position.y < actor2.position.y) {
                    actors[j] = actor2;
                    actors[j + 1] = actor1;
                }
            }
        }
    }

    public void OnMainCharacterKilled() {
        Invoke("LoseLevel", _restartTime);
    }

   public void OnEnemyKilled() {
        _enemiesKilled++;
        if (_enemiesKilled >= _enemiesToKill)
            WinLevel();
        GameUIManager.Instance.EnemiesKilledCount = _enemiesKilled;
    }

    private void WinLevel() {
        _gotoNextLevelTrigger.OnCollision = GotoNextLevel;
        _gotoNextLevelObstacle.SetActive(false);
        KillAllEnemies();
        StopAllSpawnPools();
        GameUIManager.Instance.SetArrowVisible(true);
    }

    private void KillAllEnemies() {
        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();
        foreach (BaseEnemy enemy in enemies)
            Destroy(enemy.gameObject, enemy.dieTime);
    }

    private void StopAllSpawnPools() {
        SpawnPool[] spawnPools = FindObjectsOfType<SpawnPool>();
        foreach (SpawnPool spawnPool in spawnPools)
            Destroy(spawnPool.gameObject);
    }

    private void GotoNextLevel() {
        Application.LoadLevel(_nextLevelName);
    }

    private void LoseLevel() {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void AddActor(Transform actor) {
        actors.Add(actor);
    }

    public void RemoveActor(Transform actor) {
        actors.Remove(actor);
    }
}
