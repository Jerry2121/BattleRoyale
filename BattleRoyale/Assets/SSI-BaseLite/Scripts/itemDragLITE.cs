using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class itemDragLITE : MonoBehaviour
{

	public GameObject panel;

	public RectTransform rect;

	public Vector2 originalPos;
	public Vector2 originalScale;
	public Vector3 originalRot;

	private Transform oldParent;

	public itemScriptLITE obj;
	public GameObject equipButton;
	public GameObject dropButton;

	void Awake(){
		rect = GetComponent<RectTransform> ();
	}

	void Start(){
		panel = GameObject.FindGameObjectWithTag ("ItemPanel");
		oldParent = transform.parent;
		originalPos = rect.anchoredPosition;
		originalScale = rect.localScale;
		originalRot = transform.Find ("image").localEulerAngles;
		equipButton = panel.transform.Find ("Equip").gameObject;
		dropButton = panel.transform.Find ("Drop").gameObject;
		rect = GetComponent<RectTransform> ();
	}

	public void BeginDrag(){
		oldParent = transform.parent;
		originalPos = rect.anchoredPosition;
		originalScale = rect.localScale;
		originalRot = transform.Find ("image").localEulerAngles;
		transform.SetParent(transform.parent.parent);
		transform.SetAsLastSibling();
	}

	public void OnDrag(UnityEngine.EventSystems	.BaseEventData eventData)
	{
		var pointerData = eventData as UnityEngine.EventSystems.PointerEventData;
		pointerData.useDragThreshold = true;

		var currentPosition = rect.anchoredPosition;
		currentPosition.x += pointerData.delta.x;
		currentPosition.y += pointerData.delta.y;
		rect.anchoredPosition = currentPosition;
	}

	public void EndDrag(){
		transform.SetParent(oldParent);
		if (GetComponent<TriggerCheckerLITE> ().triggeredInBag) {
			oldParent = transform.parent;
			transform.SetParent(GetComponent<TriggerCheckerLITE> ().bagTrig);
		} else{
			if (oldParent != transform.parent) {
				oldParent.SendMessage ("RemoveItem", this);
			} else {
				transform.parent.SendMessage ("RemoveItem", this);
			}
			panel.GetComponent<RectTransform> ().localScale = new Vector2 (0, 0); //hides the panel without making it inactive so that future itemDrags don't crash
		}
		if (GetComponent<TriggerCheckerLITE> ().triggered == false && Mathf.Round (rect.anchoredPosition.x / 50) * 50 <= (50 * transform.parent.GetComponent<InventoryGridScriptLITE>().width - 1) - (50 * (transform.localScale.x - 1)) && Mathf.Round (rect.anchoredPosition.x / 50) * 50  >= 0 && Mathf.Round (rect.anchoredPosition.y / 50) * 50 <= 0 && Mathf.Round (rect.anchoredPosition.y / 50) * 50 >= -(50 * transform.parent.GetComponent<InventoryGridScriptLITE>().height - 1) + (50 * (transform.localScale.y - 1))) {
			rect.anchoredPosition = new Vector2 (Mathf.Round (GetComponent<RectTransform> ().anchoredPosition.x / 50) * 50, Mathf.Round (GetComponent<RectTransform> ().anchoredPosition.y / 50) * 50);
			if (oldParent != transform.parent) {
				oldParent.SendMessage ("TransferItemAway", this);
				transform.parent.SendMessage ("TransferItemTo", this);
			} else {
				transform.parent.SendMessage ("SortSlots", this);
				originalPos = rect.anchoredPosition;
				originalRot = rect.transform.Find ("image").localEulerAngles;
			}
		} else if (GetComponent<TriggerCheckerLITE> ().triggered == true || Mathf.Round (rect.anchoredPosition.x / 50) * 50 <= 50 * (transform.parent.GetComponent<InventoryGridScriptLITE>().width - 1)) {
			transform.SetParent(oldParent);
			rect.anchoredPosition = originalPos;
			rect.localScale = originalScale;
			transform.Find ("image").localEulerAngles = originalRot;
			GetComponent<TriggerCheckerLITE> ().triggered = false;
		}else{
			if (oldParent != transform.parent) {
				oldParent.SendMessage ("RemoveItem", this);
			} else {
				transform.parent.SendMessage ("RemoveItem", this);
			}
			panel.GetComponent<RectTransform> ().localScale = new Vector2 (0, 0);
		}
		CheckIfEquipable ();
	}

	void Drop () {
		transform.parent.SendMessage ("RemoveItem", this);
	}

	void ReturnToNormal(){
		transform.SetParent(oldParent);
		rect.anchoredPosition = originalPos;
		rect.localScale = originalScale;
		transform.Find ("image").localEulerAngles = originalRot;
		GetComponent<TriggerCheckerLITE> ().triggered = false;
	}

	public void CheckIfEquipable(){
		panel.GetComponent<RectTransform> ().localScale = new Vector2 (1, 1);
		panel.GetComponent<inventoryPanelLITE> ().SelectItem (this);
		dropButton.SendMessage ("ItemToDrop", transform.GetComponent<itemDragLITE> ());
		if (obj.equipable) {
			equipButton.SetActive (true);
			equipButton.SendMessage ("ItemToEquip", transform.GetComponent<itemDragLITE> ());
		} else {
			equipButton.SetActive (false);
		}
	}
}﻿
