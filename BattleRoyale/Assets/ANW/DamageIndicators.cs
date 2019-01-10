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
	void Update () {
        // image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a, 0, 2f));
        
		if(image.color.a > 0)
        {
            Debug.Log("Before: " + image.name + " color is " + image.color);
            Color a = image.color;
            if (a.a < 0.01f)
                a.a = 0;
            else if (image.name == "HitMarker")
                a.a -= 0.1f;
            else
                a.a -= 0.01f;
            image.color = a;
            Debug.Log("After: " + image.name + " color is " + image.color);
        }
	}
    public void Show()
    {
        Color a = image.color;

        if (gameObject.name == "HitMarker")
        {
            a.a += 1;
        }
        else
        {
            a.a += 0.25f;
        }
        image.color = a;

        Debug.Log(a);
        //image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a, 0, 2f));
    }
}
