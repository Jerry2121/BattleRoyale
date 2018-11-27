using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCircle : MonoBehaviour {
    public GameObject Zone;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ZoneWall")
        {
            Zone.GetComponent<ChangeCircle>().OutsideOfCircle = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ZoneWall")
        {
            Zone.GetComponent<ChangeCircle>().OutsideOfCircle = true;
        }
    }
}
