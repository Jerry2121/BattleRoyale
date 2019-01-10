using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicators : MonoBehaviour {

    // Use this for initialization
    Image image;
	void Start () {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(image.color.a > 0)
        {
            Color a = image.color;
            a.a -= 1;
            image.color = a;
        }
	}
    public void Show()
    {
        Color a = image.color;

        if (gameObject.name == "HitMarker")
        {
            a.a += 255;
        }
        else
        {
            a.a += 64;
        }
        image.color = a;

    }
}
