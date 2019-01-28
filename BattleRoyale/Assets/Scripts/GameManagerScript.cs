using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    [Header("Items To Spawn (Prefabs)")]
    public bool DisableItemSpawning;
    [Header("Weapons")]
    public GameObject[] weapons;
    /*
    public GameObject Weapon1;
    public GameObject Weapon2;
    public GameObject Weapon3;
    public GameObject Weapon4;
    public GameObject Weapon5;
    public GameObject Weapon6;*/
    [Header("Ammo")]
    public GameObject lightAmmo;
    public GameObject mediumAmmo;
    public GameObject heavyAmmo;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
