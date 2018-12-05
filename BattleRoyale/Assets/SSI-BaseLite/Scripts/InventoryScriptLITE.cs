using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class InventoryScriptLITE : MonoBehaviour {

    //this manages all of the inventory based interactions (ie equipping guns and picking up items)
	public KeyCode inventoryKey = KeyCode.Tab;
	public KeyCode useKey = KeyCode.E;

	public float rayLength = 2;
	private Transform lookingAt;
	[Tooltip("The layermask used when it comes to raycasting for items.")]
	public LayerMask layerMask;
	private float timer;

	[Tooltip("This is for when the player has more than one inventory grid.")]
	public InventoryGridScriptLITE bag;
	private itemScriptLITE[] items;
	private bool doesFit = false; //this is used when more than one bag is used

	//this is all the ui stuff
	[Tooltip("Don't change this to True or else the code may break.")]
	public bool inventory = false; //this is the check to see if the player is in the inventory, it's public so that other scripts can access it, don't change this
	public RectTransform inventoryPanel;
    public GameObject inventoryTab;
	public float canvasMoveSpeed = 1;

	[Range(0.25f, 1)]
	public float uiScale = 2.0f; //the scale of the grids, default = 0.5f
	private float scaleMultiplier; //the scale compared to the default value (ie uiScale of 0.5f will have a multiplier of 1)

    public Transform player;
	public GameObject itemPrompt;

	void Start(){
		scaleMultiplier = uiScale / 0.5f;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		RectTransform parentRect;
		parentRect = bag.transform.parent.GetComponent<RectTransform> ();

		/*bag.GetComponent<RectTransform> ().localScale = new Vector3 (uiScale, uiScale, uiScale);
		if (bag.GetComponent<RectTransform>().rect.height * bag.GetComponent<RectTransform>().localScale.y > parentRect.rect.height) {
			parentRect.offsetMin = new Vector2 (parentRect.offsetMin.x, 0 - ((bag.height * 25 * scaleMultiplier) - parentRect.rect.height));
			bag.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
		} else {
			parentRect.offsetMin = new Vector2 (0, 0);
			bag.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
		}
		if (bag.GetComponent<RectTransform> ().rect.width > parentRect.rect.width) {
			parentRect.offsetMin = new Vector2 (0 - ((bag.width * 25 * scaleMultiplier) - parentRect.rect.width), parentRect.offsetMin.y);
		}
		parentRect.parent.GetComponent<ScrollRect> ().horizontalNormalizedPosition = 0;*/
	}

	void Update(){
		RaycastHit hit;
        Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);

        // add a GameObject inventoryTab variable? And have it ask if active == true? 

        // Change the below code to:
        // If (GameObject.Find("InventoryButtonSelected")) { inventory = true; inventoryPanel.anchorMax = new Vector2 (1, 1); inventoryPanel.anchorMin = new Vector2 (0, 0); }
        // else { inventory = false; inventoryPanel.anchorMax = new Vector2 (1, 0); inventoryPanel.anchorMin = new Vector2 (0, -1); }

        // How can I force one canvas to always appear in front of another?

        /*if (Input.GetKeyDown(inventoryKey)) {
			if (!inventoryTab.activeInHierarchy) {
                inventory = true;
				inventoryPanel.anchorMax = new Vector2 (1, 1);
				inventoryPanel.anchorMin = new Vector2 (0, 0);
			} else {
                inventory = false;
                inventoryPanel.anchorMax = new Vector2 (1, 0);
                inventoryPanel.anchorMin = new Vector2 (0, -1);
			}
		}*/

        if (Physics.Raycast (player.position, fwd, out hit, rayLength, layerMask) && !PauseMenu.isOn) {
            Debug.Log(hit.collider.gameObject.name);
			itemPrompt.SetActive (true);
			if (hit.transform != lookingAt) {
				if (lookingAt != null) {
					lookingAt.SendMessage ("LookAway", SendMessageOptions.DontRequireReceiver);
					lookingAt = null;
				}
				hit.transform.SendMessage ("LookAt", SendMessageOptions.DontRequireReceiver);
				lookingAt = hit.transform;
			}
			if (Input.GetKeyUp (useKey)) {
				doesFit = false;
				itemScriptLITE temp = hit.transform.GetComponent<itemScriptLITE> ();

                if (temp == null)
                    return;

                //foreach (InventoryGridScript i in bags) {
                Debug.Log(bag.name);
                Debug.Log(temp.name);
				if (bag.freeSpaces >= temp.width * temp.height) {
                    bag.GiveItem(temp);//bag.SendMessage ("GiveItem", temp);
				} 
				if (doesFit == false) {
					StartCoroutine ("NotEnough");
				}
				//}
				//hit.transform.SendMessage ("Interacted", transform, SendMessageOptions.DontRequireReceiver);
				//hit.transform.SendMessage ("Execute", SendMessageOptions.DontRequireReceiver);
			}
			if (Input.GetKey (useKey)) {
				timer += Time.deltaTime;
				if (timer >= 0.4) {
					timer = 0;
					hit.transform.SendMessage ("Interacted", transform, SendMessageOptions.DontRequireReceiver);
					hit.transform.SendMessage ("Hold", transform, SendMessageOptions.DontRequireReceiver);
				}
			}
			if (!Input.GetKey (useKey)) {
				timer = 0;
			}
		} else if (lookingAt != null) {
			itemPrompt.SetActive (false);
			itemPrompt.GetComponent<Text> ().text = "E to Take Item";
			itemPrompt.GetComponent<Text> ().color = Color.white;
			StopCoroutine("NotEnough");
			lookingAt.SendMessage ("LookAway", SendMessageOptions.DontRequireReceiver);
			lookingAt = null;
		}
	}

	IEnumerator NotEnough(){
		itemPrompt.GetComponent<Text> ().text = "Not Enough Space";
		itemPrompt.GetComponent<Text> ().color = Color.red;
		yield return new WaitForSeconds (1);
		itemPrompt.GetComponent<Text> ().text = "E to Take Item";
		itemPrompt.GetComponent<Text> ().color = Color.white;
	}

	void Equip (itemScriptLITE item){
	}

	void GiveItem(itemScriptLITE item){
		doesFit = false;
		itemScriptLITE temp = item;
		//foreach (InventoryGridScript i in bags) {
		if (bag.freeSpaces >= temp.width * temp.height) {
			bag.SendMessage ("GiveItem", temp);
		} 
		else {
			StartCoroutine ("NotEnough");
		}
	}

	void UnloadItem(itemScriptLITE item){ //used for example, when reloading and there isnt enough space
		doesFit = false;
		itemScriptLITE temp = item;
		//foreach (InventoryGridScript i in bags) {
		if (bag.freeSpaces >= temp.width * temp.height) {
			bag.SendMessage ("GiveItem", temp);
		} 
		if (doesFit == false) {
			item.obj.gameObject.SetActive (true);
			item.obj.transform.position = transform.position;
			item.transform.eulerAngles = new Vector3 (0, 0, 0);
			item.obj.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 2);
			item.obj.transform.SetParent(null);
		}
	}

	public void DoesFit(){
		doesFit = true;
	}
}
