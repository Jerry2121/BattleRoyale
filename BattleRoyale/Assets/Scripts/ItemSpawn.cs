using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemSpawn : NetworkBehaviour {

    public GameObject itemPrefab;

    NetworkManager networkManager;
    NetworkDiscoveryScript networkDiscoveryScript;

    // Use this for initialization
    void Start () {
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();

        //Only Instantiate objects on the server, then tell the server to spawn it on all clients
        if (networkDiscoveryScript.isServer)
        {
            StartCoroutine(WaitForInstance());
        }
    }

    IEnumerator WaitForInstance()
    {
        Debug.Log("Wait");
        yield return new WaitForSeconds(5);
        if (GameManager.Instance == null)
            yield return new WaitForSeconds(5);
        if (GameManager.Instance == null)
            throw new System.NullReferenceException("GameManager.Instance is null");

        GameManagerScript gameManagerScript = GameManager.Instance.gameManagerScript;

        if (gameManagerScript.DisableItemSpawning)
            yield break;

        Utility.InstantiateOverNetwork(gameManagerScript.items[Random.Range(0, gameManagerScript.items.Length)], transform.position, Quaternion.identity);
    }
}
