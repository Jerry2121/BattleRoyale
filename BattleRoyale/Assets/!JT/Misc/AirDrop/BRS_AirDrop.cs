using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRS_AirDrop : MonoBehaviour
{

	public GameObject GroundDetection;
	public GameObject Canopy;
	public Light DropLight;
	public ParticleSystem Smoke;
	private Rigidbody AirDropRB;
	private bool Landed = false;

	// Use this for initialization
	void Start ()
	{
		AirDropRB = transform.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit objectHit;

		if (Physics.Raycast (transform.position, Vector3.down, out objectHit, 1))
		{
			if (objectHit.collider.gameObject.name != "FPSController")
			{
				Landed = true;
			}
		}

		if (Landed)
		{
			DropHasLanded ();
			Landed = false;
		}
	}

	void DropHasLanded()
	{
		DropLight.gameObject.SetActive (true);
		Smoke.gameObject.SetActive (true);
		AirDropRB.drag = 0;
		AirDropRB.mass = 5000;
		Destroy (GroundDetection);
		Destroy (Canopy);
	}
}
