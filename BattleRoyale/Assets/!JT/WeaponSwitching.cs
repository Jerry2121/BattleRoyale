using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {
    private int selectedSlot;
    public GameObject Slot1Outline;
    public GameObject Slot2Outline;
    private void Start()
    {
        selectedSlot = 1;
    }
    private void Update()
    {
        if (selectedSlot == 1)
        {
            Slot1Outline.SetActive(true);
            Slot2Outline.SetActive(false);
        }
        if (selectedSlot == 2)
        {
            Slot1Outline.SetActive(false);
            Slot2Outline.SetActive(true);
        }
        if (selectedSlot < 1)
        {
            selectedSlot = 2;
        }
        if (selectedSlot > 2)
        {
            selectedSlot = 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.V))
        {
            selectedSlot++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedSlot--;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            selectedSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            selectedSlot = 2;
        }
    }
}
