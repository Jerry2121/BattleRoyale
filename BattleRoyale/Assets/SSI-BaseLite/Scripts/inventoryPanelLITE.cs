using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryPanelLITE : MonoBehaviour {

	public Sprite itemTexture;
	public bool isEquiped = false;
	[TextArea(3, 10)]
	public string itemDescription;
	public Image img;
	public Text itemName;
	public Text itemDesc;

	// Use this for initialization
	void Start () {
		GetComponent<RectTransform> ().localScale = new Vector2 (0, 0);
	}

	public void SelectItem(itemDragLITE item){
		img.transform.localScale = new Vector2 (0.5f, 0.5f / (item.obj.width / item.obj.height));
		itemTexture = item.obj.itemTexture;
		itemDescription = item.obj.itemDescription;
		img.sprite = itemTexture;
		itemName.text = item.obj.itemName;
		itemDesc.text = item.obj.itemDescription;
	}
}
