using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField]
    string weaponLayerName = "Weapon";
    [SerializeField]
    PlayerWeapon primaryWeapon;
    [SerializeField]
    PlayerWeapon secondaryWeapon;
    [SerializeField]
    Transform weaponHolder;

    public WeaponSwitchingUI weaponSwitchingUI;
    private PlayerWeapon currentWeapon;
    GameObject currentWeaponGameObject;
    private WeaponGraphics currentGraphics;
    public int selectedWeapon = 0;

    [HideInInspector]
    public bool isReloading = false;

	// Use this for initialization
	void Start () {
        GameObject pui = GetComponent<PlayerSetup>().playerUIInstance;
        weaponSwitchingUI = pui.GetComponent<WeaponSwitchingUI>();
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
    public void SwitchWeaponLocal(PlayerWeapon _weapon)
    {
        if (isLocalPlayer == false)
        {
            if (Debug.isDebugBuild)
                Debug.LogError("WeaponManager -- SwitchWeaponLocal: This script is being called on a player that is not the local player! This should only be called on the local client, call SwitchWeaponRemote for remote clients.");
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
                Debug.LogError("WeaponManager -- SwitchWeaponLocal: The passed in weapon was not equal to any of the equipped weapons! If you meant to equip a new weapon, use EquipWeapon instead");
            return;
        }

        weaponSwitchingUI.selectedSlot = weaponNum;
        selectedWeapon = weaponNum;

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
            Debug.LogError("WeaponManager -- SwitchWeaponLocal: There is no WeaponGraphics on the " + weaponIns.name + " weapon object!");
        }
        else
        {
            weaponHolder.localRotation = Quaternion.Euler(currentGraphics.rotationOffset);
        }

        CmdOnWeaponChanged(weaponNum);
        Utility.SetLayerRecursively(weaponIns, LayerMask.NameToLayer(weaponLayerName));
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
    /// Equips the passed in weapon, replacing the weapon corresponding to the weaponNum with the passed in weapon, e.g. EquipWeapon(weapon, 1) would replace the primary weapon(which is weapon 1) 
    /// </summary>
    /// <param name="_weapon"></param>
    /// <param name="_weaponNum"></param>
    public void EquipWeapon(PlayerWeapon _weapon, int _weaponNum)
    {
        int currentWeaponNum = 0;

        if (currentWeapon == primaryWeapon)
            currentWeaponNum = 1;
        else if (currentWeapon == secondaryWeapon)
            currentWeaponNum = 2;

        _weapon.currentAmmo = _weapon.maxAmmo;

        if(_weaponNum == 1)
        {
            primaryWeapon = _weapon;
            if (currentWeaponNum == 1)
                SwitchWeaponLocal(primaryWeapon);
        }
        else if(_weaponNum == 2)
        {
            secondaryWeapon = _weapon;
            if (currentWeaponNum == 2)
                SwitchWeaponLocal(secondaryWeapon);
        }
        else
        {
            if (Debug.isDebugBuild)
                Debug.LogError("WeaponManager -- EquipWeapon: There is no weapon corresponding to the number " + _weaponNum);
            return;
        }

        weaponSwitchingUI.selectedSlot = currentWeaponNum;
        weaponSwitchingUI.ChangeWeaponInSlot(_weapon);

    }
    
    public void Reload()
    {
        if(isReloading)
            return;

        if (Debug.isDebugBuild)
            Debug.Log("Reloading");

        StartCoroutine(Reload_Coroutine());
    }

    IEnumerator Reload_Coroutine()
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

        currentWeapon.currentAmmo = currentWeapon.maxAmmo;

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
            if (Debug.isDebugBuild)
                Debug.LogError("WeaponManager -- SwitchWeaponRemote: There is no weapon corresponding to the number " + _weaponNum);
            return;
        }

        if (currentWeaponGameObject != null)
        {
            Destroy(currentWeaponGameObject);
            currentWeaponGameObject = null;
        }

        currentWeapon = weapon;

        GameObject weaponIns = Instantiate(weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);
        currentWeaponGameObject = weaponIns;
        currentGraphics = weaponIns.GetComponent<WeaponGraphics>();
        if (currentGraphics == null)
        {
            Debug.LogError("WeaponManager -- EquipWeaponRemote: There is no WeaponGraphics on the " + weaponIns.name + " weapon object!");
        }
        else
        {
            weaponHolder.localRotation = Quaternion.Euler(currentGraphics.rotationOffset);
        }
    }

    public PlayerWeapon GetWeaponFromInt(int _weaponNum)
    {
        if(_weaponNum == 1)
            return primaryWeapon;
        if (_weaponNum == 2)
            return secondaryWeapon;
        else return null;
    }

}
