using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damageUI : MonoBehaviour {

    [SerializeField]
    public Image hitMarker;
    [SerializeField]
    public Image[] damageIndicator;
    [SerializeField]
    Color alphaSet;

    private void Start()
    {
        alphaSet = hitMarker.color;
        alphaSet.a = 0;
        hitMarker.color = alphaSet;
        for (int i = 0; i < damageIndicator.Length; i++)
        {
            alphaSet = damageIndicator[i].color;
            alphaSet.a = 0;
            damageIndicator[i].color = alphaSet;
        }
    }

    /*
    void Update()
    {
        if (hitMarker.color.a > 0)
        {
            Debug.Log("Hit marker has been displayed!");
            alphaSet = hitMarker.color;
            alphaSet.a -= Time.deltaTime;
            hitMarker.color = alphaSet;
        }

        for (int i = 0; i < damageIndicator.Length; i++)
        {
            if (damageIndicator[i].color.a > 0)
            {
                Debug.Log("Damage indicator " + i + " has been displayed!");
                alphaSet = damageIndicator[i].color;
                alphaSet.a -= Time.deltaTime / 4;
                damageIndicator[i].color = alphaSet;
            }
        }
    }
    */

    public void ShowHitMarker()
    {
        hitMarker.GetComponent<DamageIndicators>().Show();
    }

    public void FindDamageSourceDirection(float angle)
    {
        if (angle <= 22 && angle >= -22)
        {

            damageIndicator[5].GetComponent<DamageIndicators>().Show();

            Debug.Log("Damage from front!");
        }

        else if (angle <= 67 && angle >= 22)
        {

            damageIndicator[6].GetComponent<DamageIndicators>().Show();

            Debug.Log("Damage from front-left!");
        }

        else if (angle <= 112 && angle >= 67)
        {

            damageIndicator[3].GetComponent<DamageIndicators>().Show();

            Debug.Log("Damage from left!");
        }

        else if (angle <= 157 && angle >= 112)
        {

            damageIndicator[1].GetComponent<DamageIndicators>().Show();

            Debug.Log("Damage from back-left!");
        }

        else if (angle >= 157 || angle <= -157)
        {
            
            damageIndicator[0].GetComponent<DamageIndicators>().Show();
            
            Debug.Log("Damage from back!");
        }

        else if (angle <= -112 && angle >= -157)
        {
            
            damageIndicator[2].GetComponent<DamageIndicators>().Show();
            
            Debug.Log("Damage from back-right!");
        }

        else if (angle <= -67 && angle >= -112)
        {
            
            damageIndicator[4].GetComponent<DamageIndicators>().Show();
            
            Debug.Log("Damage from right!");
        }

        else if (angle <= -22 && angle >= -67)
        {
            
            damageIndicator[7].GetComponent<DamageIndicators>().Show();
           
            Debug.Log("Damage from front-right!");
        }
    }
}
