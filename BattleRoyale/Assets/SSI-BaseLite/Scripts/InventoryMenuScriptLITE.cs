using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the only purpose of this script is to lock/unlock the mouse cursor and disable player movement if you're using the RigidbodyFPSController (such as in the sample scene)

public class InventoryMenuScriptLITE : MonoBehaviour {

	private bool inInventory = false;
	public Transform playerObj;
	public KeyCode inventoryKey;

	void Start (){
		inventoryKey = GetComponent<InventoryScriptLITE> ().inventoryKey;
		if (playerObj == null) {
			playerObj = transform.parent.parent; //this is assuming the Inventory object is in the MainCamera object
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (inventoryKey)) {
			inInventory = !inInventory;
			if (inInventory) {
				playerObj.GetComponent<MonoBehaviour> ().enabled = false;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			} else {
				playerObj.GetComponent<MonoBehaviour> ().enabled = true;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}
}

