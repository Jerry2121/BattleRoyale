﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {
    private int selectedSlot;
    public GameObject Slot1Outline;
    public GameObject Slot2Outline;
    public GameObject WeaponCanvas;
    private float showtimer;
    private bool ShowUI;
    public float WeaponUIDisappearTime;
    public Animator animator;
    private void Start()
    {
        selectedSlot = 1;
        showtimer = 0;
    }
    private void Update()
    {
        showtimer -= Time.deltaTime;
        if (!ShowUI)
        {
            WeaponCanvas.SetActive(false);
        }
        if (showtimer <= 0)
        {
            showtimer = 0;
        }
        if (showtimer == 0)
        {
            animator.ResetTrigger("Appear");
            animator.SetTrigger("Disappear");
            //ShowUI = false;
        }
        if (ShowUI)
        {
            WeaponCanvas.SetActive(true);
        }
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
            animator.ResetTrigger("Disappear");
            animator.SetTrigger("Appear");
            ShowUI = true;
            showtimer = WeaponUIDisappearTime;
            selectedSlot++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            animator.ResetTrigger("Disappear");
            animator.SetTrigger("Appear");
            ShowUI = true;
            showtimer = WeaponUIDisappearTime;
            selectedSlot--;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            animator.ResetTrigger("Disappear");
            animator.SetTrigger("Appear");
            ShowUI = true;
            showtimer = WeaponUIDisappearTime;
            selectedSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            animator.ResetTrigger("Disappear");
            animator.SetTrigger("Appear");
            ShowUI = true;
            showtimer = WeaponUIDisappearTime;
            selectedSlot = 2;
        }
    }
}
