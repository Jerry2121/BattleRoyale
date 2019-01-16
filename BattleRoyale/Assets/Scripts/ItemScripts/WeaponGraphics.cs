using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGraphics : MonoBehaviour {

    public ParticleSystem muzzleFlash;
    public GameObject impactEffectPrefab;
    public Vector3 rotationOffset;

    private void OnDestroy()
    {
        Debug.Log("This was destroyed", this);
    }

}
