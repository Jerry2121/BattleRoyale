using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerItemInteractions : NetworkBehaviour {

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

    [Command]
    public void CmdOpenAirDrop(NetworkInstanceId _netID)
    {
        //RpcOpenAirDrop(_netID);
        GameObject Airdrop = NetworkServer.FindLocalObject(_netID);
        Airdrop.GetComponent<AirDropItemSpawn>().SpawnSupplies();
    }
    public void RpcOpenAirDrop(NetworkInstanceId _netID)
    {
        GameObject Airdrop = ClientScene.FindLocalObject(_netID);
    }

}
