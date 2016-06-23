using UnityEngine;
using System.Collections;
using System;

public class BaseEnemy : MonoBehaviour {
    [SerializeField] 
    private Transform _myTransform;

    [SerializeField]
    private LevelManager _levelManager;

    [SerializeField]
    private float _healthPoints;

    [SerializeField]
    private float _attackDamage;
    
    [SerializeField]
    private float _attackCoolDown;

    [SerializeField]
    private float _dieTime;

    public float dieTime {
        get { return _dieTime; }
    }

    [SerializeField]
    private bool _isDead;

    public bool isDead {
        get { return _isDead; }
    }

    [SerializeField]
    private MainCharacter _mainCharacter;

    private SeekNFlee _movementAIComponent;

    [SerializeField]
    private bool _isOnDebugMode;
    
    public Action OnDead { get; set; }

    public float AttackRange {
        get { return GetComponent<SeekNFlee>().MinDistanceToTarget; }
    }

    // Use this for initialization
    private void Awake() {
        _myTransform = transform;
        InitLevelManager();
        InitMainCharacter();
        InitAiComponent();
    }

    private void InitLevelManager() {
        _levelManager = FindObjectOfType<LevelManager>();
        if (_levelManager != null) {
            OnDead = _levelManager.OnEnemyKilled;
            _levelManager.AddActor(_myTransform);
        }
            
    }

    private void InitMainCharacter() {
        _mainCharacter = FindObjectOfType<MainCharacter>();
    }

    private void InitAiComponent() {
        _movementAIComponent = GetComponent<SeekNFlee>();
        _movementAIComponent.Target = _mainCharacter.gameObject;
        _movementAIComponent.OnNearestDistanceReached = OnReachPlayer;
    }

    internal void Hit(float attackDamage) {
        if (!_isDead) {
            _healthPoints -= attackDamage;
            GetComponent<Animator>().SetTrigger("getHit");
            GetComponent<AudioSource>().Play();
            if (_healthPoints <= 0)
            {
                _healthPoints = 0;
                Die();
            }
        }
    }

    private void Die() {
        if (!_isDead) {
            _isDead = true;
            _movementAIComponent.enabled = false;
            
            if (_levelManager != null)
                _levelManager.RemoveActor(gameObject.transform);

            if (OnDead != null)
                OnDead();
            GetComponent<Animator>().SetBool("isDead", true);
            Destroy(gameObject, _dieTime);
        }
        
    }


    // Update is called once per frame
	private void Update () {
	    if (_isOnDebugMode)
            DebugCircleAttackRange();
	}

    private void DebugCircleAttackRange() {
        ArrayList circlePoints = GetCirclePoints();
        int m = circlePoints.Count;
        Color debugColor = Color.yellow;
        for (int i = 0; i < m; i++)
        {
            int index2 = i + 1;
            if (index2 >= (m - 1))
                index2 = 0;
            Vector3 point1 = (Vector3)circlePoints[i];
            Vector3 point2 = (Vector3)circlePoints[index2];
            Debug.DrawLine(point1, point2, debugColor);
        }
    }

    private ArrayList GetCirclePoints() {
        ArrayList circlePositions = new ArrayList();
        int points = 36;
        for (int i = 0; i < points; i++)
        {
            float angles = (360 / points) * i;
            float x = _myTransform.position.x + AttackRange * Mathf.Cos(angles * Mathf.Deg2Rad);
            float y = _myTransform.position.y + AttackRange * Mathf.Sin(angles * Mathf.Deg2Rad);
            circlePositions.Add(new Vector3(x, y));
        }
        return circlePositions;
    }

    private void OnReachPlayer() {
        if (!isDead) {
            _movementAIComponent.enabled = false;
            Attack();
            Invoke("OnAttackCoolDownEnd", _attackCoolDown);
        }
    }

    private void Attack() {
        _mainCharacter.Hit(_attackDamage);
        GetComponent<Animator>().SetTrigger("attack");
    }

    private void OnAttackCoolDownEnd() {
        _movementAIComponent.IsNearToTarget = false;
        _movementAIComponent.enabled = true;

    }
}
