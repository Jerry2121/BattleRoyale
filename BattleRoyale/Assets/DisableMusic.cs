using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            this.gameObject.GetComponent<AudioSource>().volume = 0;
        }
	}
}
