using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar] //When this is changed on the server, the sever will push it to all clients
    private int currentHealth;
    [SerializeField]
    Behaviour[] disableOnDeath;

    [SerializeField]
    GameObject[] disableGameObjectsOnDeath;

    private bool[] wasEnabled;

    [SyncVar]
    private bool isDead = false;
    public bool IsDead { get { return isDead; } protected set { isDead = value; } } //Done this way instead of "public bool isDead {get; protected set}" so that it can be set as a SyncVar

    [SerializeField]
    private GameObject spawnEffect;
    [SerializeField]
    private GameObject deathEffect;

    public int kills;
    public int deaths;

    private bool firstSetup = true;

    public void SetupPlayer()
    {
        if (isLocalPlayer)
        {
            //Disable the scene camera for the new/respawning player
            GameManager.instance.SetSceneCameraActiveState(false);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
        }
        //Tell they server a player needs to be setup on all clients
        CmdBroadcastNewPlayerSetup();
    }

    [Command] //will be called on the server
    private void CmdBroadcastNewPlayerSetup()
    {
        RpcSetupPlayerOnAllClients();
    }

    [ClientRpc]
    private void RpcSetupPlayerOnAllClients()
    {
        if (firstSetup)
        {
            wasEnabled = new bool[disableOnDeath.Length];
            for (int i = 0; i < wasEnabled.Length; i++)
            {
                wasEnabled[i] = disableOnDeath[i].enabled;
            }
            firstSetup = false;
        }
        SetDefaults();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                RpcTakeDamage(10009, "Aaron");
            }
        }
    }

    public void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;

        //Enable components
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        //set gameobjects to active
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(true);
        }
        //Enable collider
        Collider col = GetComponent<Collider>(); //colliders arent behaviours, so they can't be added to the array
        if (col != null)
            col.enabled = true;

        //create spawn effect
        GameObject gfxInstance = Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(gfxInstance, 3f);
    }

    [ClientRpc] //Called on all clients from the server
    public void RpcTakeDamage(int _damage, string _sourceID)
    {
        if (isDead)
            return;

        currentHealth -= _damage;
        if (Debug.isDebugBuild)
            Debug.Log(transform.name + " now has " + currentHealth + " health");
        if(currentHealth <= 0)
        {
            Die(_sourceID);
        }
    }

    private void Die(string _sourceID)
    {
        isDead = true;

        Player sourcePlayer = GameManager.GetPlayer(_sourceID);
        if (sourcePlayer != null)
        {
            sourcePlayer.kills++;
        }
        deaths++;

        //disable components
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        //Disable graphics
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(false);
        }
        //disable collider
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;
        //spawn death effect
        GameObject gfxInstance = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gfxInstance, 3f);

        //Turn scene camera on for the dieing player
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActiveState(true);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(false);
        }

        if (Debug.isDebugBuild)
            Debug.Log(transform.name + " is dead");

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);
        
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        yield return new WaitForSeconds(0.5f);//this is to make sure the network has updated the position of the player before we spawn him in with effects and everything

        SetupPlayer();

        if (Debug.isDebugBuild)
            Debug.Log(transform.name + " respawned");
    }

}
