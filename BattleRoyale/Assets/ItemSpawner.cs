using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    [Header("Spawner Type")]
    public bool WeaponSpawner;
    public bool AmmoSpawner;
    [Header("AmmoSpawners")]
    public GameObject AmmoSpawwner1;
    public GameObject AmmoSpawwner2;
    [Header("Weapon Bools")]
    public bool canspawnItem;
    public bool ran;
    public bool Weapon1;
    public bool Weapon2;
    public bool Weapon3;
    public bool Weapon4;
    public bool Weapon5;
    public bool Weapon6;
    private int d1;
	// Use this for initialization
	void Start () {
        canspawnItem = true;
        ran = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (canspawnItem)
        {
            d1 = Random.Range(0, 7);
            if (d1 == 1 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon1 = true;
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Weapon1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
            }
            if (d1 == 2 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon2 = true;
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Weapon2, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
            }
            if (d1 == 3 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon3 = true;
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Weapon3, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
            }
            if (d1 == 4 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon4 = true;
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Weapon4, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
            }
            if (d1 == 5 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon5 = true;
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Weapon5, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
            }
            if (d1 == 6 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon6 = true;
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Weapon6, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
            }
            //Ammo
            if (Weapon1 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawwner1.transform.position.x, AmmoSpawwner1.transform.position.y, AmmoSpawwner1.transform.position.z), Quaternion.identity);
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawwner2.transform.position.x, AmmoSpawwner2.transform.position.y, AmmoSpawwner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon2 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo2, new Vector3(AmmoSpawwner1.transform.position.x, AmmoSpawwner1.transform.position.y, AmmoSpawwner1.transform.position.z), Quaternion.identity);
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo2, new Vector3(AmmoSpawwner2.transform.position.x, AmmoSpawwner2.transform.position.y, AmmoSpawwner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon3 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawwner1.transform.position.x, AmmoSpawwner1.transform.position.y, AmmoSpawwner1.transform.position.z), Quaternion.identity);
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawwner2.transform.position.x, AmmoSpawwner2.transform.position.y, AmmoSpawwner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon4 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo3, new Vector3(AmmoSpawwner1.transform.position.x, AmmoSpawwner1.transform.position.y, AmmoSpawwner1.transform.position.z), Quaternion.identity);
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo3, new Vector3(AmmoSpawwner2.transform.position.x, AmmoSpawwner2.transform.position.y, AmmoSpawwner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon5 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo2, new Vector3(AmmoSpawwner1.transform.position.x, AmmoSpawwner1.transform.position.y, AmmoSpawwner1.transform.position.z), Quaternion.identity);
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo2, new Vector3(AmmoSpawwner2.transform.position.x, AmmoSpawwner2.transform.position.y, AmmoSpawwner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon6 && GameObject.Find("GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawwner1.transform.position.x, AmmoSpawwner1.transform.position.y, AmmoSpawwner1.transform.position.z), Quaternion.identity);
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawwner2.transform.position.x, AmmoSpawwner2.transform.position.y, AmmoSpawwner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
        }
	}
}
