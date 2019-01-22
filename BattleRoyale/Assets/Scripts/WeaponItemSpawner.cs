using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponItemSpawner : MonoBehaviour {
    [Header("AmmoSpawners")]
    public GameObject AmmoSpawner1;
    public GameObject AmmoSpawner2;
    private WeaponType weaponType;

    NetworkManager networkManager;
    NetworkDiscoveryScript networkDiscoveryScript;
    bool run = false;
    
    // Use this for initialization
    void Start () {
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();
        
        if (networkDiscoveryScript.isServer)
        {
            /*WaitForInstance();
            while(run == false)
            {
                Debug.Log("waiting");
            }*/

            /*if (GameManager.Instance == null)
            {
                throw new System.Exception("The GameManager instance is not set");
            }*/

            GameManagerScript gameManagerScript = GameManager.Instance.GetComponent<GameManagerScript>();

            if (gameManagerScript.DisableItemSpawning)
                return;

            GameObject weaponSpawned = Utility.InstantiateOverNetwork(gameManagerScript.weapons[Random.Range(0, gameManagerScript.weapons.Length - 1)], transform.position, Quaternion.identity);
            weaponType = weaponSpawned.GetComponent<WeaponItem>().weapon.weaponType;

            #region OldCode
            /*if (d1 == 1 && canspawnItem && !ran)
            {
                Weapon1 = true;
                Utility.InstantiateOverNetwork(gameManagerScript.Weapon1, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 2 && canspawnItem && !ran)
            {
                Weapon2 = true;
                Utility.InstantiateOverNetwork(gameManagerScript.Weapon2, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 3 && canspawnItem && !ran)
            {
                Weapon3 = true;
                Utility.InstantiateOverNetwork(gameManagerScript.Weapon3, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 4 && canspawnItem && !ran)
            {
                Weapon4 = true;
                Utility.InstantiateOverNetwork(gameManagerScript.Weapon4, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 5 && canspawnItem && !ran)
            {
                Weapon5 = true;
                Utility.InstantiateOverNetwork(gameManagerScript.Weapon5, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 6 && canspawnItem && !ran)
            {
                Weapon6 = true;
                Utility.InstantiateOverNetwork(gameManagerScript.Weapon6, transform.position, Quaternion.identity);
                ran = true;
            }
            
            //Ammo

            if (Weapon1 && ran)
            {
                Utility.InstantiateOverNetwork(gameManagerScript.lightAmmo, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManagerScript.lightAmmo, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon2 && ran)
            {
                Utility.InstantiateOverNetwork(gameManagerScript.mediumAmmo, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManagerScript.mediumAmmo, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon3 && ran)
            {
                Utility.InstantiateOverNetwork(gameManagerScript.lightAmmo, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManagerScript.lightAmmo, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon4 && ran)
            {
                Utility.InstantiateOverNetwork(gameManagerScript.heavyAmmo, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManagerScript.heavyAmmo, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon5 && ran)
            {
                Utility.InstantiateOverNetwork(gameManagerScript.mediumAmmo, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManagerScript.mediumAmmo, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon6 && ran)
            {
                Utility.InstantiateOverNetwork(gameManagerScript.lightAmmo, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManagerScript.lightAmmo, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }*/
            #endregion


            if (weaponType == WeaponType.Light)
            {
                Utility.InstantiateOverNetwork(gameManagerScript.lightAmmo, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManagerScript.lightAmmo, AmmoSpawner2.transform.position, Quaternion.identity);
            }
            else if (weaponType == WeaponType.Medium)
            {
                Utility.InstantiateOverNetwork(gameManagerScript.mediumAmmo, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManagerScript.mediumAmmo, AmmoSpawner2.transform.position, Quaternion.identity);
            }
            else if (weaponType == WeaponType.Heavy)
            {
                Utility.InstantiateOverNetwork(gameManagerScript.heavyAmmo, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManagerScript.heavyAmmo, AmmoSpawner2.transform.position, Quaternion.identity);
            }
            else
            {
                throw new System.Exception("The weapon type is unaccounted for, or is null!");
            }
        }
    }
	
	IEnumerator WaitForInstance()
    {
        Debug.Log("Wait");
        yield return new WaitForSeconds(5);
        if (GameManager.Instance == null)
            WaitForInstance();
        else
            run = true;
    }

}
