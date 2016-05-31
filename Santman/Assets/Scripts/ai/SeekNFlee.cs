using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;

public class SeekNFlee : MonoBehaviour {

    public Action OnNearestDistanceReached { get; set; }

    [SerializeField]
    private GameObject _target;

    public GameObject Target {
        get { return _target; }
        set { _target = value; }
    }

    private Transform _targetTransform;

    private Transform _myTransform;

    [SerializeField]
    private float _collideDistance;
    
    [SerializeField]
    private float _minDistanceToTarget;

    public float MinDistanceToTarget {
        get { return _minDistanceToTarget; }
    }

    [SerializeField]
    private float _speed;

    [SerializeField]
    private bool _isNearToTarget;

    public bool IsNearToTarget {
        get { return _isNearToTarget; }
        set { _isNearToTarget = value; }
    }

    // Use this for initialization
	void Start () {
        _isNearToTarget = false;
	    _myTransform = transform;
	    InitTarget();
	}

    private void InitTarget() {
        _targetTransform = _target.GetComponent<Transform>();
    }

    // Update is called once per frame
	void Update () {
        CheckPositionEvents();
        if(enabled)
	        Seek();
	}

    private void Seek() {
        Vector3 direction = (_targetTransform.position - _myTransform.position).normalized;
        Vector3 step = direction * _speed * Time.deltaTime;
        Vector3 candidatePosition = _myTransform.position + step;

        RaycastHit2D[] hits = Physics2D.RaycastAll(_myTransform.position, step, _collideDistance);

        if (hits.Length == 1) {
            GetComponent<Animator>().SetBool("isWalking", true);
            _myTransform.position = candidatePosition;
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
        else {
            GetComponent<Animator>().SetBool("isWalking", false);
        }

    }

    private void CheckPositionEvents() {
        if (!_isNearToTarget &&
            Vector3.Distance(_targetTransform.position, _myTransform.position) <= _minDistanceToTarget) {
            _isNearToTarget = true;
            if (OnNearestDistanceReached != null)
                OnNearestDistanceReached();
        }
        
        if (Vector3.Distance(_targetTransform.position, _myTransform.position) > _minDistanceToTarget) {
            _isNearToTarget = false;
        }
    }
}