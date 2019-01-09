using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar] //When this is changed on the server, the server will push it to all clients
    private int currentHealth;
    [SyncVar]
    public string username = "Loading...";

    [HideInInspector]
    public GameObject outsideOfZoneImage;

    [SerializeField]
    Transform cam2;

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

    public float rayLength = 2;

    [SerializeField]
    LayerMask weaponItemMask;
    [SerializeField]
    LayerMask airDropMask;

    public PlayerItemInteractions itemInteractions;

    [SerializeField]
    GameObject SpectCameraPrefab;
    [SerializeField]
    public GameObject PlayerUI;

    public int kills;
    public int deaths;

    private bool firstSetup = true;
    bool inBounds;
    float zoneDamageTimer = 1f;

    public void SetupPlayer()
    {
        if (isLocalPlayer)
        {
            //Disable the scene camera for the new/respawning player
            GameManager.instance.SetSceneCameraActiveState(false);
            PlayerUI = GetComponent<PlayerSetup>().playerUIInstance;
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
        RaycastHit hit;
        Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, weaponItemMask) && !PauseMenu.isOn)
        {
            Debug.Log("Player -- Update: Hit a weapon item");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");
                //hit.transform.GetComponent<WeaponItem>().OnWeaponEquip(transform.name);
                itemInteractions.CmdEquipWeaponFromItem(hit.transform.gameObject.GetComponent<NetworkIdentity>().netId);
            }
        }
        else if (Physics.Raycast(transform.position, fwd, out hit, rayLength, airDropMask) && !PauseMenu.isOn)
        {
            Debug.Log("Player -- Update: Hit an airdrop");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");
                //hit.transform.GetComponent<WeaponItem>().OnWeaponEquip(transform.name);
                itemInteractions.CmdOpenAirDrop(hit.transform.gameObject.GetComponent<NetworkIdentity>().netId);
            }
        }

        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                CmdTakeDamage(20, "Dev");
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                CmdHeal(20);
            }
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (UserAccountManager.PlayerUsername == "Aaron")
            {
                foreach (Player player in GameManager.GetAllPlayers())
                {
                    player.CmdTakeDamage(9999, this.username);
                }
            }
        }
        if (transform.position.y <= -50)
        {
            CmdTakeDamage(9999, "Dev");
        }
        if (inBounds == false && GameManager.instance.inStartPeriod == false)
        {
            zoneDamageTimer -= Time.deltaTime;
            if (zoneDamageTimer <= 0f)
            {
                CmdTakeDamage(5, "Dev");
                zoneDamageTimer = 1f;
            }
        }
        else
            zoneDamageTimer = 1f;

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

    [Command]
    public void CmdHeal(int _amount)
    {
        RpcHeal(_amount);
    }

    [ClientRpc]
    public void RpcHeal(int _amount)
    {
        currentHealth += _amount;
        if (Debug.isDebugBuild)
            Debug.Log(transform.name + " healed " + _amount + " health");
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    [Command]
    void CmdTakeDamage(int _damage, string _sourceID) //only call on player, other scripts like shoot have their own Command to call RpcTakeDamage
    {
        RpcTakeDamage(_damage, _sourceID);
    }

    [ClientRpc] //Called on all clients from the server
    public void RpcTakeDamage(int _damage, string _sourceID)
    {
        try
        {
            // Show damage direction indicators
            Transform cameraTransformRe = cam2;
            Vector3 camTran = cameraTransformRe.parent.transform.forward;
            camTran.y = 0;
            // reset reCameraTransform's y position to 0

            //find player with _sourceID and get their position
            Vector3 damageSourcePosition = GameManager.GetPlayer(_sourceID).transform.position;
            Vector3 damageSourceRe = new Vector3(damageSourcePosition.x, 0, damageSourcePosition.z);
            Vector3 damageDirection = camTran - damageSourceRe;
            Vector3 damageAxis = new Vector3(0, 1, 0);
            float angle = Vector3.SignedAngle(damageDirection, camTran, damageAxis);
            Debug.Log("Angle = " + angle);
            PlayerUI.GetComponent<damageUI>().FindDamageSourceDirection(angle);
        }
        catch
        {

        }


        if (isDead || GameManager.IsGameOver())
            return;

        currentHealth -= _damage;

        if (Debug.isDebugBuild)
            Debug.Log(transform.name + " took damage");

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
            GameManager.instance.onPlayerKilledCallback.Invoke(username, sourcePlayer.username);
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

        if(GameManager.instance == null)
        {
            Debug.LogError("Player -- Die: There is no instance of the GameManager!");
            return;
        }
        if(GameManager.instance.matchSettings.canRespawn)
            StartCoroutine(Respawn());
        else
        {
            if (isLocalPlayer)
            {
                GameManager.UnregisterPlayer(transform.name);
                StartCoroutine(CreateSpectatorCam());
                Destroy(this.gameObject, 2f);
            }
            else
            {
                GameManager.UnregisterPlayer(transform.name);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        yield return new WaitForSeconds(0.5f);//this is to make sure the network has updated the position of the player before we spawn him in with effects and everything

        SetupPlayer();

        if (Debug.isDebugBuild)
            Debug.Log(transform.name + " respawned");
    }

    IEnumerator CreateSpectatorCam()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(SpectCameraPrefab, transform.position, transform.rotation);
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ZoneWall")
        {
            inBounds = false;
            if(outsideOfZoneImage != null)
                outsideOfZoneImage.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ZoneWall")
        {
            inBounds = true;
            if (outsideOfZoneImage != null)
                outsideOfZoneImage.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Utility.WaitForEndOfFrame();
        if (other.gameObject.tag == "Lobby" && GameManager.instance.inStartPeriod == false)
        {
            
            if (isLocalPlayer)
            {
                CmdTakeDamage(999999999, "Dev");
            }
        }
    }

}
