using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCheckerLITE : MonoBehaviour {

	public bool triggered;
	public bool triggeredInBag;
	public Transform bagTrig = null;
	public Image img;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerStay (Collider other) {
		if (other.transform.tag != "InventoryGrid") {
			triggered = true;
		} else {
			triggeredInBag = true;
			if (bagTrig == null) {
				bagTrig = other.transform;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.transform.tag != "InventoryGrid") {
			triggered = false;
		} else {
			triggeredInBag = false;
			bagTrig = null;
		}
	}
}
