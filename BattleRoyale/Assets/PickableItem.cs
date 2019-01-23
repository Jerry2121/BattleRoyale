using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public GameObject obj;
    public bool equipable = false;
    public string itemType;
    public int amount;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        obj = gameObject;
    }
}
