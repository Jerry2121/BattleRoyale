using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingErrors : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Debug.LogError("TestingErrors -- Update: Error");
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            Debug.LogWarning("TestingErrors -- Update: Warning");
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            Debug.Log("TestingErrors -- Update: Log");
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            throw new System.Exception("TestingErrors -- Update: Exception");
        }
    }
}
