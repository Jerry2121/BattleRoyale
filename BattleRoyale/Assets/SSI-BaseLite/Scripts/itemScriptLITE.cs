using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemScriptLITE : MonoBehaviour {
	public string itemName;
	public Sprite itemTexture;
	public float width = 1;
	public float height = 1;
	public GameObject obj;
	public bool equipable = false;
	[TextArea(3, 10)]
	public string itemDescription;
	[Tooltip("Gun/Melee/Deployable/Other")]
	public string itemType;
	public Text ammoText;

	void Awake(){
		obj = gameObject;
	}
}
