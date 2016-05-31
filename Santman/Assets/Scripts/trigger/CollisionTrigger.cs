using System;
using UnityEngine;
using System.Collections;

public class CollisionTrigger : MonoBehaviour {

    public Action OnCollision { get; set; }

    private void OnTriggerEnter2D(Collider2D other) {
        if (OnCollision != null)
            OnCollision();
    }
}
