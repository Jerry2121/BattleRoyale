using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerItemInteractions : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    public void CmdEquipWeaponFromItem(NetworkInstanceId _netID)
    {
        RpcEquipWeaponFromItem(_netID);
    }
    [ClientRpc]
    void RpcEquipWeaponFromItem(NetworkInstanceId _netID)
    {
        GameObject itemGO = ClientScene.FindLocalObject(_netID);
        itemGO.GetComponent<WeaponItem>().OnWeaponEquip(transform.name);
    }

    [Command]
    public void CmdTakeItem(NetworkInstanceId _netID)
    {
        RpcTakeItem(_netID);
    }
    [ClientRpc]
    void RpcTakeItem(NetworkInstanceId _netID)
    {
        GameObject item = ClientScene.FindLocalObject(_netID);
        if (item != null)
            item.SetActive(false);
    }

    [Command]
    public void CmdDropItem(NetworkInstanceId _netID, Vector3 _position)
    {
        RpcDropItem(_netID, _position);
    }

    [ClientRpc]
    void RpcDropItem(NetworkInstanceId _netID, Vector3 _position)
    {
        GameObject item = ClientScene.FindLocalObject(_netID);
        if (item != null)
        {
            item.transform.position = _position;
            item.SetActive(true);
        }
    }

}
