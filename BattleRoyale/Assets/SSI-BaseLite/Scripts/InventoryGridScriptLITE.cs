using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridScriptLITE : MonoBehaviour {

	public Transform player;


	public int width;
	public int height;

	public int freeSpaces;

	[Tooltip("The inventory item prefab used when new items are added")]
	public RectTransform invObjPref;

	private Image grid;

	[System.Serializable]
	public class Item
	{
		public Vector2 scale;
		public Sprite image;
	}

	public bool[] slots;
	public List<itemDragLITE> items;

    [SerializeField]
    InventoryScriptLITE inventoryScriptLITE;

	void Start () {
        player = GetComponentInParent<PlayerUI>().player.transform;
		slots = new bool[width * height];
		freeSpaces = width * height;
		grid = transform.Find ("gridimage").GetComponent<Image>();
        grid.transform.position = new Vector2(500, -275 * height);
        grid.rectTransform.sizeDelta = new Vector2 (50 * width, 50 * height);
        grid.rectTransform.anchoredPosition = new Vector2(25 * width, 0);
		GetComponent<RectTransform>().sizeDelta = new Vector2 (50 * width, 50 * height);
		GetComponent<BoxCollider>().size = new Vector3(grid.rectTransform.sizeDelta.x, grid.rectTransform.sizeDelta.y, 0.05f);
		GetComponent<BoxCollider> ().center = new Vector2 (grid.rectTransform.sizeDelta.x / 2, -(grid.rectTransform.sizeDelta.y / 2));
        // transform.position = new Vector2(34, -797);
        //transform.Translate(new Vector3(34, -797));
    }

	// Use this for initialization
	void Update () {
	}
	
	// Update is called once per frame
	void GiveItem (itemScriptLITE item) {
        Debug.Log("Foo1");
		bool fits = false;
		int topleftslot = 0;
		RectTransform itemClone = Instantiate (invObjPref);
		itemClone.GetComponent<itemDragLITE> ().obj = item;
		itemClone.GetComponent<itemDragLITE> ().panel = transform.parent.parent.parent.Find ("Item Panel").gameObject;
		itemClone.SetParent(transform);
		itemClone.GetComponent<TriggerCheckerLITE> ().img.sprite = item.itemTexture;
		itemClone.gameObject.SetActive (true);
		itemClone.localScale = new Vector2 (item.width, item.height);
		itemClone.localPosition = new Vector3 (0, 0, 0);
		itemClone.localEulerAngles = new Vector3 (0, 0, 0);
		itemClone.anchoredPosition = new Vector2 (0, 0);
		for (int i = 0; i < height - (item.height - 1); i++) {
			if (fits == true) {
				break;
			}
			for (int j = 0; j < width - (item.width - 1); j++) {
				
				if (slots [j + (i * width)] == false) {
					for (int y = 0; y < item.height; y++) {
						for (int x = 0; x < item.width; x++) {
							int slot = (j + x + (y * width) + (i * width));
							if (slot < slots.Length) {
								if (slots [slot] == false) {
									fits = true;
								} else {
									fits = false;
									break;
								}
							}
						}
						if (fits == false) {
							break;
						}
					}
				}
				if (fits == true) {
					topleftslot = j + (i * width);
					break;
				}
			}

		}

		
		if (fits == true) {
            //player.SendMessage ("DoesFit");
            inventoryScriptLITE.DoesFit();
			items.Add (itemClone.GetComponent<itemDragLITE>());

			itemClone.anchoredPosition = new Vector2 (topleftslot % width * 50, -Mathf.Floor (topleftslot / width) * 50);
			itemClone.GetComponent<itemDragLITE> ().originalPos = itemClone.anchoredPosition;
			itemClone.GetComponent<itemDragLITE> ().originalScale = itemClone.localScale;
			for (int y = 0; y < item.height; y++) {
				for (int x = 0; x < item.width; x++) {
					slots[topleftslot + x + (y * width)] = true;
				}
			}
			item.transform.SetParent(transform);
			item.gameObject.SetActive (false);
			freeSpaces -= (int)(item.width * item.height);
		}
		else {
			Destroy (itemClone.gameObject);
		}
	}

	void SortSlots(itemDragLITE pos){
		float slotsWidth = Mathf.Round(pos.originalScale.x);
		float slotsHeight = Mathf.Round(pos.originalScale.y);
		float slotPosWidth = Mathf.Round(pos.originalPos.x / 50);
		float slotPosHeight = -Mathf.Round(pos.originalPos.y / 50);
		if (slotsWidth <= width && slotsHeight <= height) {

			for (int i = 0; i < slotsWidth; i++) {
				for (int j = 0; j < slotsHeight; j++) {
					slots [(int)slotPosWidth + i + (j * width) + (int)(slotPosHeight * width)] = false;
				}
			}

			bool fits = true;
			float slotPosWidth2 = Mathf.Round (pos.rect.anchoredPosition.x / 50); //the item's new position
			float slotPosHeight2 = -Mathf.Round (pos.rect.anchoredPosition.y / 50);
			float slotsWidth2 = Mathf.Round(pos.rect.localScale.x);
			float slotsHeight2 = Mathf.Round(pos.rect.localScale.y);
			for (int i = 0; i < slotsWidth2; i++) {
				for (int j = 0; j < slotsHeight2; j++) {

					if (slots [(int)slotPosWidth2 + i + (j * width) + (int)(slotPosHeight2 * width)] == true) {
						fits = false;
						for (int x = 0; x < slotsWidth; x++) {
							for (int y = 0; y < slotsHeight; y++) {
								slots [(int)slotPosWidth + x + (y * width) + (int)(slotPosHeight * width)] = true;
							}
						}
						pos.SendMessage ("ReturnToNormal");
						break;
					}

				}
			}
			if (fits) {
				for (int i = 0; i < slotsWidth2; i++) {
					for (int j = 0; j < slotsHeight2; j++) {
						slots [(int)slotPosWidth2 + i + (j * width) + (int)(slotPosHeight2 * width)] = true;
					}
				}
			}
		} else {
			pos.SendMessage ("ReturnToNormal");
		}
	}

	void TransferItemAway(itemDragLITE pos){
		float slotPosWidth = Mathf.Round(pos.originalPos.x / 50);
		float slotPosHeight = -Mathf.Round(pos.originalPos.y / 50);
		float slotsWidth = Mathf.Round(pos.originalScale.x);
		float slotsHeight = Mathf.Round(pos.originalScale.y);
		items.Remove (pos.GetComponent<itemDragLITE>());
		for (int i = 0; i < slotsWidth; i++) {
			for (int j = 0; j < slotsHeight; j++) {
				slots [(int)slotPosWidth + i + (j * width) + (int)(slotPosHeight * width)] = false;
			}
		}
	}

	void TransferItemTo(itemDragLITE pos){
		float slotPosWidth = Mathf.Round(pos.rect.anchoredPosition.x / 50);
		float slotPosHeight = -Mathf.Round(pos.rect.anchoredPosition.y / 50);
		float slotsWidth = Mathf.Round(pos.rect.localScale.x);
		float slotsHeight = Mathf.Round(pos.rect.localScale.y);
		items.Add (pos.GetComponent<itemDragLITE>());
		for (int i = 0; i < slotsWidth; i++) {
			for (int j = 0; j < slotsHeight; j++) {
				slots [(int)slotPosWidth + i + (j * width) + (int)(slotPosHeight * width)] = true;
			}
		}
	}

	void RemoveItem(itemDragLITE pos){
		items.Remove (pos.GetComponent<itemDragLITE> ());

		float slotsWidth = pos.obj.width;
		float slotsHeight = pos.obj.height;
		float slotPosWidth = Mathf.Round(pos.originalPos.x / 50);
		float slotPosHeight = -Mathf.Round(pos.originalPos.y / 50);


		pos.obj.gameObject.SetActive (true);
		pos.obj.transform.position = player.position;
		pos.transform.eulerAngles = new Vector3 (0, 0, 0);
		pos.obj.GetComponent<Rigidbody>().velocity = player.TransformDirection(Vector3.forward * 2);
		pos.obj.transform.SetParent(null);



		for (int i = 0; i < slotsWidth; i++) {
			for (int j = 0; j < slotsHeight; j++) {
				slots [(int)slotPosWidth + i + (j * width) + (int)(slotPosHeight * width)] = false;
			}
		}
		freeSpaces += (int)(pos.obj.width * pos.obj.height);
		Destroy (pos.gameObject);
	}

	void DestroyItem(itemDragLITE pos){
		
	}

	void RemoveAllItems(){
		foreach (itemDragLITE item in items) {
			if (item.obj.equipable) {
				GameObject.FindGameObjectWithTag ("MainCamera").SendMessage ("RemoveItem", item.GetComponent<itemDragLITE>());
			}
			item.obj.gameObject.SetActive (true);
			item.obj.transform.position = player.position;
			item.obj.transform.eulerAngles = new Vector3 (0, 0, 0);
			item.obj.GetComponent<Rigidbody>().velocity = player.TransformDirection(Vector3.forward * 2);
			item.obj.transform.SetParent(null);

			float slotsWidth = Mathf.Round(item.GetComponent<itemDragLITE>().rect.localScale.x);
			float slotsHeight = Mathf.Round(item.GetComponent<itemDragLITE>().rect.localScale.y);
			float slotPosWidth = Mathf.Round(item.GetComponent<itemDragLITE>().originalPos.x / 50);
			float slotPosHeight = -Mathf.Round(item.GetComponent<itemDragLITE>().originalPos.y / 50);

			for (int i = 0; i < slotsWidth; i++) {
				for (int j = 0; j < slotsHeight; j++) {
					slots [(int)slotPosWidth + i + (j * width) + (int)(slotPosHeight * width)] = false;
				}
			}
			freeSpaces += (int)(item.obj.width * item.obj.height);
			Destroy (item.gameObject);
		}
		for(int i = 0; i < slots.Length; i++){
			slots[i] = false;
		}
		items.Clear();
	}
}
