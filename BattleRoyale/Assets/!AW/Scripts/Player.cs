using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar] //When this is changed on the server, the sever will push it to all clients
    private int currentHealth;
    [SerializeField]
    Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SyncVar]
    private bool isDead = false;
    public bool IsDead { get { return isDead; } protected set { isDead = value; } } //Done this way instead of "public bool isDead {get; protected set}" so that it can be set as a SyncVar
    
    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.K)){
            RpcTakeDamage(10009);
        }

    }

    public void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider col = GetComponent<Collider>(); //colliders arent behaviours, so they can't be added to the array
        if (col != null)
            col.enabled = true;
    }

    [ClientRpc] //Called on all clients from the server
    public void RpcTakeDamage(int _damage)
    {
        if (isDead)
            return;

        currentHealth -= _damage;

        Debug.Log(transform.name + " now has " + currentHealth + " health");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Debug.Log(transform.name + " is dead");

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        SetDefaults();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        Debug.Log(transform.name + " respawned");
    }

}
