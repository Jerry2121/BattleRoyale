using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemScriptLITE : MonoBehaviour {

		
	public InventoryGridScriptLITE grid;
	public itemDragLITE item;

	// Use this for initialization
	public void ItemToDrop (itemDragLITE item2) {
		item = item2;
		grid = item.transform.parent.GetComponent<InventoryGridScriptLITE>();
	}
	
	// Update is called once per frame
	public void Drop () {
		item.originalPos = item.rect.anchoredPosition;
		GameObject.FindGameObjectWithTag ("MainCamera").SendMessage ("RemoveItem", item.GetComponent<itemDragLITE>()); //in case you're making equipable items
		grid.SendMessage ("RemoveItem", item);
		transform.parent.GetComponent<RectTransform> ().localScale = new Vector2 (0, 0);
	}
}
