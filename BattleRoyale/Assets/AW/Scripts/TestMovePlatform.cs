using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TestMovePlatform : MonoBehaviour {

    [SerializeField]
    Vector3 movement;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
	}
}
