using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField]
    string weaponLayerName = "Weapon";
    [SerializeField]
    public PlayerWeapon primaryWeapon { get; protected set; }
    [SerializeField]
    public PlayerWeapon secondaryWeapon { get; protected set; }
    [SerializeField]
    Transform weaponHolder;

    public WeaponSwitchingUI weaponSwitchingUI;
    private PlayerWeapon currentWeapon;
    GameObject currentWeaponGameObject;
    private WeaponGraphics currentGraphics;
    public int selectedWeapon = 1;

    [HideInInspector]
    public bool isReloading = false;

	// Use this for initialization
	void Start () {
        primaryWeapon = secondaryWeapon = null;

        if (isLocalPlayer == false)
            return;

        if (GetComponent<PlayerSetup>().playerUIInstance != null) //This will be null on player other than the Local Player
        {
            GameObject pui = GetComponent<PlayerSetup>().playerUIInstance;
            weaponSwitchingUI = pui.GetComponent<WeaponSwitchingUI>();
        }
        SwitchWeaponLocal(primaryWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwitchWeaponLocal(primaryWeapon);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SwitchWeaponLocal(secondaryWeapon);
        }

	}

    /// <summary>
    /// Switches the current weapon of the local player
    /// </summary>
    /// <param name="_weapon"></param>
    [Client]
    public void SwitchWeaponLocal(PlayerWeapon _weapon)
    {
        if (isLocalPlayer == false)
        {
            if (Debug.isDebugBuild)
                Debug.LogError("WeaponManager -- SwitchWeaponLocal: This script is being called on a player that is not the local player! This should only be called on the local client, call SwitchWeaponRemote for remote clients.", this);
            return;
        }

        int weaponNum = 0;
        if (_weapon == primaryWeapon)
            weaponNum = 1;
        else if (_weapon == secondaryWeapon)
            weaponNum = 2;
        else
        {
            if (Debug.isDebugBuild)
                Debug.LogError("WeaponManager -- SwitchWeaponLocal: The passed in weapon was not equal to any of the equipped weapons! If you meant to equip a new weapon, use EquipWeapon instead", this);
            return;
        }
        if(weaponSwitchingUI != null)
           weaponSwitchingUI.selectedSlot = weaponNum;
        selectedWeapon = weaponNum;

        if (_weapon == null)
        {
            currentWeapon = null;
            Destroy(currentWeaponGameObject);
            return;
        }

        if (currentWeaponGameObject != null)
        {
            Destroy(currentWeaponGameObject);
            currentWeaponGameObject = null;
        }

        currentWeapon = _weapon;
        currentWeapon.currentAmmo = _weapon.currentAmmo;
        if(currentWeapon.currentAmmo <= 0)
        {
            Reload();
        }

        GameObject weaponIns = Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);
        currentWeaponGameObject = weaponIns;
        currentGraphics = weaponIns.GetComponent<WeaponGraphics>();
        if (currentGraphics == null)
        {
            Debug.LogError("WeaponManager -- SwitchWeaponLocal: There is no WeaponGraphics on the " + weaponIns.name + " weapon object!", this);
        }
        else
        {
            weaponHolder.localRotation = Quaternion.Euler(currentGraphics.rotationOffset);
        }

        CmdOnWeaponChanged(weaponNum);
        Utility.SetLayerRecursively(weaponIns, LayerMask.NameToLayer(weaponLayerName));
        return;
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }
    
    /// <summary>
    /// Equips the passed in weapon, replacing the weapon corresponding to the weaponNum with the passed in weapon, e.g. EquipWeaponLocal(weapon, 1) would replace the primary weapon(which is weapon 1) 
    /// </summary>
    /// <param name="_weapon"></param>
    /// <param name="_weaponNum"></param>
    public void EquipWeapon(PlayerWeapon _weapon, int _weaponNum)
    {
        /*if(isLocalPlayer == false)
        {
            if(Debug.isDebugBuild)
                Debug.LogError("WeaponManager -- EquipWeaponLocal: This script is being called on a player that is not the local player! This should only be called on the local client, call EquipWeaponRemote for remote clients.", this);
            return;
        }*/

        int currentWeaponNum = 0;

        if (currentWeapon == primaryWeapon)
            currentWeaponNum = 1;
        else if (currentWeapon == secondaryWeapon)
            currentWeaponNum = 2;

        _weapon.currentAmmo = _weapon.maxAmmo;

        if(_weaponNum == 1)
        {
            primaryWeapon = _weapon;
            if (currentWeaponNum == 1 && isLocalPlayer)
                SwitchWeaponLocal(primaryWeapon);
        }
        else if(_weaponNum == 2)
        {
            secondaryWeapon = _weapon;
            if (currentWeaponNum == 2 && isLocalPlayer)
                SwitchWeaponLocal(secondaryWeapon);
        }
        else
        {
            if (Debug.isDebugBuild)
                Debug.LogError("WeaponManager -- EquipWeapon: There is no weapon corresponding to the number " + _weaponNum, this);
            return;
        }

        if (weaponSwitchingUI != null)
        {
            weaponSwitchingUI.selectedSlot = currentWeaponNum;
            weaponSwitchingUI.ChangeWeaponInSlot(_weapon);
        }

        //CmdOnEquipWeapon(_weapon, _weaponNum);

    }
    
    public void Reload()
    {
        if(isReloading)
            return;

        string ammoType;
        if(currentWeapon.weaponType == WeaponType.Light)
        {
            ammoType = "LightAmmo";
        }
        else if (currentWeapon.weaponType == WeaponType.Medium)
        {
            ammoType = "MediumAmmo";
        }
        else if (currentWeapon.weaponType == WeaponType.Heavy)
        {
            ammoType = "HeavyAmmo";
        }
        else
        {
            Debug.LogError("WeaponType not found");
            throw new System.Exception("WeaponManager -- Reload: Weapon type not accounted for!");
        }

        PlayerUI playerUI = weaponSwitchingUI.GetComponent<PlayerUI>();

        int ammoNeeded = currentWeapon.maxAmmo - currentWeapon.currentAmmo;
        int ammoRecieved = 0;
        
        if (ammoType == "HeavyAmmo")
        {
            if (playerUI.heavyAmmoAmount > ammoNeeded)
            {
                playerUI.heavyAmmoAmount -= ammoNeeded;
                ammoRecieved = currentWeapon.currentAmmo + ammoNeeded;
            }
            else
            {
                ammoRecieved = currentWeapon.currentAmmo + playerUI.heavyAmmoAmount;
                playerUI.heavyAmmoAmount = 0;
            }
        }

        if (ammoType == "MediumAmmo")
        {
            if(playerUI.mediumAmmoAmount > ammoNeeded)
            {
                ammoRecieved = currentWeapon.currentAmmo + ammoNeeded;
                playerUI.mediumAmmoAmount -= ammoNeeded;
            }
            else
            {
                ammoRecieved = currentWeapon.currentAmmo + playerUI.mediumAmmoAmount;
                playerUI.mediumAmmoAmount = 0;
            }
        }

        if (ammoType == "LightAmmo")
        {
            if (playerUI.lightAmmoAmount > ammoNeeded)
            {
                playerUI.lightAmmoAmount -= ammoNeeded;
                ammoRecieved = currentWeapon.currentAmmo + ammoNeeded;
            }
            else
            {
                ammoRecieved = currentWeapon.currentAmmo + playerUI.lightAmmoAmount;
                playerUI.lightAmmoAmount = 0;
            }
        }
        
        

        /*int ammoRecieved = inventoryPanelLITE.GetAmmo(ammoType, (currentWeapon.maxAmmo - currentWeapon.currentAmmo));

        if(ammoRecieved <= 0)
        {
            Debug.Log("No ammo found of type " + ammoType);
            return;
        }*/

        if (Debug.isDebugBuild)
            Debug.Log("Reloading");

        if(ammoRecieved > currentWeapon.maxAmmo - currentWeapon.currentAmmo)
        {
            Debug.LogError("WeaponManager -- Reload: Recieved more ammo than needed! Setting ammoRecieved to the max needed amount");
            ammoRecieved = currentWeapon.maxAmmo - currentWeapon.currentAmmo;
        }
        if (ammoRecieved == 0)
            return;
        StartCoroutine(Reload_Coroutine(ammoRecieved));
    }

    IEnumerator Reload_Coroutine(int _ammoRecieved)
    {
        isReloading = true;

        CmdOnReload();

        PlayerWeapon reloadingWeapon = currentWeapon;
        yield return new WaitForSeconds(currentWeapon.reloadTime);

        //if we have switched weapons, don't reload
        if(currentWeapon != reloadingWeapon)
        {
            isReloading = false;
            yield break;
        }

        currentWeapon.currentAmmo += _ammoRecieved;

        isReloading = false;
    }

    [Command]
    void CmdOnReload()
    {
        RpcOnReload();
    }

    [ClientRpc]
    void RpcOnReload()
    {
        Animator anim = currentGraphics.GetComponent<Animator>();
        if(anim != null)
        {
            anim.SetTrigger("Reload");
        }
        else
        {
            Debug.LogError("WeaponManager -- RpcOnReload: Anim is null!", currentGraphics.gameObject);
        }
    }

    [Command]
    void CmdOnWeaponChanged(int _weaponNum)
    {
        RpcOnWeaponChanged(_weaponNum);
    }

    [ClientRpc]
    void RpcOnWeaponChanged(int _weaponNum)
    {
        SwitchWeaponRemote(_weaponNum);
    }

    void SwitchWeaponRemote(int _weaponNum)
    {
        //If we are the local player, we should have already handled weapon switching
        if (isLocalPlayer)
        {
            return;
        }

        PlayerWeapon weapon = null;
        if (_weaponNum == 1)
            weapon = primaryWeapon;
        else if (_weaponNum == 2)
            weapon = secondaryWeapon;
        else
        {
            throw new System.Exception("WeaponManager-- SwitchWeaponRemote: There is no weapon corresponding to the number " + _weaponNum);
            //if (Debug.isDebugBuild)
            //    Debug.LogError("WeaponManager -- SwitchWeaponRemote: There is no weapon corresponding to the number " + _weaponNum, this);
            //return;
        }

        if (currentWeaponGameObject != null)
        {
            Destroy(currentWeaponGameObject);
            currentWeaponGameObject = null;
        }

        if (weapon == null)
            return;

        currentWeapon = weapon;

        GameObject weaponIns = Instantiate(weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);
        Utility.SetLayerRecursively(weaponIns, LayerMask.NameToLayer("RemotePlayer"));
        currentWeaponGameObject = weaponIns;
        currentGraphics = weaponIns.GetComponent<WeaponGraphics>();
        if (currentGraphics == null)
        {
            Debug.LogError("WeaponManager -- EquipWeaponRemote: There is no WeaponGraphics on the " + weaponIns.name + " weapon object!", this);
            throw new System.Exception("WeaponManager -- EquipWeaponRemote: There is no WeaponGraphics on the " + weaponIns.name + " weapon object!");
        }
        else
        {
            weaponHolder.localRotation = Quaternion.Euler(currentGraphics.rotationOffset);
        }
    }

    /*[Command]
    void CmdOnEquipWeapon(PlayerWeapon _weapon, int _weaponNum)
    {
        RpcOnEquipWeapon(_weapon, _weaponNum);
    }

    [ClientRpc]
    void RpcOnEquipWeapon(PlayerWeapon _weapon, int _weaponNum)
    {
        EquipWeaponRemote(_weapon, _weaponNum);
    }

    public void EquipWeaponRemote(PlayerWeapon _weapon, int _weaponNum)
    {
        if (isLocalPlayer)
        {
            return;
        }

        int currentWeaponNum = 0;

        if (currentWeapon == primaryWeapon)
            currentWeaponNum = 1;
        else if (currentWeapon == secondaryWeapon)
            currentWeaponNum = 2;

        _weapon.currentAmmo = _weapon.maxAmmo;

        if (_weaponNum == 1)
        {
            primaryWeapon = _weapon;
            if (currentWeaponNum == 1)
                SwitchWeaponRemote(_weaponNum);
        }
        else if (_weaponNum == 2)
        {
            secondaryWeapon = _weapon;
            if (currentWeaponNum == 2)
                SwitchWeaponRemote(_weaponNum);
        }
        else
        {
            if (Debug.isDebugBuild)
                Debug.LogError("WeaponManager -- EquipWeapon: There is no weapon corresponding to the number " + _weaponNum);
            return;
        }
    }*/

    public PlayerWeapon GetWeaponFromInt(int _weaponNum)
    {
        if(_weaponNum == 1)
            return primaryWeapon;
        if (_weaponNum == 2)
            return secondaryWeapon;
        else return null;
    }

}
