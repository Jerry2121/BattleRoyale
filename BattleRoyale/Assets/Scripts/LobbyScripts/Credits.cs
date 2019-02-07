using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {

    public float speed = 1f;

    public GameObject creditsCanvas;
    public Transform credits;

    private Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = credits.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
