using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

    public string name = "Pistol";
    public int damage = 10;
    public float range = 100f;

    public float fireRate = 0; //If 0 it is a single-fire weapon, e.g. press key for every shot insteat of holding down

    public GameObject graphics;



}
