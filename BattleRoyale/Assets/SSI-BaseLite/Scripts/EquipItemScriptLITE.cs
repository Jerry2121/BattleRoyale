using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemScriptLITE : MonoBehaviour {

	public itemDragLITE item;
	// Use this for initialization
	void Start () {
		
	}

	public void ItemToEquip (itemDragLITE item2) {
		item = item2;
	}

	// Update is called once per frame
	public void Equip () {
		GameObject.FindGameObjectWithTag ("MainCamera").SendMessage ("Equip", item.obj.GetComponent<itemScriptLITE>());
		transform.parent.GetComponent<RectTransform> ().localScale = new Vector2 (0, 0);
	}
}
