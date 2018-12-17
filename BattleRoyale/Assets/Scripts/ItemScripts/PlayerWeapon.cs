using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

    public string name = "Pistol";
    public int damage = 10;
    public float range = 100f;
    [Tooltip("Shots per Second")]
    public float fireRate = 0; //If 0 it is a single-fire weapon, e.g. press key for every shot insteat of holding down

    public float reloadTime = 1f;

    public int maxAmmo = 20;
    [HideInInspector]
    public int currentAmmo;

    public GameObject graphics;
    public Sprite sprite;

    public string AmmoType = "defaultAmmo";

    public PlayerWeapon()
    {
        currentAmmo = maxAmmo;
    }

}
