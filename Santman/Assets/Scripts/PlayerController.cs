using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private XBox360Input _XBox360Input;

	// Use this for initialization
	void Start () {
	    InitXBox360Input();
	}

    private void InitXBox360Input() {
        _XBox360Input = new XBox360Input(1);
    }
	
	// Update is called once per frame
	void Update () {
	    CheckInput();
	}

    private void CheckInput() {
        
    }
}
