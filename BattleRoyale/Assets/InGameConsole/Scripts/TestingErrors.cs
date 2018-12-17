using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingErrors : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.LogError("TestingErrors -- Update: Error");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.LogWarning("TestingErrors -- Update: Warning");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("TestingErrors -- Update: Log");
        }
    }
}
