using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDropSystem : MonoBehaviour {
    [SerializeField]
    Slider InputSlider;
    [SerializeField]
    TMP_InputField InputFieldText;
    private float tempNum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void InputNumChanged()
    {
        InputFieldText.text = "" + InputSlider.value;
    }
    public void InputValChanged()
    {
        tempNum = InputSlider.value;
        InputFieldText.text = "" + tempNum;
    }
}
