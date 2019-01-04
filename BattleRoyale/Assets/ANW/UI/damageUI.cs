using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damageUI : MonoBehaviour {

    public Image hitMarker;
    public Image[] damageIndicator;
    Color changeAlpha = new Color(0, 0, 0, 255);

    private void Start()
    {
        hitMarker.color -= new Color(0, 0, 0, 255);
        for (int i = 0; i < damageIndicator.Length; i++)
        {
            damageIndicator[i].color -= new Color(0, 0, 0, 255);
        }
    }

    void Update()
    {
        if (hitMarker.color.a > 0)
        {
            
            hitMarker.color -= Time.deltaTime / 128 * changeAlpha;
        }

        for (int i = 0; i < damageIndicator.Length; i++)
        {
            if (damageIndicator[i].color.a > 0)
            {
                damageIndicator[i].color -= Time.deltaTime / 32 * changeAlpha;
            }
        }
    }

    public void FindDamageSourceDirection(float angle)
    {
        if (angle <= 22 && angle >= 0 || angle >= 337 && angle <= 359)
        {
            damageIndicator[5].color += new Color(0, 0, 0, 64);
        }

        else if (angle <= 67 && angle >= 22)
        {
           damageIndicator[6].color += new Color(0, 0, 0, 64);
        }

        else if (angle <= 112 && angle >= 67)
        {
            damageIndicator[3].color += new Color(0, 0, 0, 64);
        }

        else if (angle <= 157 && angle >= 112)
        {
            damageIndicator[1].color += new Color(0, 0, 0, 64);
        }

        else if (angle <= 202 && angle >= 157)
        {
            damageIndicator[0].color += new Color(0, 0, 0, 64);
        }

        else if (angle <= 247 && angle >= 202)
        {
            damageIndicator[2].color += new Color(0, 0, 0, 64);
        }

        else if (angle <= 292 && angle >= 247)
        {
            damageIndicator[4].color += new Color(0, 0, 0, 64);
        }

        else if (angle <= 337 && angle >= 292)
        {
            damageIndicator[7].color += new Color(0, 0, 0, 64);
        }
    }
}
