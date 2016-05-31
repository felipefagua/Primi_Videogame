using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SpawnPool : MonoBehaviour {

    [SerializeField]
    private LevelManager _levelManager;

    [SerializeField]
    private float _firstSpawnTime;

    [SerializeField] 
    private float _spawnTime;

    [SerializeField]
    private GameObject _prototype;

    [SerializeField] 
    private Transform[] _pointsToSpawn;

    [SerializeField]
    private float _minDistanceToSpawn = -1;

    [SerializeField]
    private ArrayList _createdObjects = new ArrayList();


    // Use this for initialization
	void Start () {
        InitSpawnPoints();
        InvokeRepeating("SpawnObject", _firstSpawnTime, _spawnTime);
	}

    private void InitSpawnPoints() {
        Transform[] _pointsToSpawn2 = GetComponentsInChildren<Transform>();
        _pointsToSpawn = new Transform[_pointsToSpawn2.Length - 1];
        Array.Copy(_pointsToSpawn2, 1, _pointsToSpawn, 0, _pointsToSpawn.Length);
    }

    private void SpawnObject() {
        if (!SomeOneIsNear()) {
            GameObject clone = Instantiate(_prototype);
            int randomIndex = Mathf.RoundToInt(Random.value*(_pointsToSpawn.Length - 1));
            clone.transform.position = _pointsToSpawn[randomIndex].position;
            _createdObjects.Add(clone.transform);
        }
    }

    private bool SomeOneIsNear() {
        for (int i = 0; i < _createdObjects.Count; i++) {
            if (_createdObjects[i] != null) {
                Transform objectTransform = (Transform) _createdObjects[i];
                foreach (Transform spawnTransform in _pointsToSpawn) {
                    if (objectTransform != null) {
                        float distance = Vector3.Distance(objectTransform.position, spawnTransform.position);
                        if (distance < _minDistanceToSpawn)
                            return true;
                    }
                }    
            }
            
        }

        return false;
    }
}
