using System;
using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour {

    private Transform _myTransform;

    private MovementController _movementController;

    [SerializeField]
    private float _healthPoints;

    [SerializeField]
    private float _attackDamage;

    [SerializeField]
    private float _attackRange;

    [SerializeField]
    private bool _isDead;

    public Action OnDead { get; set; }

    public bool isDead {
        get { return _isDead; }
    }

    [SerializeField]
    private bool _isOnDebugMode;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _walkSound;

    [SerializeField]
    private AudioClip _hitSound;

    [SerializeField]
    private AudioClip _jumpSound;

    // Use this for initialization
    void Start () {
        _myTransform = transform;

        GameUIManager.Instance.LivesCount = (int)_healthPoints;
        GameUIManager.Instance.EnemiesKilledCount = 0;

	    OnDead = LevelManager.Instance.OnMainCharacterKilled;
        LevelManager.Instance.AddActor(_myTransform);

        InitAudioSource();
	    InitMovementController();
	}

    private void InitAudioSource() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void InitMovementController() {
        _movementController = GetComponent<MovementController>();
        _movementController.AttackAction = Attack;
        _movementController.StartMovingAction = PlayWalkingSound;
        _movementController.StopMovingAction = StopWalkingSound;
        _movementController.JumpAction = PlayJumpSound;
    }

     // Update is called once per frame
	void Update () {
	    if (_isOnDebugMode)
	        DebugAttackRange();
	}

    private void DebugAttackRange() {
        DebugCircleAttackRange();
        DebugDistanceToEnemies();
    }

    private void DebugCircleAttackRange() {
        ArrayList circlePoints = GetCirclePoints();
        int m = circlePoints.Count;
        Color debugColor = Color.blue;
        for (int i = 0; i < m; i++) {
            int index2 = i+1;
            if (index2 >= (m-1))
                index2 = 0;
            Vector3 point1 = (Vector3)circlePoints[i];
            Vector3 point2 = (Vector3)circlePoints[index2];
            Debug.DrawLine(point1, point2, debugColor);
        }
    }

    private void DebugDistanceToEnemies() {
        ArrayList enemiesPositions = GetEnemiesPositions();
        foreach (Vector3 enemiePosition in enemiesPositions) {
            Color debugColor = Color.red;
            if (Vector3.Distance(enemiePosition, _myTransform.position) < _attackRange)
                debugColor = Color.green;
            Debug.DrawLine(_myTransform.position, enemiePosition, debugColor);
        }
    }

    private ArrayList GetCirclePoints() {
        ArrayList circlePositions = new ArrayList();
        int points = 36;
        for (int i = 0; i < points; i++) {
            float angles = (360 / points) * i;
            float x = _myTransform.position.x + _attackRange * Mathf.Cos(angles * Mathf.Deg2Rad);
            float y = _myTransform.position.y + _attackRange * Mathf.Sin(angles * Mathf.Deg2Rad);
            circlePositions.Add(new Vector3(x,y));
        }
        return circlePositions;
    }

    private ArrayList GetEnemiesPositions() {
        ArrayList enemiesPositions = new ArrayList();
        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();
        foreach (BaseEnemy enemy in enemies) 
            enemiesPositions.Add(enemy.transform.position);
        return enemiesPositions;
    }

    private void PlayWalkingSound() {
        if (!_audioSource.isPlaying ||
            _audioSource.clip != _walkSound) {
            PlayClip(_walkSound, true);
        }
    }

    private void StopWalkingSound() {
        if (_audioSource.isPlaying &&
            _audioSource.clip == _walkSound) {
            _audioSource.Stop();
        }
        
    }

    private void PlayJumpSound() {
        PlayClip(_jumpSound, false);
    }

    private void PlayGetHitSound() {
        PlayClip(_hitSound, false);
    }

    private void PlayClip(AudioClip audioClip, bool loop) {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.loop = loop;
        _audioSource.Play();
    }

    private void Attack() {
        
        BaseEnemy enemy = GetNearestEnemy();
        GetComponent<Animator>().SetTrigger("attack");
        if (enemy != null &&
            Vector3.Distance(enemy.transform.position, _myTransform.position) < _attackRange) {
            enemy.Hit(_attackDamage);
            Vector3 direction = enemy.transform.position - _myTransform.position;
            if (direction.x < 0) {
                Vector3 newScale = _myTransform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * -1;
                _myTransform.localScale = newScale;
            }
            else {
                Vector3 newScale = _myTransform.localScale;
                newScale.x = Mathf.Abs(newScale.x);
                _myTransform.localScale = newScale;
            }
        }
    }

    private BaseEnemy GetNearestEnemy() {
        BaseEnemy nearestEnemy = null;
        float currentMinDistance = 0;

        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();

        foreach (BaseEnemy enemy in enemies) {
            float enemyDistance = Vector3.Distance(enemy.transform.position, _myTransform.position);

            if (nearestEnemy == null) {
                nearestEnemy = enemy;
                currentMinDistance = enemyDistance;
            }
            else if (enemyDistance < currentMinDistance &&
                !enemy.isDead) {
                nearestEnemy = enemy;
                currentMinDistance = enemyDistance;
            }
        }
        
        return nearestEnemy;
    }

    public void Hit(float attackDamage) {
        _healthPoints -= attackDamage;
        GetComponent<Animator>().SetTrigger("getHit");
        PlayGetHitSound();
        if (_healthPoints <= 0) {
            _healthPoints = 0;
            Die();
        }

        GameUIManager.Instance.LivesCount = (int)_healthPoints;
    }

    private void Die() {
        if (!_isDead) {
            _isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            _movementController.enabled = false;
            if (OnDead != null)
                OnDead();
        }
        
    }
}
