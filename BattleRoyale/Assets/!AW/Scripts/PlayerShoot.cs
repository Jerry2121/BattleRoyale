using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

	// Use this for initialization
	void Start () {
		if(cam == null)
        {
            Debug.LogError("PlayerShhot -- Start: No camera referenced");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
	}

    [Client] //called on the local client
    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            //We hit something
            if(hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(hit.collider.name);
            }
        }

    }

    [Command] //called on the server
    void CmdPlayerShot(string _ID)
    {
        Debug.Log(_ID + " has been shot");
    }

}
