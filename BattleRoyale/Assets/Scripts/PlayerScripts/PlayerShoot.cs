using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";
    
    [SerializeField]
    private Camera cam;
    [SerializeField]
    Camera weaponCam;
    [SerializeField]
    float scopedFOV = 15f;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    Canvas sniperScopeOverlayCanvasPrefab;
    Canvas sniperScopeOverlayCanvas;

    private bool isShooting;
    private float camNormalFOV;

    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;

    public int meleeDamage;
    public float meleeRange;
    public float meleeCooldown;

	// Use this for initialization
	void Start () {
        if (cam == null)
        {
            this.enabled = false;
            throw new System.Exception("PlayerShoot -- Start: No camera referenced");
        }

        weaponManager = GetComponent<WeaponManager>();

        sniperScopeOverlayCanvas = Instantiate(sniperScopeOverlayCanvasPrefab);
        sniperScopeOverlayCanvas.enabled = false;

        camNormalFOV = cam.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if (PauseMenu.isOn)
            return;

        if (currentWeapon == null)
            return;

        //We're aiming
        if (Input.GetButton("Fire2"))
        {
            if (currentWeapon == null)
            {
                if (cam.fieldOfView <= 60)
                {
                    cam.fieldOfView = 60;
                }

                else
                    cam.fieldOfView -= 5;
            }

            else if (currentWeapon.name == "Pistol")
            {
                if (cam.fieldOfView <= 55)
                {
                    cam.fieldOfView = 55;
                }

                else
                    cam.fieldOfView -= 4;
            }

            else if (currentWeapon.name == "Automatic")
            {
                if (cam.fieldOfView <= 45)
                {
                    cam.fieldOfView = 45;
                }

                else
                    cam.fieldOfView -= 3;
            }

            else if (currentWeapon.name == "Heavy")
            {
                if (cam.fieldOfView <= 45)
                {
                    cam.fieldOfView = 45;
                }

                else
                    cam.fieldOfView -= 2;
            }
            else if (currentWeapon.name == "Sniper") //For the sniper we need to scope
            {
                Animator anim = weaponManager.GetCurrentGraphics().GetComponent<Animator>();
                StartCoroutine(OnScoped(true));
                anim.SetBool("Scoped", true);

            }
            else
            {
                if (cam.fieldOfView <= 45)
                {
                    cam.fieldOfView = 45;
                }

                else
                    cam.fieldOfView -= 2;
            }
        }

        //We're not aiming anymore
        else
        {
            if (currentWeapon.name == "Sniper")
            {
                Animator anim = weaponManager.GetCurrentGraphics().GetComponent<Animator>();
                StartCoroutine(OnScoped(false));
                anim.SetBool("Scoped", false);
            }

            else if (cam.fieldOfView >= 70)
            {
                cam.fieldOfView = 70;
            }

            else
                cam.fieldOfView += 3;
        }


        if (currentWeapon.currentAmmo < currentWeapon.maxAmmo)
        {
            if (Input.GetButtonDown("Reload") && weaponManager.isReloading == false)
            {
                weaponManager.Reload();
                return;
            }
        }

        if (meleeCooldown > 0)
        {
            meleeCooldown -= Time.deltaTime;
        }

        if (currentWeapon.fireRate <= 0)
        {
            if (Input.GetButtonDown("Fire1") && PlayerUI.InInventory == false)
            {
                Shoot();
            }

            else if (Input.GetKeyDown(KeyCode.Z) && PlayerUI.InInventory == false)
            {
                if (meleeCooldown <= 0)
                {
                    MeleeAttack();
                    meleeCooldown += 2;
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && PlayerUI.InInventory == false && IsInvoking("Shoot") == false)
            {
                isShooting = true;
                InvokeRepeating("Shoot", 0f, 1f/currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1") && PlayerUI.InInventory == false && IsInvoking("Shoot") == false)
            {
                isShooting = false;
            }
        }
	}

    //called on server when a player shoots
    [Command]
    void CmdOnShoot()
    {
        RpcDoShootEffect();
    }

    //called on all clients when we need to display a shoot effect
    [ClientRpc]
    void RpcDoShootEffect()
    {
        if(weaponManager.GetCurrentGraphics() == null)
        {
            throw new System.Exception("PlayerShoot -- RpcDoShootEffect: CurrentGraphics is null!");
            //Debug.LogError("PlayerShoot -- RpcDoShootEffect: CurrentGraphics is null!");
            //return;
        }
        weaponManager.GetCurrentGraphics().GetComponent<AudioSource>().Play();
        weaponManager.GetCurrentGraphics().muzzleFlash.Play();
    }

    //called when we hit somerhing
    [Command]
    void CmdOnHit(Vector3 _pos, Vector3 _normal)
    {
        RpcDoHitEffect(_pos, _normal);
        
    }

    //called on all clients to spawn in a hit effect
    [ClientRpc]
    void RpcDoHitEffect(Vector3 _pos, Vector3 _normal)
    {
        GameObject hitEffect = SimplePool.Spawn(weaponManager.GetCurrentGraphics().impactEffectPrefab, _pos, Quaternion.LookRotation(_normal));
        //Utility.DespawnAfterSeconds(hitEffect, 2f);
        //SimplePool.Despawn(hitEffect);
        StartCoroutine(Utility.DespawnAfterSeconds(hitEffect, 2f));
    }

    [Client] // called on the local client
    void MeleeAttack()
    {
        if (isLocalPlayer == false || weaponManager.isReloading == true)
            return;

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, meleeRange, mask))
        {
            //We hit something
            if (hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(hit.collider.name, meleeDamage, transform.name);
                // Show Hitmarker
                GetComponent<Player>().PlayerUI.GetComponent<damageUI>().ShowHitMarker();
                // play sound?
            }
            //we hit something, call OnHit method on server
            CmdOnHit(hit.point, hit.normal);
        }
    }

    [Client] //called on the local client
    void Shoot()
    {
        if (isLocalPlayer == false || weaponManager.isReloading == true)
            return;

        if (isShooting == false)
        {
            CancelInvoke("Shoot");
            return;
        }

        if (currentWeapon.currentAmmo <= 0)
        {
            weaponManager.Reload();
            return;
        }

        currentWeapon.currentAmmo--;
        

        //we are shooting call on shoot method on server
        CmdOnShoot();

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, currentWeapon.range, mask))
        {
            //We hit something
            if(hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(hit.collider.name, currentWeapon.damage, transform.name);
                // Show Hitmarker
                GetComponent<Player>().PlayerUI.GetComponent<damageUI>().ShowHitMarker();
                // play sound?
            }
            //we hit something, call OnHit method on server
            CmdOnHit(hit.point, hit.normal);
        }

        if (currentWeapon.currentAmmo <= 0)
        {
            weaponManager.Reload();
            return;
        }

    }

    [Command] //called on the server
    void CmdPlayerShot(string _playerID, int _damage, string _sourceID)
    {
        if (Debug.isDebugBuild)
            Debug.Log(_playerID + " has been shot");

        Player player = GameManager.GetPlayer(_playerID);

        player.RpcTakeDamage(_damage, _sourceID);
    }

    IEnumerator OnScoped(bool scoped)
    {
        yield return new WaitForSeconds(0.15f);
        sniperScopeOverlayCanvas.enabled = scoped;
        weaponCam.enabled = !scoped;
        if (scoped)
            cam.fieldOfView = scopedFOV;
        else
            cam.fieldOfView = camNormalFOV;
    }

}


