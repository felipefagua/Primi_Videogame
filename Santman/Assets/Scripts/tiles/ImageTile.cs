using UnityEngine;
using System.Collections;

public class ImageTile : MonoBehaviour {

    [SerializeField]
    private TileManager _manager;

    [SerializeField]
    private Vector3 _movementDirection;

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private Vector3 _tileOffset;

    [SerializeField] 
    private float _leftLimit;

    private Transform _myTransform;

	// Use this for initialization
	void Start () {
	    _myTransform = transform;
	    InitDaataFromTileManager();
	}

    private void InitDaataFromTileManager() {
        _movementDirection = _manager.TilesMovementDirection;
        _movementSpeed = _manager.TilesMovementSpeed;
        _tileOffset = _manager.TilesOffset;
        _leftLimit = _manager.TilesLeftLmimit;
    }

    public void Move() {
        Vector3 step = _movementDirection.normalized *
                        _movementSpeed * 
                        Time.deltaTime;
        _myTransform.Translate(step);
    }

    public void GotoLastIfNeeded() {
        if (_myTransform.position.x < _leftLimit)
            GotoLastTile();
    }

    private void GotoLastTile() {
        ImageTile lastTile = _manager.GetLastTile();
        _myTransform.position = lastTile.transform.position + _tileOffset;
    }
}
