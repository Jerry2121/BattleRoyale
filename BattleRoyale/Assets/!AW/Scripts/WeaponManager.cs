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

    private PlayerWeapon currentWeapon;
    GameObject currentWeaponGameObject;
    private WeaponGraphics currentGraphics;

    [HideInInspector]
    public bool isReloading = false;

	// Use this for initialization
	void Start () {
        SwitchWeaponLocal(primaryWeapon, 1);
	}

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwitchWeaponLocal(primaryWeapon, 1);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SwitchWeaponLocal(secondaryWeapon, 2);
        }

	}

    void SwitchWeaponLocal(PlayerWeapon _weapon, int _weaponNum)
    {
        if (currentWeaponGameObject != null)
        {
            Destroy(currentWeaponGameObject);
            currentWeaponGameObject = null;
        }

        currentWeapon = _weapon;
        currentWeapon.currentAmmo = _weapon.currentAmmo;

        GameObject weaponIns = Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);
        currentWeaponGameObject = weaponIns;
        currentGraphics = weaponIns.GetComponent<WeaponGraphics>();
        if(currentGraphics == null)
        {
            Debug.LogError("WeaponManager -- SwitchWeaponLocal: There is no WeaponGraphics on the " + weaponIns.name +" weapon object!");
        }
        else
        {
            weaponHolder.localRotation = Quaternion.Euler(currentGraphics.rotationOffset);
        }

        if (isLocalPlayer)
        {
            CmdOnWeaponChanged(_weaponNum);
            Utility.SetLayerRecursively(weaponIns, LayerMask.NameToLayer(weaponLayerName));
        }
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }

    public void EquipWeapon(PlayerWeapon _weapon, int _weaponNum)
    {
        int currentWeaponNum = 0;

        if (currentWeapon == primaryWeapon)
            currentWeaponNum = 1;
        else if (currentWeapon == secondaryWeapon)
            currentWeaponNum = 2;

        if(_weaponNum == 1)
        {
            primaryWeapon = _weapon;
        }
        if(_weaponNum == 2)
        {
            secondaryWeapon = _weapon;
        }
        else
        {
            if (Debug.isDebugBuild)
                Debug.LogError("WeaponManager -- EquipWeapon: There is no weapon corresponding to the number " + _weaponNum);
            return;
        }



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

        yield return new WaitForSeconds(currentWeapon.reloadTime);

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
            return;

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

}
