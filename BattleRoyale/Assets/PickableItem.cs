using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickableItem : MonoBehaviour
{
    public GameObject obj;
    public bool equipable = false;
    public string itemType;
    public int amount;
    public TextMeshProUGUI AmountText;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AmountText.text = "" + amount;
        obj = gameObject;
    }
}
