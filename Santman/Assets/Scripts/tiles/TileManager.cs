using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;

public class TileManager : MonoBehaviour {
    [SerializeField] 
    private ImageTile[] _tiles;

    [SerializeField]
    private Vector3 _tilesMovementDirection;

    [SerializeField]
    private float _tilesMovementSpeed;

    [SerializeField]
    private Vector3 _tilesOffset;

    [SerializeField]
    private float _tilesLeftLmimit;

    public Vector3 TilesMovementDirection {
        get { return _tilesMovementDirection; }
    }

    public float TilesMovementSpeed {
        get { return _tilesMovementSpeed; }
    }

    public Vector3 TilesOffset {
        get { return _tilesOffset; }
    }

    public float TilesLeftLmimit {
        get { return _tilesLeftLmimit; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    MoveTiles();
	}

    private void MoveTiles() {
        foreach (ImageTile tile in _tiles)
            tile.Move();

        foreach (ImageTile tile in _tiles)
            tile.GotoLastIfNeeded();
    }

    public ImageTile GetLastTile() {
        ImageTile lastTile = null;

        foreach (ImageTile tile in _tiles) {
            if (lastTile == null) 
                lastTile = tile;
            else {
                float tileDistance = Mathf.Abs(TilesLeftLmimit - tile.transform.position.x);
                float lastTileDistance = Mathf.Abs(TilesLeftLmimit - lastTile.transform.position.x);

                if (tileDistance > lastTileDistance)
                    lastTile = tile;
            }
        }

        return lastTile;
    }
}
